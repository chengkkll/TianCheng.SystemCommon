using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService.PlugIn.Swagger;
using TianCheng.BaseService.Services;
using TianCheng.Company.Services;
using TianCheng.DataImport.Controller;
using TianCheng.DataImport.Model;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 员工数据导入
    /// </summary>
    [Produces("application/json")]
    [Route("api/Employees/Import")]
    public class DataImportEmployeesController
        : DataImportController<EmployeesImportDetail, EmployeesImportDetailDAL, ImportEmployeesService, EmployeeInfo>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>     
        public DataImportEmployeesController()
        {

        }
        #endregion

        #region 查询导入列表
        /// <summary>
        /// 查询员工导入历史
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employees.Import.Search")]
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpGet("Search")]
        [SwaggerFileUpload]
        public List<DataImportIndexView> Search()
        {
            return importService.SearchMainByType();
        }
        /// <summary>
        /// 查看导入详情
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpGet("Single/{id}")]
        [SwaggerFileUpload]
        public DataImportIndexView Single(string id)
        {
            return importService.SearchMainById(id);
        }
        #endregion

        #region 保存上传文件并解析持久化
        /// <summary>
        /// 导入员工数据  上传导入企业文件，并返导入成功的文件信息
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employees.Import.Upload")]
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpPost("Upload")]
        [SwaggerFileUpload]
        public DataImportIndexView Upload()
        {
            // 获取存储导入企业的文件信息
            FileNameView fileName = filePath.DataImportEmployeesFileName();
            // 将上传的文件保存至服务器磁盘
            SaveSingleFile(fileName.DiskFileFullName);
            // 解析上传的文件，并创建导入文件索引对象      
            return importService.AnalyzFile(fileName, LogonInfo);
        }
        #endregion

        #region 获取需要导入的数据
        /// <summary>
        /// 获取需要导入的数据   一次最多获取20个
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpGet("Step/{batch}-{start}")]
        public List<EmployeesImportDetail> Step(string batch, int start)
        {
            return importService.Step(batch, start);
        }
        #endregion

        #region 导入数据
        /// <summary>
        /// 导入一条数据
        /// </summary>
        /// <param name="importId"></param>
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpPost("{importId}")]
        public DataImportDetailResult ImportSingle(string importId)
        {
            return importService.ImportSingle(importId, LogonInfo);
        }
        #endregion

        #region 完成数据导入
        /// <summary>
        /// 完成数据导入
        /// </summary>
        /// <param name="batch"></param>
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpPatch("{batch}")]
        public void SetComplete(string batch)
        {
            FileNameView failFileName = filePath.DataImportEmployeesFailFileName(batch);
            importService.SetComplete(batch, failFileName, LogonInfo);
        }
        #endregion

        #region 删除导入数据
        /// <summary>
        /// 删除员工导入数据
        /// </summary>
        /// <param name="batch"></param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employees.Import.Remove")]
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpDelete("{batch}")]
        public ResultView Remove(string batch)
        {
            return importService.Remove(batch);
        }
        #endregion

        #region 下载导入失败结果
        /// <summary>
        /// 下载导入失败报表
        /// </summary>
        /// <param name="batch"></param>
        [SwaggerOperation(Tags = new[] { "企业管理-数据导入" })]
        [HttpGet("Download/Fail/{batch}")]
        public IActionResult DownloadFailFile(string batch)
        {
            ImportEmployeesService importService = ServiceLoader.GetService<ImportEmployeesService>();
            var main = importService.SearchMainByBatch(batch);
            if (!System.IO.File.Exists(main.FailImportFile.DiskFileFullName))
            {
                importService.GenerateFailFile(main);
            }
            return PhysicalFile(main.FailImportFile.DiskFileFullName, "application/x-xls", $"{batch}.{main.FailImportFile.FileExtName}");
        }
        #endregion
    }
}
