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
    /// 员工信息
    /// </summary>
    [CollectionMapping("System_EmployeeInfo")]
    public class EmployeeInfo : BusinessMongoModel
    {
        /// <summary>
        /// 旧系统ID   主要用于数据导入时
        /// </summary>
        public string OldId { get; set; }

        #region 基本信息
        /// <summary>
        /// 员工编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请填写员工名称")]
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }
        #endregion

        #region 联系信息
        /// <summary>
        /// 手机电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 座机电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        #endregion

        #region 登录信息
        /// <summary>
        /// 登录账号
        /// </summary>
        [Required(ErrorMessage = "请填写登录账号")]
        public string LogonAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "请填写登录密码")]
        public string LogonPassword { get; set; }
        /// <summary>
        /// 登录错误的次数
        /// </summary>
        public int LogonWrongNum { get; set; }
        /// <summary>
        /// 锁定账号不允许登录
        /// </summary>
        public bool LogonLock { get; set; }
        #endregion

        #region 登录令牌信息
        /// <summary>
        /// 动态令牌Id
        /// </summary>
        public string SecureKeyId { get; set; }
        /// <summary>
        /// 动态令牌号
        /// </summary>
        public string SecureKeyMark { get; set; }
        #endregion

        #region 部门信息

        private SelectView _Department = new SelectView();
        /// <summary>
        /// 部门信息
        /// </summary>
        public SelectView Department { get { return _Department; } set { _Department = value; } }

        #endregion

        #region 角色信息
        /// <summary>
        /// 角色信息
        /// </summary>
        private SelectView _Role = new SelectView();
        /// <summary>
        /// 角色信息
        /// </summary>
        public SelectView Role { get { return _Role; } set { _Role = value; } }

        #endregion

        #region 扩展信息
        /// <summary>
        /// 性别
        /// </summary>
        public UserGender Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }
        /// <summary>
        /// 职称 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNumber { get; set; }
        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        public UserState State { get; set; }
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateText
        {
            get
            {
                if (this == null)
                {
                    return String.Empty;
                }

                switch (State)
                {
                    case UserState.Disable: return "禁用";
                    case UserState.Enable: return "正常";
                    case UserState.LogonLock: return "登录已锁";
                    default: return String.Empty;
                }
            }
        }
        #endregion

        /// <summary>
        /// 扩展ID 用于员工信息的扩展
        /// </summary>
        public string ExtId { get; set; }
    }
    
    /// <summary>
    /// 用户性别枚举
    /// </summary>
    public enum UserGender
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,
        /// <summary>
        /// 男性
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女性
        /// </summary>
        Female = 2
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// 正常可用状态
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 登录锁住
        /// </summary>
        LogonLock = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 5
    }
}
