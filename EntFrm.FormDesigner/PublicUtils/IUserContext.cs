using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;

namespace MyFormDesinger
{
    public class IUserContext
    {
        public static bool SetParamValueByName(string sName, string sValue, string sType = "2")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 100;
                    SysParams info = null;

                    SysParamsBLL infoBLL = new SysParamsBLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1,10, " KeyName='" + sName + "' And KeyType='" + sType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sKeyValue = sValue;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;

                        return infoBLL.UpdateRecord(info);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New8ByteGuid();
                        info.sKeyName = sName;
                        info.sKeyValue = sValue;
                        info.sKeyType = sType;

                        info.sAddOptor = sSuNo;
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";

                        info.sAppCode = IPublicHelper.Get_AppCode() + ";";

                        return infoBLL.AddNewRecord(info);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public static string GetParamValueByName(string sName, string sType = "2")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;

                    SysParamsBLL infoBLL = new SysParamsBLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1,10, " KeyName='" + sName + "' And KeyType='" + sType + "' ");
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        return infoColl.GetFirstOne().sKeyValue;
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return "";
        }
    }
}