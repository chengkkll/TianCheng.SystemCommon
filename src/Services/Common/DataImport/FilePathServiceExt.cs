using System;
using TianCheng.BaseService.Services;
using TianCheng.Model;

namespace TianCheng.Company.Services
{
    /// <summary>
    /// 文件路径服务，扩展处理
    /// </summary>
    static public class FilePathServiceExt
    {
        /// <summary>
        /// 转换路径
        /// </summary>
        /// <param name="diskPath"></param>
        /// <returns></returns>
        static private string WebPath(string diskPath)
        {
            return diskPath.Replace('\\', '/');
        }

        #region 导入员工信息文件
        /// <summary>
        /// 导入员工信息文件保存路径
        /// </summary>
        static private string DataImportEmployees { get; } = System.IO.Path.Combine(FilePathService.RootPath, "Files\\DataImport\\Employees");

        /// <summary>
        /// 导入的员工信息文件名
        /// </summary>
        static public FileNameView DataImportEmployeesFileName(this FilePathService fpservice)
        {
            string file = $"{Guid.NewGuid().ToString("N")}.xlsx";
            string webPath = WebPath(DataImportEmployees);
            return new FileNameView
            {
                DiskRoot = FilePathService.RootPath,
                WebRoot = "~",
                DiskPath = DataImportEmployees,
                WebPath = $"~/{webPath}",
                FileName = file,
                FileExtName = "xlsx",
                DiskFileFullName = $"{DataImportEmployees}\\{file}",
                WebFileFullName = $"~/{webPath}/{file}"
            };
        }
        /// <summary>
        /// 导入失败时的导入失败报表文件名
        /// </summary>
        /// <param name="fpservice"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        static public FileNameView DataImportEmployeesFailFileName(this FilePathService fpservice, string batch)
        {
            string file = $"fail-{batch}.xlsx";
            string webPath = WebPath(DataImportEmployees);
            return new FileNameView
            {
                DiskRoot = FilePathService.RootPath,
                WebRoot = "~",
                DiskPath = DataImportEmployees,
                WebPath = $"~/{webPath}",
                FileName = file,
                FileExtName = "xlsx",
                DiskFileFullName = $"{DataImportEmployees}\\{file}",
                WebFileFullName = $"~/{webPath}/{file}"
            };
        }
        #endregion
    }
}
