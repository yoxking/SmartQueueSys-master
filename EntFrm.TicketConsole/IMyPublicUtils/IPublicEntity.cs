using EntFrm.Framework.Utility;
using System.Collections.Generic;

namespace EntFrm.TicketConsole
{
    public class IPublicEntity
    {

        public static List<ItemObject> getAdminRegion()
        {
            List<ItemObject> regionList = new List<ItemObject>();

            regionList.Add(new ItemObject("重庆主城", "渝中区"));
            regionList.Add(new ItemObject("重庆主城", "江北区"));
            regionList.Add(new ItemObject("重庆主城", "渝北区"));
            regionList.Add(new ItemObject("重庆主城", "南岸区"));
            regionList.Add(new ItemObject("重庆主城", "九龙坡区"));
            regionList.Add(new ItemObject("重庆主城", "沙坪坝区"));
            regionList.Add(new ItemObject("重庆主城", "大渡口区"));
            regionList.Add(new ItemObject("重庆主城", "北碚区"));
            regionList.Add(new ItemObject("重庆主城", "巴南区"));
            regionList.Add(new ItemObject("重庆区县", "万州区"));
            regionList.Add(new ItemObject("重庆区县", "涪陵区"));
            regionList.Add(new ItemObject("重庆区县", "黔江区"));
            regionList.Add(new ItemObject("重庆区县", "长寿区"));
            regionList.Add(new ItemObject("重庆区县", "江津区"));
            regionList.Add(new ItemObject("重庆区县", "永川区"));
            regionList.Add(new ItemObject("重庆区县", "合川区"));
            regionList.Add(new ItemObject("重庆区县", "南川区"));
            regionList.Add(new ItemObject("重庆区县", "綦江区"));
            regionList.Add(new ItemObject("重庆区县", "大足区"));
            regionList.Add(new ItemObject("重庆区县", "潼南区"));
            regionList.Add(new ItemObject("重庆区县", "铜梁区"));
            regionList.Add(new ItemObject("重庆区县", "荣昌区"));
            regionList.Add(new ItemObject("重庆区县", "璧山区"));
            regionList.Add(new ItemObject("重庆区县", "梁平区"));
            regionList.Add(new ItemObject("重庆区县", "城口县"));
            regionList.Add(new ItemObject("重庆区县", "丰都县"));
            regionList.Add(new ItemObject("重庆区县", "垫江县"));
            regionList.Add(new ItemObject("重庆区县", "武隆区"));
            regionList.Add(new ItemObject("重庆区县", "忠县"));
            regionList.Add(new ItemObject("重庆区县", "开州区"));
            regionList.Add(new ItemObject("重庆区县", "云阳县"));
            regionList.Add(new ItemObject("重庆区县", "奉节县"));
            regionList.Add(new ItemObject("重庆区县", "巫山县"));
            regionList.Add(new ItemObject("重庆区县", "巫溪县"));
            regionList.Add(new ItemObject("重庆区县", "石柱县"));
            regionList.Add(new ItemObject("重庆区县", "秀山县"));
            regionList.Add(new ItemObject("重庆区县", "酉阳县"));
            regionList.Add(new ItemObject("重庆区县", "彭水县"));
            regionList.Add(new ItemObject("其他省份", "其他省份"));
            regionList.Add(new ItemObject("国外", "国外"));

            return regionList;
        }
    }
}
