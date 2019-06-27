using System;
using System.Collections.Generic;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
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
        public AreaService(AreaDAL dal) : base(dal)
        {

        }
        #endregion

        #region 数据初始化
        private readonly string AmapDistrictApiUrl = "http://restapi.amap.com/v3/config/district?key=26783229f3c043e9281864044f1154e0&subdistrict=3&extensions=base";
        /// <summary>
        /// 初始化区域信息
        /// </summary>
        public void DefaultInit()
        {
            // 通过高德API获取城市信息
            AmapDistricts amap = LoadApi.Get<AmapDistricts>(AmapDistrictApiUrl);
            // 删除已有城市信息
            _Dal.Drop();
            // 插入获取的城市
            foreach (var counAmap in amap.Districts)
            {
                AreaInfo country = CreateDist(counAmap);
                foreach (var provAmap in counAmap.Districts)
                {
                    AreaInfo province = CreateDist(provAmap, country);
                    foreach (var cityAmap in provAmap.Districts)
                    {
                        AreaInfo city = CreateDist(cityAmap, province);
                        foreach (var distAmap in cityAmap.Districts)
                        {
                            AreaInfo district = CreateDist(distAmap, city);
                            foreach (var streAmap in distAmap.Districts)
                            {
                                AreaInfo street = CreateDist(streAmap, district);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 创建一个区域信息
        /// </summary>
        /// <param name="dist">区域信息</param>
        /// <param name="superior">上级区域信息</param>
        /// <returns>区域ID</returns>
        public AreaInfo CreateDist(DistrictInfo dist, AreaInfo superior = null)
        {
            AreaInfo areaInfo = new AreaInfo
            {
                Name = dist.Name,
                ShortName = GetShortName(dist.Name),
                Code = dist.CityCodeString,
                Zip = dist.Adcode,
                TelephoneCode = string.Empty,
                AreaType = GetAreaType(dist.Level),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            areaInfo.NameEn = GetNameEn(areaInfo);
            if (superior != null)
            {
                areaInfo.SuperiorId = superior.IdString;
                areaInfo.SuperiorCode = superior.Code;
                areaInfo.SuperiorName = superior.Name;
                areaInfo.SuperiorIds = superior.SuperiorIds.ToJson().JsonToObject<List<string>>();
                areaInfo.SuperiorIds.Add(superior.Id.ToString());
                areaInfo.SuperiorNames = superior.SuperiorNames.ToJson().JsonToObject<List<string>>();
                areaInfo.SuperiorNames.Add(superior.Name.ToString());
            }
            _Dal.InsertObject(areaInfo);
            return areaInfo;
        }
        /// <summary>
        /// 获取地址的缩写
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetShortName(string name)
        {
            name = name.Replace("县", "").Replace("市", "").Replace("省", "").Replace("特别行政区", "").Replace("自治州", "").Replace("自治区", "").Replace("自治县", "").Replace("地区", "").Replace("林区", "").Replace("中华人民共和国", "中国");
            name = name.Replace("维吾尔", "").Replace("蒙古族", "").Replace("藏族", "").Replace("布依族", "").Replace("苗族", "").Replace("黎族", "").Replace("回族", "").Replace("彝族", "")
                .Replace("傣族", "").Replace("景颇族", "").Replace("侗族", "").Replace("土家族", "").Replace("壮族", "").Replace("羌族", "").Replace("朝鲜族", "").Replace("哈尼族", "")
                .Replace("白族", "").Replace("僳族", "").Replace("内蒙古", "内蒙").Replace("蒙古", "");
            return name;
        }
        /// <summary>
        /// 获取区域的枚举信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private AreaType GetAreaType(string type)
        {
            switch (type.ToLower())
            {
                case "country": return AreaType.Country;        // country: 国家
                case "province": return AreaType.Province;      // province:省份（直辖市会在province和city显示）
                case "city": return AreaType.City;              // city:市（直辖市会在province和city显示）
                case "district": return AreaType.District;      // district:区县
                case "street": return AreaType.Street;          // street:街道
                default: return AreaType.None;
            }
        }

        /// <summary>
        /// 获取地区的英文名称
        /// </summary>
        /// <param name="areaInfo"></param>
        /// <returns></returns>
        private string GetNameEn(AreaInfo areaInfo)
        {
            List<string> result = new List<string>();
            foreach (char sinogram in areaInfo.ShortName.ToCharArray())
            {
                result.Add(FirstLetterToUpper(GetPinYin(sinogram)));
            }
            return string.Join("", result);
        }

        /// <summary>
        /// 获取汉字的拼音
        /// </summary>
        /// <param name="sinogram"></param>
        /// <returns></returns>
        private string GetPinYin(char sinogram)
        {
            Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(sinogram);
            string pinyin = cc.Pinyins.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(pinyin))
            {
                return "";
            }
            string last = pinyin.Substring(pinyin.Length - 1);
            if (last.Equals("1") || last.Equals("2") || last.Equals("3") || last.Equals("4"))
            {
                pinyin = pinyin.Substring(0, pinyin.Length - 1);
            }

            return pinyin;
        }

        /// <summary>
        /// 设置字符串首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string FirstLetterToUpper(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1).ToLower();
            return str.ToUpper();
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
                query = query.Where(e => !string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
            }
            // 按编码模糊查询
            if (!string.IsNullOrWhiteSpace(input.Code))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.Code) && e.Code.Contains(input.Code));
            }
            // 按上级区域Id查询
            if (!string.IsNullOrWhiteSpace(input.SuperiorId))
            {
                query = query.Where(e => e.SuperiorId == input.SuperiorId);
            }
            // 按上级区域Code查询
            if (!string.IsNullOrWhiteSpace(input.SuperiorCode))
            {
                query = query.Where(e => e.SuperiorCode == input.SuperiorCode);
            }
            // 按上级区域名称模糊查询
            if (!string.IsNullOrWhiteSpace(input.SuperiorName))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.SuperiorName) && e.SuperiorName.Contains(input.SuperiorName));
            }
            // 按区域类型查询
            if (input.AreaType != AreaType.None)
            {
                AreaType country = ((input.AreaType & AreaType.Country) == AreaType.Country) ? AreaType.Country : AreaType.None;
                AreaType province = ((input.AreaType & AreaType.Province) == AreaType.Province) ? AreaType.Province : AreaType.None;
                AreaType city = ((input.AreaType & AreaType.City) == AreaType.City) ? AreaType.City : AreaType.None;
                query = query.Where(e => e.AreaType == country || e.AreaType == province || e.AreaType == city);
            }

            #endregion

            #region 设置排序规则
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
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<AreaInfo>();
            }
            //return _Dal.Queryable().Where(e => (!string.IsNullOrEmpty(e.Code) && e.Code.Contains(key)) ||
            //    (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(key)) ||
            //    (!string.IsNullOrEmpty(e.NameEn) && e.NameEn.Contains(key)) ||
            //    (!string.IsNullOrEmpty(e.TelephoneCode) && e.TelephoneCode.Contains(key)));
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
            if (string.IsNullOrEmpty(address))
            {
                return new List<AreaInfo>();
            }

            List<AreaInfo> areaList = new List<AreaInfo>();
            //foreach (var area in _Dal.Queryable().ToList())
            //{
            //    if (!string.IsNullOrEmpty(area.Name) && address.Contains(area.Name))
            //    {
            //        areaList.Add(area);
            //        continue;
            //    }
            //    if (!string.IsNullOrEmpty(area.NameEn) && address.Contains(area.NameEn.ToLower()))
            //    {
            //        areaList.Add(area);
            //        continue;
            //    }
            //    if (!string.IsNullOrEmpty(area.Code) && address.Contains(area.Code))
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
