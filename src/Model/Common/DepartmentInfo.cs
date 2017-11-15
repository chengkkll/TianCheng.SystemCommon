using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianCheng.DAL.MongoDB;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class DepartmentInfo : BusinessMongoModel
    {
        #region 部门基本信息
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序的序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string Desc { get; set; }

        #endregion

        #region 上级信息
        /// <summary>
        /// 上级部门Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }
        #endregion

        #region 主管信息
        /// <summary>
        /// 部门主管ID
        /// </summary>
        public string ManageId { get; set; }
        /// <summary>
        /// 部门主管名称
        /// </summary>
        public string ManageName { get; set; }
        #endregion

        #region 子部门信息
        /// <summary>
        /// 子部门列表
        /// </summary>
        private List<BaseViewModel> _SubList = new List<BaseViewModel>();
        /// <summary>
        /// 子部门列表
        /// </summary>
        public List<BaseViewModel> SubList
        {
            get { return _SubList; }
            set
            {
                if (value == null)
                    _SubList = new List<BaseViewModel>();
                else
                    _SubList = value;
            }
        }
        #endregion

        #region 行业信息
        /// <summary>
        /// 部门内的行业列表
        /// </summary>
        private List<BaseViewModel> _Industries = new List<BaseViewModel>();
        /// <summary>
        /// 部门内的行业列表
        /// </summary>
        public List<BaseViewModel> Industries
        {
            get { return _Industries; }
            set
            {
                if (value == null)
                    _Industries = new List<BaseViewModel>();
                else
                    _Industries = value;
            }
        }
        #endregion

        #region 部门内员工信息
        private List<SelectView> _Employees = new List<SelectView>();
        /// <summary>
        /// 部门内的员工信息
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonIgnore]
        public List<SelectView> Employees
        {
            get { return _Employees; }
            set
            {
                if (value == null)
                    _Employees = new List<SelectView>();
                else
                    _Employees = value;
            }
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 扩展ID 用于部门信息的扩展
        /// </summary>
        public string ExtId { get; set; }
        #endregion
    }
}
