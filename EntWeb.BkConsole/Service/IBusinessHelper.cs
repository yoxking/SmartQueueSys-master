using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Service
{
    public class IBusinessHelper
    {
        public static string InsertRUserInfo(string sRUserNo,string sRUserName,string sRUserSex,string sTelephone,string sIdNo,string sRemark)
        {
            try
            {
                int count = 0; 
                 
                    RUsersInfoBLL infoBoss = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RUsersInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + sIdNo + "' ");
                    RUsersInfo info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    info.sCnName = sRUserName;
                    info.iSex = sRUserSex.Equals("男") ? 1 : 0;
                    info.sTelphone = sTelephone;
                    info.sSummary = sRemark;

                    info.dModDate = DateTime.Now;

                    if (infoBoss.UpdateRecord(info))
                    {
                        return info.sRUserNo;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(sRUserNo))
                    {
                        sRUserNo = CommonHelper.Get_New12ByteGuid();
                    }
                    info = new RUsersInfo();

                    info.sRUserNo = sRUserNo;
                    info.sCnName = sRUserName;
                    info.sEnName = "";
                    info.iAge = 0;
                    info.iSex = sRUserSex.Equals("男")?1:0;
                    info.sNation = "汉";
                    info.iCardType = 1;
                    info.sIdCardNo = sIdNo;
                    info.sRiCardNo = "";
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = sTelephone;
                    info.sHeadPhoto = "";
                    info.sSummary = sRemark;

                    info.sBranchNo = "";
                    info.sComments = "";
                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBoss.AddNewRecord(info))
                    {
                        return sRUserNo;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}