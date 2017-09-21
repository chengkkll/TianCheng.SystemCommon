using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 员工信息 [查看对象]
    /// </summary>
    public class EmployeeView : BaseViewModel
    {
        #region 基本信息
        /// <summary>
        /// 员工编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty("headimg")]
        public string HeadImg { get; set; }
        #endregion

        #region 联系信息
        /// <summary>
        /// 手机电话
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        /// <summary>
        /// 座机电话
        /// </summary>
        [JsonProperty("telephone")]
        public string Telephone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
        #endregion

        #region 登录信息
        /// <summary>
        /// 登录账号
        /// </summary>
        [JsonProperty("logon_account")]
        public string LogonAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [JsonProperty("logon_password")]
        public string LogonPassword { get; set; }
        #endregion

        #region 登录令牌信息
        /// <summary>
        /// 动态令牌Id
        /// </summary>
        [JsonProperty("secure_id")]
        public string SecureKeyId { get; set; }
        /// <summary>
        /// 动态令牌号
        /// </summary>
        [JsonProperty("secure_mark")]
        public string SecureKeyMark { get; set; }
        #endregion

        #region 部门信息

        /// <summary>
        /// 部门信息
        /// </summary>
        [JsonProperty("department")]
        public SelectView Department { get ;  set ; }

        #endregion

        #region 角色信息

        /// <summary>
        /// 角色信息
        /// </summary>
        [JsonProperty("role")]
        public SelectView Role { get; set; }

        #endregion

        #region 扩展信息
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("gender")]
        public UserGender gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty("birthday")]
        public DateTime? birthday { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [JsonProperty("education")]
        public string education { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        [JsonProperty("blood_type")]
        public string BloodType { get; set; }
        /// <summary>
        /// 职称 
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [JsonProperty("native_place")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("idnumber")]
        public string IdNumber { get; set; }
        #endregion

        #region 状态
        /// <summary>
        /// "状态（1-可用，3-锁住，5-禁用）
        /// 新增修改时，不用设置，列表时需要根据具体值判断如果现实数据。如果为1时显示‘禁用’按钮，为3时显示‘登录解锁’按钮，为5时显示‘取消禁用’按钮"
        /// </summary>
        [JsonProperty("state")]
        public UserState State { get; set; }

        #endregion

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [JsonProperty("update_date")]
        public string UpdateDate { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        [JsonProperty("updater_name")]
        public string UpdaterName { get; set; }

        /// <summary>
        /// 扩展ID 用于员工信息的扩展
        /// </summary>
        [JsonProperty("ext_id")]
        public string ExtId { get; set; }
    }
}
