using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.BaseService;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 区域服务
    /// </summary>
    public class AreaService : MongoBusinessService<AreaInfo, AreaView, AreaQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="servicesProvider"></param>
        public AreaService(AreaDAL dal, ILogger<AreaService> logger, IServiceProvider servicesProvider) 
            : base(dal)
        {

        }
        #endregion

        #region 数据初始化
        /// <summary>
        /// 初始化区域信息
        /// </summary>
        public void DefaultInit()
        {
            List<AreaInfo> areaList = new List<AreaInfo>
            {
                new AreaInfo() { Name = "中国", AreaType = AreaType.Country, Code = "86", TelephoneCode = "+86" },

                new AreaInfo() { Name = "北京市", AreaType = AreaType.Municipality, Code = "010", TelephoneCode = "010", SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "上海市", AreaType = AreaType.Municipality, Code = "021", TelephoneCode = "021", SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "天津市", AreaType = AreaType.Municipality, Code = "022", TelephoneCode = "022", SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "重庆市", AreaType = AreaType.Municipality, Code = "023", TelephoneCode = "023", SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "香港", AreaType = AreaType.Municipality, Code = "852", TelephoneCode = "852", SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "澳门", AreaType = AreaType.Municipality, Code = "853", TelephoneCode = "853", SuperiorCode = "86", SuperiorName = "中国" },

                new AreaInfo() { Name = "河北省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "邯郸市", AreaType = AreaType.City, Code = "0310", TelephoneCode = "0310", SuperiorName = "河北省" },
                new AreaInfo() { Name = "石家庄", AreaType = AreaType.City, Code = "0311", TelephoneCode = "0311", SuperiorName = "河北省" },
                new AreaInfo() { Name = "保定市", AreaType = AreaType.City, Code = "0312", TelephoneCode = "0312", SuperiorName = "河北省" },
                new AreaInfo() { Name = "张家口", AreaType = AreaType.City, Code = "0313", TelephoneCode = "0313", SuperiorName = "河北省" },
                new AreaInfo() { Name = "承德市", AreaType = AreaType.City, Code = "0314", TelephoneCode = "0314", SuperiorName = "河北省" },
                new AreaInfo() { Name = "唐山市", AreaType = AreaType.City, Code = "0315", TelephoneCode = "0315", SuperiorName = "河北省" },
                new AreaInfo() { Name = "廊坊市", AreaType = AreaType.City, Code = "0316", TelephoneCode = "0316", SuperiorName = "河北省" },
                new AreaInfo() { Name = "沧州市", AreaType = AreaType.City, Code = "0317", TelephoneCode = "0317", SuperiorName = "河北省" },
                new AreaInfo() { Name = "衡水市", AreaType = AreaType.City, Code = "0318", TelephoneCode = "0318", SuperiorName = "河北省" },
                new AreaInfo() { Name = "邢台市", AreaType = AreaType.City, Code = "0319", TelephoneCode = "0319", SuperiorName = "河北省" },
                new AreaInfo() { Name = "秦皇岛", AreaType = AreaType.City, Code = "0335", TelephoneCode = "0335", SuperiorName = "河北省" },

                new AreaInfo() { Name = "浙江省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "衢州市", AreaType = AreaType.City, Code = "0570", TelephoneCode = "0570", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "杭州市", AreaType = AreaType.City, Code = "0571", TelephoneCode = "0571", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "湖州市", AreaType = AreaType.City, Code = "0572", TelephoneCode = "0572", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "嘉兴市", AreaType = AreaType.City, Code = "0573", TelephoneCode = "0573", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "宁波市", AreaType = AreaType.City, Code = "0574", TelephoneCode = "0574", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "绍兴市", AreaType = AreaType.City, Code = "0575", TelephoneCode = "0575", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "台州市", AreaType = AreaType.City, Code = "0576", TelephoneCode = "0576", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "温州市", AreaType = AreaType.City, Code = "0577", TelephoneCode = "0577", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "丽水市", AreaType = AreaType.City, Code = "0578", TelephoneCode = "0578", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "金华市", AreaType = AreaType.City, Code = "0579", TelephoneCode = "0579", SuperiorName = "浙江省" },
                new AreaInfo() { Name = "舟山市", AreaType = AreaType.City, Code = "0580", TelephoneCode = "0580", SuperiorName = "浙江省" },

                new AreaInfo() { Name = "辽宁省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "沈阳市", AreaType = AreaType.City, Code = "024", TelephoneCode = "024", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "铁岭市", AreaType = AreaType.City, Code = "0410", TelephoneCode = "0410", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "大连市", AreaType = AreaType.City, Code = "0411", TelephoneCode = "0411", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "鞍山市", AreaType = AreaType.City, Code = "0412", TelephoneCode = "0412", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "抚顺市", AreaType = AreaType.City, Code = "0413", TelephoneCode = "0413", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "本溪市", AreaType = AreaType.City, Code = "0414", TelephoneCode = "0414", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "丹东市", AreaType = AreaType.City, Code = "0415", TelephoneCode = "0415", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "锦州市", AreaType = AreaType.City, Code = "0416", TelephoneCode = "0416", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "营口市", AreaType = AreaType.City, Code = "0417", TelephoneCode = "0417", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "阜新市", AreaType = AreaType.City, Code = "0418", TelephoneCode = "0418", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "辽阳市", AreaType = AreaType.City, Code = "0419", TelephoneCode = "0419", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "朝阳市", AreaType = AreaType.City, Code = "0421", TelephoneCode = "0421", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "盘锦市", AreaType = AreaType.City, Code = "0427", TelephoneCode = "0427", SuperiorName = "辽宁省" },
                new AreaInfo() { Name = "葫芦岛", AreaType = AreaType.City, Code = "0429", TelephoneCode = "0429", SuperiorName = "辽宁省" },

                new AreaInfo() { Name = "湖北省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "武汉市", AreaType = AreaType.City, Code = "027", TelephoneCode = "027", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "襄城市", AreaType = AreaType.City, Code = "0710", TelephoneCode = "0710", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "鄂州市", AreaType = AreaType.City, Code = "0711", TelephoneCode = "0711", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "孝感市", AreaType = AreaType.City, Code = "0712", TelephoneCode = "0712", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "黄州市", AreaType = AreaType.City, Code = "0713", TelephoneCode = "0713", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "黄石市", AreaType = AreaType.City, Code = "0714", TelephoneCode = "0714", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "咸宁市", AreaType = AreaType.City, Code = "0715", TelephoneCode = "0715", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "荆沙市", AreaType = AreaType.City, Code = "0716", TelephoneCode = "0716", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "宜昌市", AreaType = AreaType.City, Code = "0717", TelephoneCode = "0717", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "恩施市", AreaType = AreaType.City, Code = "0718", TelephoneCode = "0718", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "十堰市", AreaType = AreaType.City, Code = "0719", TelephoneCode = "0719", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "随枣市", AreaType = AreaType.City, Code = "0722", TelephoneCode = "0722", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "荆门市", AreaType = AreaType.City, Code = "0724", TelephoneCode = "0724", SuperiorName = "湖北省" },
                new AreaInfo() { Name = "江汉市", AreaType = AreaType.City, Code = "0728", TelephoneCode = "0728", SuperiorName = "湖北省" },

                new AreaInfo() { Name = "江苏省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "南京市", AreaType = AreaType.City, Code = "025", TelephoneCode = "025", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "无锡市", AreaType = AreaType.City, Code = "0510", TelephoneCode = "0510", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "镇江市", AreaType = AreaType.City, Code = "0511", TelephoneCode = "0511", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "苏州市", AreaType = AreaType.City, Code = "0512", TelephoneCode = "0512", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "南通市", AreaType = AreaType.City, Code = "0513", TelephoneCode = "0513", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "扬州市", AreaType = AreaType.City, Code = "0514", TelephoneCode = "0514", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "盐城市", AreaType = AreaType.City, Code = "0515", TelephoneCode = "0515", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "徐州市", AreaType = AreaType.City, Code = "0516", TelephoneCode = "0516", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "淮阴市", AreaType = AreaType.City, Code = "0517", TelephoneCode = "0517", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "淮安市", AreaType = AreaType.City, Code = "0517", TelephoneCode = "0517", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "连云港", AreaType = AreaType.City, Code = "0518", TelephoneCode = "0518", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "常州市", AreaType = AreaType.City, Code = "0519", TelephoneCode = "0519", SuperiorName = "江苏省" },
                new AreaInfo() { Name = "泰州市", AreaType = AreaType.City, Code = "0523", TelephoneCode = "0523", SuperiorName = "江苏省" },

                new AreaInfo() { Name = "内蒙古", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "海拉尔", AreaType = AreaType.City, Code = "0470", TelephoneCode = "0470", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "呼和浩特", AreaType = AreaType.City, Code = "0471", TelephoneCode = "0471", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "包头市", AreaType = AreaType.City, Code = "0472", TelephoneCode = "0472", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "乌海市", AreaType = AreaType.City, Code = "0473", TelephoneCode = "0473", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "集宁市", AreaType = AreaType.City, Code = "0474", TelephoneCode = "0474", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "通辽市", AreaType = AreaType.City, Code = "0475", TelephoneCode = "0475", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "赤峰市", AreaType = AreaType.City, Code = "0476", TelephoneCode = "0476", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "东胜市", AreaType = AreaType.City, Code = "0477", TelephoneCode = "0477", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "临河市", AreaType = AreaType.City, Code = "0478", TelephoneCode = "0478", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "锡林浩特", AreaType = AreaType.City, Code = "0479", TelephoneCode = "0479", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "乌兰浩特", AreaType = AreaType.City, Code = "0482", TelephoneCode = "0482", SuperiorName = "内蒙古" },
                new AreaInfo() { Name = "阿拉善左旗", AreaType = AreaType.City, Code = "0483", TelephoneCode = "0483", SuperiorName = "内蒙古" },

                new AreaInfo() { Name = "江西省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "新余市", AreaType = AreaType.City, Code = "0790", TelephoneCode = "0790", SuperiorName = "江西省" },
                new AreaInfo() { Name = "南昌市", AreaType = AreaType.City, Code = "0791", TelephoneCode = "0791", SuperiorName = "江西省" },
                new AreaInfo() { Name = "九江市", AreaType = AreaType.City, Code = "0792", TelephoneCode = "0792", SuperiorName = "江西省" },
                new AreaInfo() { Name = "上饶市", AreaType = AreaType.City, Code = "0793", TelephoneCode = "0793", SuperiorName = "江西省" },
                new AreaInfo() { Name = "临川市", AreaType = AreaType.City, Code = "0794", TelephoneCode = "0794", SuperiorName = "江西省" },
                new AreaInfo() { Name = "宜春市", AreaType = AreaType.City, Code = "0795", TelephoneCode = "0795", SuperiorName = "江西省" },
                new AreaInfo() { Name = "吉安市", AreaType = AreaType.City, Code = "0796", TelephoneCode = "0796", SuperiorName = "江西省" },
                new AreaInfo() { Name = "赣州市", AreaType = AreaType.City, Code = "0797", TelephoneCode = "0797", SuperiorName = "江西省" },
                new AreaInfo() { Name = "景德镇", AreaType = AreaType.City, Code = "0798", TelephoneCode = "0798", SuperiorName = "江西省" },
                new AreaInfo() { Name = "萍乡市", AreaType = AreaType.City, Code = "0799", TelephoneCode = "0799", SuperiorName = "江西省" },
                new AreaInfo() { Name = "鹰潭市", AreaType = AreaType.City, Code = "0701", TelephoneCode = "0701", SuperiorName = "江西省" },

                new AreaInfo() { Name = "山西省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "忻州市", AreaType = AreaType.City, Code = "0350", TelephoneCode = "0350", SuperiorName = "山西省" },
                new AreaInfo() { Name = "太原市", AreaType = AreaType.City, Code = "0351", TelephoneCode = "0351", SuperiorName = "山西省" },
                new AreaInfo() { Name = "大同市", AreaType = AreaType.City, Code = "0352", TelephoneCode = "0352", SuperiorName = "山西省" },
                new AreaInfo() { Name = "阳泉市", AreaType = AreaType.City, Code = "0353", TelephoneCode = "0353", SuperiorName = "山西省" },
                new AreaInfo() { Name = "榆次市", AreaType = AreaType.City, Code = "0354", TelephoneCode = "0354", SuperiorName = "山西省" },
                new AreaInfo() { Name = "长治市", AreaType = AreaType.City, Code = "0355", TelephoneCode = "0355", SuperiorName = "山西省" },
                new AreaInfo() { Name = "晋城市", AreaType = AreaType.City, Code = "0356", TelephoneCode = "0356", SuperiorName = "山西省" },
                new AreaInfo() { Name = "临汾市", AreaType = AreaType.City, Code = "0357", TelephoneCode = "0357", SuperiorName = "山西省" },
                new AreaInfo() { Name = "离石市", AreaType = AreaType.City, Code = "0358", TelephoneCode = "0358", SuperiorName = "山西省" },
                new AreaInfo() { Name = "运城市", AreaType = AreaType.City, Code = "0359", TelephoneCode = "0359", SuperiorName = "山西省" },

                new AreaInfo() { Name = "甘肃省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "临夏市", AreaType = AreaType.City, Code = "0930", TelephoneCode = "0930", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "兰州市", AreaType = AreaType.City, Code = "0931", TelephoneCode = "0931", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "定西市", AreaType = AreaType.City, Code = "0932", TelephoneCode = "0932", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "平凉市", AreaType = AreaType.City, Code = "0933", TelephoneCode = "0933", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "西峰市", AreaType = AreaType.City, Code = "0934", TelephoneCode = "0934", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "武威市", AreaType = AreaType.City, Code = "0935", TelephoneCode = "0935", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "张掖市", AreaType = AreaType.City, Code = "0936", TelephoneCode = "0936", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "酒泉市", AreaType = AreaType.City, Code = "0937", TelephoneCode = "0937", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "天水市", AreaType = AreaType.City, Code = "0938", TelephoneCode = "0938", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "甘南州", AreaType = AreaType.City, Code = "0941", TelephoneCode = "0941", SuperiorName = "甘肃省" },
                new AreaInfo() { Name = "白银市", AreaType = AreaType.City, Code = "0943", TelephoneCode = "0943", SuperiorName = "甘肃省" },

                new AreaInfo() { Name = "山东省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "菏泽市", AreaType = AreaType.City, Code = "0530", TelephoneCode = "0530", SuperiorName = "山东省" },
                new AreaInfo() { Name = "济南市", AreaType = AreaType.City, Code = "0531", TelephoneCode = "0531", SuperiorName = "山东省" },
                new AreaInfo() { Name = "青岛市", AreaType = AreaType.City, Code = "0532", TelephoneCode = "0532", SuperiorName = "山东省" },
                new AreaInfo() { Name = "淄博市", AreaType = AreaType.City, Code = "0533", TelephoneCode = "0533", SuperiorName = "山东省" },
                new AreaInfo() { Name = "德州市", AreaType = AreaType.City, Code = "0534", TelephoneCode = "0534", SuperiorName = "山东省" },
                new AreaInfo() { Name = "烟台市", AreaType = AreaType.City, Code = "0535", TelephoneCode = "0535", SuperiorName = "山东省" },
                new AreaInfo() { Name = "淮坊市", AreaType = AreaType.City, Code = "0536", TelephoneCode = "0536", SuperiorName = "山东省" },
                new AreaInfo() { Name = "济宁市", AreaType = AreaType.City, Code = "0537", TelephoneCode = "0537", SuperiorName = "山东省" },
                new AreaInfo() { Name = "泰安市", AreaType = AreaType.City, Code = "0538", TelephoneCode = "0538", SuperiorName = "山东省" },
                new AreaInfo() { Name = "临沂市", AreaType = AreaType.City, Code = "0539", TelephoneCode = "0539", SuperiorName = "山东省" },

                new AreaInfo() { Name = "黑龙江", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "阿城市", AreaType = AreaType.City, Code = "0450", TelephoneCode = "0450", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "哈尔滨", AreaType = AreaType.City, Code = "0451", TelephoneCode = "0451", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "齐齐哈尔", AreaType = AreaType.City, Code = "0452", TelephoneCode = "0452", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "牡丹江", AreaType = AreaType.City, Code = "0453", TelephoneCode = "0453", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "佳木斯", AreaType = AreaType.City, Code = "0454", TelephoneCode = "0454", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "绥化市", AreaType = AreaType.City, Code = "0455", TelephoneCode = "0455", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "黑河市", AreaType = AreaType.City, Code = "0456", TelephoneCode = "0456", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "加格达奇", AreaType = AreaType.City, Code = "0457", TelephoneCode = "0457", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "伊春市", AreaType = AreaType.City, Code = "0458", TelephoneCode = "0458", SuperiorName = "黑龙江" },
                new AreaInfo() { Name = "大庆市", AreaType = AreaType.City, Code = "0459", TelephoneCode = "0459", SuperiorName = "黑龙江" },

                new AreaInfo() { Name = "福建省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "福州市", AreaType = AreaType.City, Code = "0591", TelephoneCode = "0591", SuperiorName = "福建省" },
                new AreaInfo() { Name = "厦门市", AreaType = AreaType.City, Code = "0592", TelephoneCode = "0592", SuperiorName = "福建省" },
                new AreaInfo() { Name = "宁德市", AreaType = AreaType.City, Code = "0593", TelephoneCode = "0593", SuperiorName = "福建省" },
                new AreaInfo() { Name = "莆田市", AreaType = AreaType.City, Code = "0594", TelephoneCode = "0594", SuperiorName = "福建省" },
                new AreaInfo() { Name = "泉州市", AreaType = AreaType.City, Code = "0595", TelephoneCode = "0595", SuperiorName = "福建省" },
                new AreaInfo() { Name = "晋江市", AreaType = AreaType.City, Code = "0595", TelephoneCode = "0595", SuperiorName = "福建省" },
                new AreaInfo() { Name = "漳州市", AreaType = AreaType.City, Code = "0596", TelephoneCode = "0596", SuperiorName = "福建省" },
                new AreaInfo() { Name = "龙岩市", AreaType = AreaType.City, Code = "0597", TelephoneCode = "0597", SuperiorName = "福建省" },
                new AreaInfo() { Name = "三明市", AreaType = AreaType.City, Code = "0598", TelephoneCode = "0598", SuperiorName = "福建省" },
                new AreaInfo() { Name = "南平市", AreaType = AreaType.City, Code = "0599", TelephoneCode = "0599", SuperiorName = "福建省" },

                new AreaInfo() { Name = "广东省", AreaType = AreaType.Province, SuperiorCode = "86", SuperiorName = "中国" },
                new AreaInfo() { Name = "广州市", AreaType = AreaType.City, Code = "020", TelephoneCode = "020", SuperiorName = "广东省" },
                new AreaInfo() { Name = "韶关市", AreaType = AreaType.City, Code = "0751", TelephoneCode = "0751", SuperiorName = "广东省" },
                new AreaInfo() { Name = "惠州市", AreaType = AreaType.City, Code = "0752", TelephoneCode = "0752", SuperiorName = "广东省" },
                new AreaInfo() { Name = "梅州市", AreaType = AreaType.City, Code = "0753", TelephoneCode = "0753", SuperiorName = "广东省" },
                new AreaInfo() { Name = "汕头市", AreaType = AreaType.City, Code = "0754", TelephoneCode = "0754", SuperiorName = "广东省" },
                new AreaInfo() { Name = "深圳市", AreaType = AreaType.City, Code = "0755", TelephoneCode = "0755", SuperiorName = "广东省" },
                new AreaInfo() { Name = "珠海市", AreaType = AreaType.City, Code = "0756", TelephoneCode = "0756", SuperiorName = "广东省" },
                new AreaInfo() { Name = "佛山市", AreaType = AreaType.City, Code = "0757", TelephoneCode = "0757", SuperiorName = "广东省" },
                new AreaInfo() { Name = "肇庆市", AreaType = AreaType.City, Code = "0758", TelephoneCode = "0758", SuperiorName = "广东省" },
                new AreaInfo() { Name = "湛江市", AreaType = AreaType.City, Code = "0759", TelephoneCode = "0759", SuperiorName = "广东省" },
                new AreaInfo() { Name = "中山市", AreaType = AreaType.City, Code = "0760", TelephoneCode = "0760", SuperiorName = "广东省" },
                new AreaInfo() { Name = "河源市", AreaType = AreaType.City, Code = "0762", TelephoneCode = "0762", SuperiorName = "广东省" },
                new AreaInfo() { Name = "清远市", AreaType = AreaType.City, Code = "0763", TelephoneCode = "0763", SuperiorName = "广东省" },
                new AreaInfo() { Name = "顺德市", AreaType = AreaType.City, Code = "0765", TelephoneCode = "0765", SuperiorName = "广东省" },
                new AreaInfo() { Name = "云浮市", AreaType = AreaType.City, Code = "0766", TelephoneCode = "0766", SuperiorName = "广东省" },
                new AreaInfo() { Name = "潮州市", AreaType = AreaType.City, Code = "0768", TelephoneCode = "0768", SuperiorName = "广东省" },
                new AreaInfo() { Name = "东莞市", AreaType = AreaType.City, Code = "0769", TelephoneCode = "0769", SuperiorName = "广东省" },
                new AreaInfo() { Name = "汕尾市", AreaType = AreaType.City, Code = "0660", TelephoneCode = "0660", SuperiorName = "广东省" },
                new AreaInfo() { Name = "潮阳市", AreaType = AreaType.City, Code = "0661", TelephoneCode = "0661", SuperiorName = "广东省" },
                new AreaInfo() { Name = "阳江市", AreaType = AreaType.City, Code = "0662", TelephoneCode = "0662", SuperiorName = "广东省" },
                new AreaInfo() { Name = "揭西市", AreaType = AreaType.City, Code = "0663", TelephoneCode = "0663", SuperiorName = "广东省" }
            };


            //            四川省
            //            成都市 028
            //涪陵市 0810
            //重庆市 0811
            //攀枝花 0812
            //自贡市 0813
            //永川市 0814
            //绵阳市 0816
            //南充市 0817
            //达县市 0818
            //万县市 0819
            //遂宁市 0825
            //广安市 0826
            //巴中市 0827
            //泸州市 0830
            //宜宾市 0831
            //内江市 0832
            //乐山市 0833
            //西昌市 0834
            //雅安市 0835
            //康定市 0836
            //马尔康 0837
            //德阳市 0838
            //广元市 0839
            //泸州市 0840


            //湖南省
            //岳阳市 0730
            //长沙市 0731
            //湘潭市 0732
            //株州市 0733
            //衡阳市 0734
            //郴州市 0735
            //常德市 0736
            //益阳市 0737
            //娄底市 0738
            //邵阳市 0739
            //吉首市 0743
            //张家界 0744
            //怀化市 0745
            //永州冷 0746

            //                河南省
            //商丘市 0370
            //郑州市 0371
            //安阳市 0372
            //新乡市 0373
            //许昌市 0374
            //平顶山 0375
            //信阳市 0376
            //南阳市 0377
            //开封市 0378
            //洛阳市 0379
            //焦作市 0391
            //鹤壁市 0392
            //濮阳市 0393
            //周口市 0394
            //漯河市 0395
            //驻马店 0396
            //三门峡 0398


            //                云南省
            //昭通市 0870
            //昆明市 0871
            //大理市 0872
            //个旧市 0873
            //曲靖市 0874
            //保山市 0875
            //文山市 0876
            //玉溪市 0877
            //楚雄市 0878
            //思茅市 0879
            //景洪市 0691
            //潞西市 0692
            //东川市 0881
            //临沧市 0883
            //六库市 0886
            //中甸市 0887
            //丽江市 0888


            //安徽省
            //滁州市 0550
            //合肥市 0551
            //蚌埠市 0552
            //芜湖市 0553
            //淮南市 0554
            //马鞍山 0555
            //安庆市 0556
            //宿州市 0557
            //阜阳市 0558
            //黄山市 0559
            //淮北市 0561
            //铜陵市 0562
            //宣城市 0563
            //六安市 0564
            //巢湖市 0565
            //贵池市 0566

            //宁夏
            //银川市 0951
            //石嘴山 0952
            //吴忠市 0953
            //固原市 0954

            //吉林省
            //长春市 0431
            //吉林市 0432
            //延吉市 0433
            //四平市 0434
            //通化市 0435
            //白城市 0436
            //辽源市 0437
            //松原市 0438
            //浑江市 0439
            //珲春市 0440    

            //                广西省
            //防城港 0770
            //南宁市 0771
            //柳州市 0772
            //桂林市 0773
            //梧州市 0774
            //玉林市 0775
            //百色市 0776
            //钦州市 0777
            //河池市 0778
            //北海市 0779    

            //                贵州省
            //贵阳市 0851
            //遵义市 0852
            //安顺市 0853
            //都均市 0854
            //凯里市 0855
            //铜仁市 0856
            //毕节市 0857
            //六盘水 0858
            //兴义市 0859    

            //                陕西省
            //西安市 029
            //咸阳市 0910
            //延安市 0911
            //榆林市 0912
            //渭南市 0913
            //商洛市 0914
            //安康市 0915
            //汉中市 0916
            //宝鸡市 0917
            //铜川市 0919    

            //                青海省
            //西宁市 0971
            //海东市 0972
            //同仁市 0973
            //共和市 0974
            //玛沁市 0975
            //玉树市 0976
            //德令哈 0977    
            //                海南省
            //儋州市 0890
            //海口市 0898
            //三亚市 0899

            //                西藏
            //拉萨市 0891
            //日喀则 0892
            //山南市 0893





            //_Dal.Drop();
            //_Dal.Insert(areaList);
        }
        #endregion

        #region 查询方法     
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<AreaInfo> _Filter(AreaQuery input)
        {
            var query = _Dal.Queryable();

            #region 查询条件

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
            }
            // 按编码模糊查询
            if (!string.IsNullOrWhiteSpace(input.Code))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Code) && e.Code.Contains(input.Code));
            }
            // 按上级区域Code查询
            if (!string.IsNullOrWhiteSpace(input.SuperiorCode))
            {
                query = query.Where(e => e.SuperiorCode == input.SuperiorCode);
            }
            // 按上级区域名称模糊查询
            if (!string.IsNullOrWhiteSpace(input.SuperiorName))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.SuperiorName) && e.SuperiorName.Contains(input.SuperiorName));
            }
            // 按区域类型查询
            if (input.AreaType != AreaType.None)
            {
                AreaType country = ((input.AreaType & AreaType.Country) == AreaType.Country) ? AreaType.Country : AreaType.None;
                AreaType province = ((input.AreaType & AreaType.Province) == AreaType.Province) ? AreaType.Province : AreaType.None;
                AreaType municipality = ((input.AreaType & AreaType.Municipality) == AreaType.Municipality) ? AreaType.Municipality : AreaType.None;
                AreaType city = ((input.AreaType & AreaType.City) == AreaType.City) ? AreaType.City : AreaType.None;
                query = query.Where(e => e.AreaType == country || e.AreaType == province || e.AreaType == municipality || e.AreaType == city);
            }

            #endregion

            #region 设置排序规则
            //设置排序方式
            //switch (input.OrderBy)
            //{
            //    case "nameAsc": { query = query.OrderBy(e => e.Name); break; }
            //    case "nameDesc": { query = query.OrderByDescending(e => e.Name); break; }
            //    case "codeAsc": { query = query.OrderBy(e => e.Code); break; }
            //    case "codeDesc": { query = query.OrderByDescending(e => e.Code); break; }
            //    case "dateAsc": { query = query.OrderBy(e => e.UpdateDate); break; }
            //    case "dateDesc": { query = query.OrderByDescending(e => e.UpdateDate); break; }
            //    default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            //}
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "code": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Code) : query.OrderByDescending(e => e.Code); break; }
                case "date": { query = input.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }

            #endregion

            //返回查询结果
            return query;
        }

        /// <summary>
        /// 根据关键字模糊查询区域信息，查询范围有编码、名称、电话区号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<AreaInfo> _SearchByKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return new List<AreaInfo>();
            }
            //return _Dal.Queryable().Where(e => (!String.IsNullOrEmpty(e.Code) && e.Code.Contains(key)) ||
            //    (!String.IsNullOrEmpty(e.Name) && e.Name.Contains(key)) ||
            //    (!String.IsNullOrEmpty(e.NameEn) && e.NameEn.Contains(key)) ||
            //    (!String.IsNullOrEmpty(e.TelephoneCode) && e.TelephoneCode.Contains(key)));
            return null;
        }
        /// <summary>
        /// 根据一段地址模糊查询区域信息，查询范围有编码、名称、电话区号
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<AreaInfo> _SearchByAddress(string address)
        {
            address = address.Trim().ToLower();
            if (String.IsNullOrEmpty(address))
            {
                return new List<AreaInfo>();
            }

            List<AreaInfo> areaList = new List<AreaInfo>();
            //foreach (var area in _Dal.Queryable().ToList())
            //{
            //    if (!String.IsNullOrEmpty(area.Name) && address.Contains(area.Name))
            //    {
            //        areaList.Add(area);
            //        continue;
            //    }
            //    if (!String.IsNullOrEmpty(area.NameEn) && address.Contains(area.NameEn.ToLower()))
            //    {
            //        areaList.Add(area);
            //        continue;
            //    }
            //    if (!String.IsNullOrEmpty(area.Code) && address.Contains(area.Code))
            //    {
            //        areaList.Add(area);
            //        continue;
            //    }
            //}
            return areaList;
        }

        /// <summary>
        /// 组个各种条件，获取命中最多的区域信息
        /// </summary>
        /// <param name="addressCn"></param>
        /// <param name="addressEn"></param>
        /// <param name="telephone"></param>
        /// <param name="code"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        public AreaInfo Single(string addressCn, string addressEn, string telephone, string code, string zip)
        {
            List<AreaInfo> areaList = _SearchByAddress(addressCn);
            areaList.AddRange(_SearchByAddress(addressEn));
            areaList.AddRange(_SearchByKey(telephone));
            areaList.AddRange(_SearchByKey(code));
            areaList.AddRange(_SearchByKey(zip));

            var res = from n in areaList
                      group n by n.Code into g
                      orderby g.Count() descending
                      select g;

            if (res != null)
            {
                var g = res.First();
                if (g != null)
                {
                    return g.FirstOrDefault();
                }
            }
            return new AreaInfo();
        }

        #endregion 
    }
}
