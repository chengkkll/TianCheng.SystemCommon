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
    [CollectionMapping("System_DepartmentInfo")]
    public class DepartmentInfo : BusinessMongoModel
    {
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

        /// <summary>
        /// 上级部门Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 部门主管ID
        /// </summary>
        public string ManageId { get; set; }
        /// <summary>
        /// 部门主管名称
        /// </summary>
        public string ManageName { get; set; }


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


        ////行业信息、人员信息、部门管理员
        ///// <summary>
        ///// 部门内的行业列表
        ///// </summary>
        //private List<BaseViewModel> _Industries = new List<BaseViewModel>();
        ///// <summary>
        ///// 部门内的行业列表
        ///// </summary>
        //public List<BaseViewModel> Industries
        //{
        //    get { return _Industries; }
        //    set
        //    {
        //        if (value == null)
        //            _Industries = new List<BaseViewModel>();
        //        else
        //            _Industries = value;
        //    }
        //}

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
    }
}
