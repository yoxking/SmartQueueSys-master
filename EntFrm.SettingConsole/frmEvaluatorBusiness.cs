using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmEvaluatorBusiness : Form
    {
        private EvaluatorInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        public frmEvaluatorBusiness()
        {
            InitializeComponent();
        }

        private void frmEvaluatorBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new EvaluatorInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;

            DoBindDataList();
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            EvaluatorInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
            {
                sCurrentNo = InfoColl[0].sEvalorNo;
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        private void DoRefreshForm()
        {
            try
            {
                EvaluatorInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    txtEvalCode.Text = info.sEvaVCode;
                    txtIpAddr.Text = info.sEvaIpAddr.ToString();
                    txtLocPort.Text = info.sEvaLcPort;
                    txtComments.Text = info.sComments;
                }
                else
                {
                    txtEvalCode.Text = "P1";
                    txtIpAddr.Text = "192.168.1.100";
                    txtLocPort.Text = "9810";
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
            string sNo = GetFocusedColumnValue("EvalorNo");
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

                txtEvalCode.Text = "P" + count;
                txtIpAddr.Text = "192.168.1.100";
                txtLocPort.Text = "9810";

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
                    string sNo = GetFocusedColumnValue("EvalorNo");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    EvaluatorInfo info = new EvaluatorInfo();

                    info.sEvalorNo = CommonHelper.Get_New12ByteGuid();
                    info.sEvaVCode = txtEvalCode.Text.Trim();
                    info.sEvaIpAddr = txtIpAddr.Text.Trim();
                    info.sEvaLcPort = txtLocPort.Text.Trim();
                    info.dRegistDate = DateTime.Now;

                    info.sBranchNo = IUserContext.GetBranchNo();

                    info.sComments = txtComments.Text.Trim();

                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(info))
                    {
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                {
                    EvaluatorInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    {
                        info.sEvaVCode = txtEvalCode.Text.Trim();
                        info.sEvaIpAddr = txtIpAddr.Text.Trim();
                        info.sEvaLcPort = txtLocPort.Text.Trim();  
                        info.sComments = txtComments.Text.Trim();


                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(info))
                        {
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

        private void btnSetup_Click(object sender, EventArgs e)
        {
            EvaluatorSetupDialog dlg = new EvaluatorSetupDialog();
            dlg.ShowDialog();
        } 
    }
}
