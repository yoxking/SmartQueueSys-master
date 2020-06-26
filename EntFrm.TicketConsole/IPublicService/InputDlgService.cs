using EntFrm.Business.Model;
using EntFrm.TicketConsole.MyInputDialog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public class InputDlgService
    {
        private volatile static InputDlgService _instance = null;
        private static readonly object lockHelper = new object();

        public static InputDlgService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new InputDlgService();
                }
            }
            return _instance;
        }

        private InputDlgService() { }

        public bool GetRUserDataByInput(string sServiceNo,ref string sNewTicketNo,ref string sRUserData)
        {
            bool bResult = false;
            bool bActive = true;
            string sMessage = "";
            RUserModel ruserModel = new RUserModel();
            ruserModel.UserName = "张三";
            ruserModel.UserSex = "男";
            ruserModel.UserAge = "20";
            ruserModel.Address = "";
            ruserModel.Telephone = "18880888088";
            ruserModel.IdcardNo = "500101199001012213";
            ruserModel.RicardNo = "";
            ruserModel.Summary = "";

            ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(sServiceNo));
            if (info.iIsShowDialog == 1)
            {

                bActive = false;
                switch (info.sShowDialogName)
                {
                    //刷身份证（姓名）
                    case "ScanCardDialog1":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = card.sCnName;
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                }
                            }
                        }
                        break;
                    //刷身份证（票号）
                    case "ScanCardDialog2":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = "";
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                }
                            }
                        }
                        break;
                    //刷身份证+手动输入(姓名)
                    case "ScanIdCardDialog1":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = card.sCnName;
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bResult = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, card.sIdCardNo, ref sMessage);
                                }
                                else
                                {
                                    NmBoardDialog ndlg = new NmBoardDialog();
                                    if (ndlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sNewTicketNo = ndlg.sStrName;
                                        ruserModel.UserName = ndlg.sStrName;
                                        ruserModel.IdcardNo = ndlg.sStrInput;
                                        ruserModel.UserSex = "先生";
                                        ruserModel.Summary = ndlg.sStrInput;

                                        bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                    }
                                }
                            }
                        }
                        break;
                    //刷身份证+手动输入(票号)
                    case "ScanIdCardDialog2":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = "";
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bResult = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, card.sIdCardNo, ref sMessage);
                                }
                                else
                                {
                                    NmBoardDialog ndlg = new NmBoardDialog();
                                    if (ndlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sNewTicketNo = "";
                                        ruserModel.UserName = ndlg.sStrName;
                                        ruserModel.IdcardNo = ndlg.sStrInput;
                                        ruserModel.UserSex = "先生";
                                        ruserModel.Summary = ndlg.sStrInput;

                                        bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                    }
                                }
                            }
                        }
                        break;
                    //刷身份证且车牌输入(车牌号)
                    case "InputPlateDialog1":
                        {
                            PlateInputDialog dlg = new PlateInputDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                sNewTicketNo = dlg.sStrInput;
                                ruserModel.UserName = "";
                                ruserModel.IdcardNo = "";
                                ruserModel.UserSex = "先生";
                                ruserModel.Summary = dlg.sStrInput;

                                bActive = true;

                            }
                        }
                        break;
                    //刷身份证且车牌输入(票号)
                    case "InputPlateDialog2":
                        {
                            PlateInputDialog dlg = new PlateInputDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                sNewTicketNo = "";
                                ruserModel.UserName = "";
                                ruserModel.IdcardNo = "";
                                ruserModel.UserSex = "先生";
                                ruserModel.Summary = dlg.sStrInput;

                                bActive = true;

                            }
                        }
                        break;
                    //刷身份证+输入电话(姓名)
                    case "InputPhoneDialog1":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = card.sCnName;
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);

                                    if (bActive)
                                    {
                                        InputPhoneDialog telDlg = new InputPhoneDialog();
                                        if (telDlg.ShowDialog() == DialogResult.OK)
                                        {
                                            ruserModel.Telephone = telDlg.sStrInput;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    //刷身份证+输入电话(票号)
                    case "InputPhoneDialog2":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = "";
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);

                                    if (bActive)
                                    {
                                        InputPhoneDialog telDlg = new InputPhoneDialog();
                                        if (telDlg.ShowDialog() == DialogResult.OK)
                                        {
                                            ruserModel.Telephone = telDlg.sStrInput;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    //输入身份证+姓名(姓名)
                    case "InputIdcardDialog1":
                        {
                            NmBoardDialog dlg = new NmBoardDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                sNewTicketNo = dlg.sStrName;
                                ruserModel.UserName = dlg.sStrName;
                                ruserModel.IdcardNo = dlg.sStrInput;
                                ruserModel.UserSex = "先生";
                                ruserModel.Summary = dlg.sStrInput;

                                bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                            }
                        }
                        break;
                    //输入身份证+姓名(票号)
                    case "InputIdcardDialog2":
                        {
                            NmBoardDialog dlg = new NmBoardDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                sNewTicketNo = "";
                                ruserModel.UserName = dlg.sStrName;
                                ruserModel.IdcardNo = dlg.sStrInput;
                                ruserModel.UserSex = "先生";
                                ruserModel.Summary = dlg.sStrInput;

                                bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                            }
                        }
                        break;
                    //刷身份证+车牌输入(车牌号)
                    case "PlateInputDialog1":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = card.sCnName;
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, card.sIdCardNo, ref sMessage);
                                }
                                else
                                {
                                    FullBoardDialog ndlg = new FullBoardDialog();
                                    if (ndlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sNewTicketNo = ndlg.sStrInput;
                                        ruserModel.UserName = "";
                                        ruserModel.IdcardNo = "";
                                        ruserModel.UserSex = "";
                                        ruserModel.Summary = ndlg.sStrInput;

                                        bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                    }
                                }
                            }
                        }
                        break;
                    //刷身份证+车牌输入(票号)
                    case "PlateInputDialog2":
                        {
                            ScanCardDialog dlg = new ScanCardDialog();
                            dlg.bInputFlag = false;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {
                                    IdCardModel card = new IdCardModel(dlg.sStrInput);

                                    sNewTicketNo = card.sCnName;
                                    ruserModel.UserName = card.sCnName;
                                    ruserModel.IdcardNo = card.sIdCardNo;
                                    ruserModel.UserSex = card.sSex.Equals("女") ? "女士" : "先生";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, card.sIdCardNo, ref sMessage);
                                }
                                else
                                {
                                    FullBoardDialog ndlg = new FullBoardDialog();
                                    if (ndlg.ShowDialog() == DialogResult.OK)
                                    {
                                        sNewTicketNo = "";
                                        ruserModel.UserName = "";
                                        ruserModel.IdcardNo = "";
                                        ruserModel.UserSex = "";
                                        ruserModel.Summary = ndlg.sStrInput;

                                        bActive = CommonService.CreateInstance().IsActiveCardNo(sServiceNo, ruserModel.IdcardNo, ref sMessage);
                                    }
                                }
                            }
                        }
                        break;
                    //刷社保卡(票号)
                    case "ScanDecardDialog":
                        {
                            ScanDecardDialog dlg = new ScanDecardDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {

                                    sNewTicketNo = "";
                                    ruserModel.UserName = dlg.sStrInput;
                                    ruserModel.IdcardNo = dlg.sStrInput;
                                    ruserModel.UserSex = "";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = true;
                                }
                            }
                        }
                        break;
                    //扫条形码(票号)
                    case "ScanBarcodeDialog":
                        {
                            ScanBcodeDialog dlg = new ScanBcodeDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.sStrInput))
                                {

                                    sNewTicketNo = "";
                                    ruserModel.UserName = dlg.sStrInput;
                                    ruserModel.IdcardNo = dlg.sStrInput;
                                    ruserModel.UserSex = "";
                                    ruserModel.Summary = dlg.sStrInput;

                                    bActive = true;
                                }
                            }
                        }
                        break;
                    //手写输入(票号)
                    case "DoHandInputDialog":
                        {
                            HandInputDialog dlg = new HandInputDialog();
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(dlg.CnName))
                                {

                                    sNewTicketNo = "";
                                    ruserModel.UserName = dlg.CnName;
                                    ruserModel.IdcardNo = dlg.IdCard;
                                    ruserModel.Telephone = dlg.Telphone;
                                    ruserModel.Summary = dlg.From;

                                    bActive = true;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
             

            if (bActive)
            {
                sRUserData = JsonConvert.SerializeObject(ruserModel);
                bResult = true;
            }
            else
            {
                frmMssgForm msgDlg = new frmMssgForm("友情提示", sMessage, true);
                msgDlg.ShowDialog();
            }

            return bResult;
        }
    }
}
