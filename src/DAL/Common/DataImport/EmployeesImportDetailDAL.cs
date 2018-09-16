using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL;
using TianCheng.DataImport.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 数据导入索引主表
    /// </summary>
    [DBMapping("import_employees")]
    public class EmployeesImportDetailDAL : DataImportDetailDAL<EmployeesImportDetail>
    {
    }
}
