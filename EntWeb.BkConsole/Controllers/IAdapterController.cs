using DotNetty.Common.Utilities;
using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntWeb.BkConsole.Common;
using EntWeb.BkConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class IAdapterController : Controller
    {
        private string getMaskName(string name, string maskFlag)
        {
            if (maskFlag.Equals("1") && name.Length > 2)
            {
                name = StringHelper.MaskString(name, 1, 1, '*');
            }
            else if (maskFlag.Equals("1"))
            {
                name = StringHelper.MaskString(name, 1, 0, '*');
            }

            return name;
        }

        #region 基本信息
        public string getCurrentDatetime()
        {
            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

            return JsonConvert.SerializeObject(codeData);
        }
        public string getCurrentDatetime2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy年MM月dd日<br />"));
            sb.Append(CalendarHelper.GetCNDayOfWeek(DateTime.Now));
            sb.Append(DateTime.Now.ToString(" HH:mm:ss"));

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = sb.ToString();

            return JsonConvert.SerializeObject(codeData);
        }

        public string getBranchNameByBranchId(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                BranchInfoBLL counterBoss = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                codeData.data = counterBoss.GetRecordNameByNo(branchNo);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getCounterNameByCounterId(string branchNo, string counterNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";

            try
            {
                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                codeData.data = counterBoss.GetRecordNameByNo(counterNo);

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #endregion 

    }
}