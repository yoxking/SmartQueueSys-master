﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmLedDispBusiness : Form
    {
        private LEDDisplayBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        private TextBox rTextBox;

        public frmLedDispBusiness()
        {
            InitializeComponent();
        }

        private void frmLedDispBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0; 
            InfoList.MultiSelect = false;

            dpFontAlign.SelectedIndex = 0;
            dpSerialPort.SelectedIndex = 0;
            dpLedModel.SelectedIndex = 0;
            rTextBox = txtDisplayFormat;

            DoBindVariableSource(); 
            DoBindDataList();
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            LEDDisplayCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0)
           {
               sCurrentNo = InfoColl[0].sDisplayNo;
               foreach(LEDDisplay led in InfoColl)
               {
                   led.sCounterNos = IPublicHelper.GetCounterNameByNo(led.sCounterNos);
               }
           }

           InfoList.AutoGenerateColumns = false;
           InfoList.DataSource = InfoColl;
        }

        private void DoBindVariableSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

            dpVariables.DataSource = varsList;
            dpVariables.ValueMember = "Value";
            dpVariables.DisplayMember = "Name";
        }  

        private void DoRefreshForm()
        {
            try
            {
                LEDDisplay info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    sCurrentNo = info.sDisplayNo;

                    txtDisplayName.Text = info.sDisplayName;
                    dpLedModel.SelectedItem = info.sLedModel;
                    txtPhyAddr.Text = info.iPhyAddr.ToString();

                    dpSerialPort.SelectedItem = info.sSerialPort;
                    txtBaudrate.Text = info.iBaudrate.ToString();
                    txtIpAddress.Text = info.sIpAddress;
                    txtLocalPort.Text = info.sLocalPort;

                    if (info.sProtocol.Equals("NET"))
                    {
                        ckNetProtocol.Checked = true;
                        ckComProtocol.Checked = false;
                    }
                    else
                    {
                        ckComProtocol.Checked = true;
                        ckNetProtocol.Checked = false;
                    }

                    string[] sparam = info.sParamFormat.Split(';');
                    if (sparam.Length == 8)
                    {
                        txtScreenWidth.Text = sparam[0];
                        txtScreenHeight.Text = sparam[1];
                        txtPosX.Text = sparam[2];
                        txtPosY.Text = sparam[3];
                        txtWidth.Text = sparam[4];
                        txtHeight.Text = sparam[5];
                        txtFontSize.Text = sparam[6];
                        dpFontAlign.SelectedIndex = int.Parse(sparam[7]);
                    }
                    else
                    {
                        txtScreenWidth.Text = "128";
                        txtScreenHeight.Text = "64";
                        txtPosX.Text = "0";
                        txtPosY.Text = "0";
                        txtWidth.Text = "128";
                        txtHeight.Text = "64";
                        txtFontSize.Text = "12";
                        dpFontAlign.SelectedIndex = 1;
                    }

                    txtDisplayFormat.Text = info.sDisplayFormat;
                    txtDisplayLength.Text = info.iDisplayLength.ToString();
                    txtPowerOnTip.Text = info.sPowerOnTip;
                    txtInServiceTip.Text = info.sInServiceTip;
                    txtOnPauseTip.Text = info.sOnPauseTip;
                    txtTimeoutTip.Text = info.sTimeoutTip;
                    txtTimeoutSec.Text = info.iTimeoutSec.ToString();

                }
                else
                {
                    txtDisplayName.Text = "LED屏1";
                }
                SetFocusedColumn();

                updateType = UpdateType.Upt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void SetFocusedColumn()
        {
            try
            {
                this.InfoList.Rows[iSelectedRow].Selected = true;
                this.InfoList.CurrentCell = this.InfoList.Rows[iSelectedRow].Cells[0];
            }
            catch (Exception ex)
            { }
        }

        private string GetFocusedColumnValue(string columnName)
        {
            try
            {
                string sResult = "";

                if (this.InfoList.SelectedRows.Count > 0)
                {
                    sResult = this.InfoList.SelectedRows[0].Cells[columnName].Value.ToString();
                    iSelectedRow = this.InfoList.SelectedRows[0].Index;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void InfoList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sNo = GetFocusedColumnValue("DisplayNo");
            if (!sNo.Equals(sCurrentNo))
            {
                sCurrentNo = sNo;
                DoRefreshForm();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' ") + 1;

                txtDisplayName.Text = "LED屏" + count;
                txtPhyAddr.Text = "" + count; 

                updateType = UpdateType.Add;
                btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        } 

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            { 
                if (MessageBox.Show("您确定要删除所选信息?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sNo = GetFocusedColumnValue("DisplayNo");
                    if (myBoss.HardDeleteRecord(sNo))
                    {
                        DoBindDataList();
                        DoRefreshForm();
                    }
                    else
                    {
                        MessageBox.Show("删除操作失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string sNo = GetFocusedColumnValue("DisplayNo");
        }

        private void tbTxtFormat_Selected(object sender, TabControlEventArgs e)
        {
            string currentName = tbTxtFormat.SelectedTab.Name;

            switch (currentName)
            {
                case "tabDisplayFormat":
                    rTextBox = txtDisplayFormat;
                    break;
                case "tabPowerOnTip":
                    rTextBox = txtPowerOnTip;
                    break;
                case "tabInServiceTip":
                    rTextBox = txtInServiceTip;
                    break;
                case "tabOnPauseTip":
                    rTextBox = txtOnPauseTip;
                    break;
                case "tabTimeoutTip":
                    rTextBox = txtTimeoutTip;
                    break;
                default:
                    rTextBox = txtDisplayFormat; break;
            } 
        }

        private void btnAddVar_Click(object sender, EventArgs e)
        { 
                string s = rTextBox.Text;
            int idx = rTextBox.SelectionStart;
            rTextBox.Text = s.Insert(idx, dpVariables.SelectedValue.ToString());

        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    LEDDisplay info = new LEDDisplay();
                    info.sDisplayNo = CommonHelper.Get_New12ByteGuid();

                    info.sDisplayName = txtDisplayName.Text.Trim();
                    info.sCounterNos = "";
                    info.sLedModel = dpLedModel.SelectedItem.ToString();
                    info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                    info.sProtocol = ckComProtocol.Checked ? "COM" : "NET";
                    info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                    info.iBaudrate = int.Parse(txtBaudrate.Text.Trim());
                    info.sIpAddress = txtIpAddress.Text.Trim();
                    info.sLocalPort = txtLocalPort.Text.Trim();
                    info.sParamFormat = txtScreenWidth.Text.Trim() + ";" + txtScreenHeight.Text.Trim() + ";" + txtPosX.Text.Trim() + ";" + txtPosY.Text.Trim() + ";" + txtWidth.Text.Trim() + ";" + txtHeight.Text.Trim() + ";" + txtFontSize.Text.Trim() + ";" + dpFontAlign.SelectedIndex;
                    info.iTimeoutSec = int.Parse(txtTimeoutSec.Text.Trim());
                    info.iDisplayLength = int.Parse(txtDisplayLength.Text.Trim());

                    info.sDisplayFormat = txtDisplayFormat.Text;
                    info.sPowerOnTip = txtPowerOnTip.Text;
                    info.sInServiceTip = txtInServiceTip.Text;
                    info.sOnPauseTip = txtOnPauseTip.Text;
                    info.sTimeoutTip = txtTimeoutTip.Text;

                    info.iUpdateFlag = 0;
                    info.dUpdateTime = DateTime.Now;
                    info.sBranchNo = IUserContext.GetBranchNo();

                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(info))
                    {
                        if (info.sLedModel.Equals("Eq2013"))
                        {
                            setEq2013_IniFile();
                        }
                        else if (info.sLedModel.Equals("Eq2023"))
                        {
                            setEq2023_IniFile();
                        }

                        //MessageBox.Show("操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                {
                    LEDDisplay info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        info.sDisplayName = txtDisplayName.Text.Trim();
                        info.sLedModel = dpLedModel.SelectedItem.ToString();
                        info.iPhyAddr = int.Parse(txtPhyAddr.Text.Trim());
                        info.sProtocol = ckComProtocol.Checked ? "COM" : "NET";
                        info.sSerialPort = dpSerialPort.SelectedItem.ToString();
                        info.iBaudrate = int.Parse(txtBaudrate.Text.Trim());
                        info.sIpAddress = txtIpAddress.Text.Trim();
                        info.sLocalPort = txtLocalPort.Text.Trim();
                        info.sParamFormat = txtScreenWidth.Text.Trim() + ";" + txtScreenHeight.Text.Trim() + ";" + txtPosX.Text.Trim() + ";" + txtPosY.Text.Trim() + ";" + txtWidth.Text.Trim() + ";" + txtHeight.Text.Trim() + ";" + txtFontSize.Text.Trim() + ";" + dpFontAlign.SelectedIndex;
                        info.iTimeoutSec = int.Parse(txtTimeoutSec.Text.Trim());
                        info.iDisplayLength = int.Parse(txtDisplayLength.Text.Trim());

                        info.sDisplayFormat = txtDisplayFormat.Text;
                        info.sPowerOnTip = txtPowerOnTip.Text;
                        info.sInServiceTip = txtInServiceTip.Text;
                        info.sOnPauseTip = txtOnPauseTip.Text;
                        info.sTimeoutTip = txtTimeoutTip.Text;
                         
                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(info))
                        {
                            if (info.sLedModel.Equals("Eq2013"))
                            {
                                setEq2013_IniFile();
                            }

                            MessageBox.Show("操作成功!");
                            DoBindDataList();
                            DoRefreshForm();
                            return;
                        }
                    }

                    MessageBox.Show("更新操作失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void setEq2013_IniFile()
        {
            try
            {
                int phyAddr = int.Parse(txtPhyAddr.Text.Trim());
                string protocol = ckNetProtocol.Checked ? "NET" : "COM";
                string baudrate = txtBaudrate.Text.Trim();
                string serialPort = dpSerialPort.SelectedItem.ToString();
                string netPort = txtLocalPort.Text.Trim();
                string ipAddress = txtIpAddress.Text.Trim();
                string screenWidth = txtScreenWidth.Text.Trim();
                string screenHeight = txtScreenHeight.Text.Trim();

                string iniFile = System.Windows.Forms.Application.StartupPath + "\\EQ2008_Dll_Set.ini";
                string addstr = "地址：" + (phyAddr - 1);
                 
                IniFileHelper.SetValue(addstr, "CardType", "21", iniFile);
                IniFileHelper.SetValue(addstr, "CardAddress", (phyAddr - 1) + "", iniFile);

                if (protocol.Equals("NET"))
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "1", iniFile);
                }
                else
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "0", iniFile);
                }

                IniFileHelper.SetValue(addstr, "SerialBaud", baudrate, iniFile);
                IniFileHelper.SetValue(addstr, "SerialNum", serialPort.Substring(3), iniFile);
                IniFileHelper.SetValue(addstr, "NetPort", netPort, iniFile);
                string[] ipList = ipAddress.Split('.');
                if (ipList.Length == 4)
                {
                    IniFileHelper.SetValue(addstr, "IpAddress0", ipList[0], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress1", ipList[1], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress2", ipList[2], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress3", ipList[3], iniFile);
                }
                IniFileHelper.SetValue(addstr, "ColorStyle", "0", iniFile);

                IniFileHelper.SetValue(addstr, "ScreemWidth", screenWidth, iniFile);
                IniFileHelper.SetValue(addstr, "ScreemHeight", screenHeight, iniFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void setEq2023_IniFile()
        {
            try
            {
                int phyAddr = int.Parse(txtPhyAddr.Text.Trim());
                string protocol = ckNetProtocol.Checked ? "NET" : "COM";
                string baudrate = txtBaudrate.Text.Trim();
                string serialPort = dpSerialPort.SelectedItem.ToString();
                string netPort = txtLocalPort.Text.Trim();
                string ipAddress = txtIpAddress.Text.Trim();
                string screenWidth = txtScreenWidth.Text.Trim();
                string screenHeight = txtScreenHeight.Text.Trim();

                string iniFile = System.Windows.Forms.Application.StartupPath + "\\EQ2008_Dll_Set.ini";
                string addstr = "地址：" + (phyAddr - 1);

                IniFileHelper.SetValue(addstr, "CardType", "22", iniFile); 
                IniFileHelper.SetValue(addstr, "CardAddress", (phyAddr - 1) + "", iniFile);

                if (protocol.Equals("NET"))
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "1", iniFile);
                }
                else
                {
                    IniFileHelper.SetValue(addstr, "CommunicationMode", "0", iniFile);
                }

                IniFileHelper.SetValue(addstr, "SerialBaud", baudrate, iniFile);
                IniFileHelper.SetValue(addstr, "SerialNum", serialPort.Substring(3), iniFile);
                IniFileHelper.SetValue(addstr, "NetPort", netPort, iniFile);
                string[] ipList = ipAddress.Split('.');
                if (ipList.Length == 4)
                {
                    IniFileHelper.SetValue(addstr, "IpAddress0", ipList[0], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress1", ipList[1], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress2", ipList[2], iniFile);
                    IniFileHelper.SetValue(addstr, "IpAddress3", ipList[3], iniFile);
                }
                IniFileHelper.SetValue(addstr, "ColorStyle", "1", iniFile); 

                IniFileHelper.SetValue(addstr, "ScreemWidth", screenWidth, iniFile);
                IniFileHelper.SetValue(addstr, "ScreemHeight", screenHeight, iniFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(dpSerialPort.SelectedItem.ToString().Substring(3));
                int addr = int.Parse(txtPhyAddr.Text.Trim());
                string  sText = rTextBox.Text;

                if (dpLedModel.SelectedItem.ToString().Equals("Eq2013"))
                {
                    //setEq2023_IniFile();
                    Eq2008LedModel.SendDatafun(int.Parse(txtPhyAddr.Text.Trim()), sText, int.Parse(txtPosX.Text.Trim()), int.Parse(txtPosY.Text.Trim()), int.Parse(txtWidth.Text.Trim()), int.Parse(txtHeight.Text.Trim()),int.Parse(txtFontSize.Text.Trim()),dpFontAlign.SelectedIndex);

                } 
                else if(dpLedModel.SelectedItem.ToString().Equals("Hzhx13"))
                {
                    int showMode = 0;
                    if(sText.Length>int.Parse(txtDisplayLength.Text.Trim()))
                    {
                        showMode = 1;
                    }
                    Pdc101LedDisplay.SendDatafun(port, 9600, addr, sText, 51, showMode, 3, 3);
                }
                else if (dpLedModel.SelectedItem.ToString().Equals("Pdc101"))
                {
                    //Pdc102LedDisplay.SendDataText(port, 9600, addr, sText);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认对话框", "您确定要批量修改？", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";

                LEDDisplayCollections infoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (LEDDisplay info in infoColl)
                    {

                        info.sParamFormat = txtPosX.Text.Trim() + ";" + txtPosY.Text.Trim() + ";" + txtWidth.Text.Trim() + ";" + txtHeight.Text.Trim() + ";" + txtFontSize.Text.Trim() + ";" + dpFontAlign.SelectedIndex;

                        info.iDisplayLength = int.Parse(txtDisplayLength.Text.Trim());
                        info.sDisplayFormat = txtDisplayFormat.Text;
                        info.sPowerOnTip = txtPowerOnTip.Text;
                        info.sInServiceTip = txtInServiceTip.Text;
                        info.sOnPauseTip = txtOnPauseTip.Text;
                        info.sTimeoutTip = txtTimeoutTip.Text;

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        myBoss.UpdateRecord(info);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ckComProtocol_CheckBoxClicked(object sender)
        {
            if (ckComProtocol.Checked)
            {
                ckNetProtocol.Checked = false;
            }
            else
            {
                ckNetProtocol.Checked = true;
            }
        }

        private void ckNetProtocol_CheckBoxClicked(object sender)
        {
            if (ckNetProtocol.Checked)
            {
                ckComProtocol.Checked = false;
            }
            else
            {
                ckComProtocol.Checked = true;
            }
        }
        
    }
}
