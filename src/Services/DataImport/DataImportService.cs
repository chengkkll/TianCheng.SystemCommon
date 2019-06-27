using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using TianCheng.BaseService;
using TianCheng.Excel;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 数据导入  服务
    /// </summary>
    public class DataImportService<T, TD, Info> : IServiceRegister
        where T : DataImportDetailInfo, new()
        where TD : DataImportDetailDAL<T>
        where Info : BusinessMongoModel, new()
    {
        #region 构造方法
        /// <summary>
        /// 数据导入索引信息持久化操作
        /// </summary>
        protected readonly DataImportIndexDAL mainDal;
        /// <summary>
        /// 数据导入明细信息持久化操作
        /// </summary>
        protected readonly TD detailDal;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DataImportService()
        {
            mainDal = ServiceLoader.GetService<DataImportIndexDAL>();
            detailDal = ServiceLoader.GetService<TD>();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据类型
        /// </summary>
        protected virtual string DataType { get; }
        #endregion

        #region 查询导入主表信息
        /// <summary>
        /// 获取本类型数据的导入列表
        /// </summary>
        /// <returns></returns>
        public List<DataImportIndexView> SearchMainByType()
        {
            var result = mainDal.Queryable().Where(e => e.DataType == DataType).OrderByDescending(e => e.CreateDate).ToList();
            return AutoMapper.Mapper.Map<List<DataImportIndexView>>(result);
        }
        /// <summary>
        /// 根据ID查询主导入信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataImportIndexView SearchMainById(string id)
        {
            var result = mainDal.SearchById(id);
            return AutoMapper.Mapper.Map<DataImportIndexView>(result);
        }
        /// <summary>
        /// 根据批次获取导入的主表信息
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public DataImportIndexInfo SearchMainByBatch(string batch)
        {
            var main = mainDal.Queryable().Where(e => e.Batch == batch).FirstOrDefault();
            if (main == null)
            {
                ApiException.ThrowBadRequest("无法找到导入的数据信息");
            }
            return main;
        }
        #endregion

        #region 解析Excel并持久化
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string GetBatch(int index = 1)
        {
            string batch = DataType + DateTime.Now.ToString($"yyyyMMddhhmm-{index}");
            bool has = mainDal.Queryable().Where(e => e.Batch == batch).Count() > 0;
            if (!has)
            {
                return batch;
            }
            return GetBatch(index++);
        }
        /// <summary>
        /// 解析Excel文件并将其数据持久化
        /// </summary>
        /// <param name="file"></param>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public DataImportIndexView AnalyzFile(FileNameView file, TokenLogonInfo logonInfo)
        {
            // 创建导入的主索引数据
            DataImportIndexInfo main = new DataImportIndexInfo()
            {
                ImportFile = file,
                Batch = GetBatch(), // file.FileName.Replace($".{file.FileExtName}", ""),
                DataType = DataType,
                CreateDate = DateTime.Now,
                CreaterId = logonInfo.Id,
                CreaterName = logonInfo.Name,
                UpdateDate = DateTime.Now,
                UpdaterId = logonInfo.Id,
                UpdaterName = logonInfo.Name,
                ProcessState = ProcessState.Edit,
                IsDelete = false,
                Count = 0,
                Current = 0,
                IsComplete = false
            };

            // 读取Excel文件中的数据
            List<T> excelData = ExcelHelper.Import<T>(main.ImportFile.DiskFileFullName);
            // 按Excel中的行号排序
            excelData = excelData.OrderBy(e => e.RowIndex).ToList();
            // 完善提交的数据信息
            foreach (var item in excelData)
            {
                item.Batch = main.Batch;
                item.ImportState = ImportState.None;
                item.CreateDate = DateTime.Now;
                item.CreaterId = logonInfo.Id;
                item.CreaterName = logonInfo.Name;
                item.UpdateDate = DateTime.Now;
                item.UpdaterId = logonInfo.Id;
                item.UpdaterName = logonInfo.Name;
            }
            //解析导入的数据信息
            List<T> importList = AnalyzeImportList(excelData);

            // 持久化导入索引信息
            main.Count = excelData.Count;
            main.Current = 1;
            mainDal.InsertObject(main);
            // 持久化导入数据
            detailDal.InsertRange(excelData);

            // 返回导入的索引查看对象
            return AutoMapper.Mapper.Map<DataImportIndexView>(main);
        }

        /// <summary>
        /// 解析导入的数据信息
        /// </summary>
        /// <param name="excelData"></param>
        /// <returns></returns>
        protected virtual List<T> AnalyzeImportList(List<T> excelData)
        {
            return excelData;
        }
        #endregion

        #region 获取需要导入的数据
        /// <summary>
        /// 获取需要导入的数据
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<T> Step(string batch, int start)
        {
            // 根据批次信息获取最多20条导入的数据
            return detailDal.Queryable().Where(e => e.Batch == batch).OrderBy(e => e.RowIndex).Skip(start).Take(20).ToList();
        }
        #endregion

        #region 导入数据
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        protected virtual Info Tran(T import)
        {
            //转换主对象信息            
            return AutoMapper.Mapper.Map<Info>(import);
        }

        /// <summary>
        /// 检查导入数据的正确性
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void ImportCheck(T import, Info info)
        {
            string fail = String.Empty;
            if (!String.IsNullOrEmpty(fail))
            {
                import.ImportState = ImportState.Fail;
                import.FailureReason = fail;
            }
            import.ImportState = ImportState.Check;
            import.FailureReason = fail;
        }
        /// <summary>
        /// 排重的检查
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        protected virtual void RepeatCheck(T import, Info info)
        {
            string fail = String.Empty;
            if (!String.IsNullOrEmpty(fail))
            {
                import.ImportState = ImportState.Fail;
                import.FailureReason = fail;
            }
            import.ImportState = ImportState.Check;
            import.FailureReason = fail;
        }
        /// <summary>
        /// 导入数据的具体操作
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected virtual void Import(T import, Info info, TokenLogonInfo logonInfo)
        {

        }

        /// <summary>
        /// 按数据ID执行导入操作，返回导入结果及失败原因
        /// </summary>
        /// <param name="importId"></param>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public DataImportDetailResult ImportSingle(string importId, TokenLogonInfo logonInfo)
        {
            #region 获取导入数据
            // 获取需要导入的数据
            T import = detailDal.SearchById(importId);
            if (import == null)
            {
                return new DataImportDetailResult
                {
                    Id = importId,
                    ImportState = ImportState.Fail,
                    ImportResult = "导入失败",
                    FailureReason = "导入数据的ID错误。id:" + importId
                };
            }
            #endregion

            // 数据转换
            Info info;
            try
            {
                info = Tran(import);
                info.CreateDate = DateTime.Now;
                info.CreaterId = logonInfo.Id;
                info.CreaterName = logonInfo.Name;
                info.UpdateDate = DateTime.Now;
                info.UpdaterId = logonInfo.Id;
                info.UpdaterName = logonInfo.Name;
            }
            catch (Exception ex)
            {
                return new DataImportDetailResult
                {
                    Id = importId,
                    RowIndex = import.RowIndex,
                    Batch = import.Batch,
                    ImportState = ImportState.Fail,
                    ImportResult = "导入失败",
                    FailureReason = ex.Message
                };
            }

            #region 检查数据
            // 检查导入的数据
            ImportCheck(import, info);
            if (import.ImportState == ImportState.Fail)
            {
                import.UpdateDate = DateTime.Now;
                import.UpdaterId = logonInfo.Id;
                import.UpdaterName = logonInfo.Name;
                detailDal.UpdateObject(import);
                return new DataImportDetailResult
                {
                    Id = importId,
                    RowIndex = import.RowIndex,
                    Batch = import.Batch,
                    ImportState = ImportState.Fail,
                    ImportResult = "导入失败",
                    FailureReason = import.FailureReason
                };
            }
            // 排重的检查  需要重写排重方法
            RepeatCheck(import, info);
            if (import.ImportState == ImportState.Fail)
            {
                import.UpdateDate = DateTime.Now;
                import.UpdaterId = logonInfo.Id;
                import.UpdaterName = logonInfo.Name;
                detailDal.UpdateObject(import);
                return new DataImportDetailResult
                {
                    Id = importId,
                    RowIndex = import.RowIndex,
                    Batch = import.Batch,
                    ImportState = ImportState.Fail,
                    ImportResult = "导入失败",
                    FailureReason = import.FailureReason
                };
            }
            #endregion

            // 保存导入的数据到对应的数据集合中
            try
            {
                // 保存导入后的对象信息
                info.Id = MongoDB.Bson.ObjectId.Empty;
                Import(import, info, logonInfo);
                if (String.IsNullOrEmpty(import.ImportResult))
                {
                    import.ImportResult = "导入成功";
                }
                // 保存导入前的对象信息    记录保存成功
                import.ImportState = ImportState.Complete;
                import.UpdateDate = DateTime.Now;
                import.UpdaterId = logonInfo.Id;
                import.UpdaterName = logonInfo.Name;
                detailDal.UpdateObject(import);
                // 更新主表进度
                var main = mainDal.Queryable().Where(e => e.Batch == import.Batch).FirstOrDefault();
                if (main != null)
                {
                    main.Current++;
                    mainDal.UpdateObject(main);
                }

                return new DataImportDetailResult
                {
                    Id = importId,
                    RowIndex = import.RowIndex,
                    Batch = import.Batch,
                    ImportState = ImportState.Complete,
                    ImportResult = "导入成功",
                };
            }
            catch (Exception ex)
            {
                //保存导入前的对象信息  记录导入失败及出错原因
                import.ImportState = ImportState.Fail;
                import.FailureReason = ex.Message;
                import.UpdateDate = DateTime.Now;
                import.UpdaterId = logonInfo.Id;
                import.UpdaterName = logonInfo.Name;
                import.FailureReason = ex.Message;
                import.ImportResult = "导入失败";
                detailDal.UpdateObject(import);
                return new DataImportDetailResult
                {
                    Id = importId,
                    RowIndex = import.RowIndex,
                    Batch = import.Batch,
                    ImportState = ImportState.Fail,
                    ImportResult = "导入失败",
                    FailureReason = import.FailureReason
                };
            }
        }

        #endregion

        #region 完成数据导入
        /// <summary>
        /// 设置导入完成
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="failFile"></param>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public void SetComplete(string batch, FileNameView failFile, TokenLogonInfo logonInfo)
        {
            // 获取主表数据
            var main = SearchMainByBatch(batch);
            // 设置主表数据
            main.ProcessState = ProcessState.Complete;
            main.IsComplete = true;
            main.UpdateDate = DateTime.Now;
            main.UpdaterId = logonInfo.Id;
            main.UpdaterName = logonInfo.Name;
            main.FailImportFile = failFile;
            // 生成导入失败的Excel文件
            // 异步生成导入失败的Excel文件
            ThreadPool.QueueUserWorkItem(h =>
            {
                GenerateFailFile(main);
            });
            // 持久化设置的主表数据
            mainDal.UpdateObject(main);
        }
        /// <summary>
        /// 生成失败的Excel文件
        /// </summary>
        /// <param name="main"></param>
        public void GenerateFailFile(DataImportIndexInfo main)
        {
            // 获取导入失败的数据
            var failList = detailDal.Queryable().Where(e => e.Batch == main.Batch && e.ImportState == ImportState.Fail).ToList();
            // 导出错误信息的文件，如果文件存在，先删除
            ExcelHelper.Export(failList, main.FailImportFile.DiskFileFullName);
        }
        #endregion

        /// <summary>
        /// 删除导入数据
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public ResultView Remove(string batch)
        {
            // 获取主表数据
            var main = SearchMainByBatch(batch);
            // 1、删除上传文件
            if (main.ImportFile != null && String.IsNullOrWhiteSpace(main.ImportFile.DiskFileFullName) && System.IO.File.Exists(main.ImportFile.DiskFileFullName))
            {
                System.IO.File.Delete(main.ImportFile.DiskFileFullName);
            }
            // 2、删除导入失败报表文件
            if (main.FailImportFile != null && String.IsNullOrWhiteSpace(main.FailImportFile.DiskFileFullName) && System.IO.File.Exists(main.FailImportFile.DiskFileFullName))
            {
                System.IO.File.Delete(main.FailImportFile.DiskFileFullName);
            }
            // 3、删除明细表数据
            var detailIds = detailDal.Queryable().Where(e => e.Batch == batch).Select(e => e.Id);
            detailDal.RemoveByTypeIdList(detailIds);
            // 4、删除主表信息
            mainDal.RemoveByTypeId(main.Id);
            return ResultView.Success();
        }

        #region 下载导入的失败文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="showName"></param>
        /// <returns></returns>
        public HttpResponseMessage DownloadFile(FileNameView file, string showName = "download")
        {
            //var context = ServiceLoader.GetService<Microsoft.AspNetCore.Http.HttpContextAccessor>();
            // context.HttpContext.Response.Headers.Add()
            try
            {
                var stream = new FileStream(file.DiskFileFullName, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                stream.Close();
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = showName + "." + file.FileExtName
                };
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

        }
        #endregion
    }
}
