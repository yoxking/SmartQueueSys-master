using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MyFormDesinger
{
    public partial class MyFormDesigner : DockContent
    {  
        public DesignerControl MyDesignerControl;

        public MyFormDesigner()
        {
            InitializeComponent();
        } 

        private void MyFormDesigner_Load(object sender, EventArgs e)
        {
            try
            {   
                MyDesignerControl = this.MyDesigner;

                OnRefreshForm("00000000");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OnRefreshForm(string selectedParentNo)
        {
            try
            {
                TicketButton btnModel = null;
                TicketButtonEx btnButton = null;
                FontStyle fsStyle = FontStyle.Regular;

                ServiceInfoBLL myBoss = new ServiceInfoBLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode()); //业务逻辑层实例   
                ServiceInfoCollections infoColl = myBoss.GetRecordsByClassNo(selectedParentNo);

                string sBgImage = IUserContext.GetParamValueByName(IPublicEntity.DEF_BGIMAGE, IPublicEntity.TYPE_BACKGROUND); 
                if (!string.IsNullOrEmpty(sBgImage))
                {
                    try
                    { 
                        Image bgImg = Image.FromFile(System.Windows.Forms.Application.StartupPath + sBgImage);
                        if (bgImg != null)
                        {
                            this.MyDesigner.HostFrame.BackgroundImage = bgImg;
                            //this.MyDesigner.HostFrame.BackgroundImageLayout = ImageLayout.Stretch;

                            int width = bgImg.Width;
                            int height = bgImg.Height;
                            MyDesignerControl.HostFrame.Size = new Size(width, height);
                            MyDesignerControl.Overlayer.setRecter(10,10, width, height);
                        }
                    }
                    catch(Exception ex)
                    { 
                    }
                }

                this.MyDesigner.HostFrame.Controls.Clear();

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo info in infoColl)
                    {
                        btnModel=JsonConvert.DeserializeObject<TicketButton>(info.sTicketButtonFmt);

                        if (btnModel != null)
                        {
                            btnButton = new TicketButtonEx();
                            btnButton.Name = info.sServiceNo;

                            btnButton.Location = new Point(btnModel.iButtonLeft, btnModel.iButtonTop);
                            btnButton.Size = new Size(btnModel.iButtonWidth, btnModel.iButtonHeight);

                            btnButton.BackColor = Color.FromArgb(int.Parse(btnModel.sButtonBgColor));
                            if (!string.IsNullOrEmpty(btnModel.sButtonBgImage1))
                            {
                                try
                                {
                                    btnButton.BackgroundImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + btnModel.sButtonBgImage1);
                                }
                                catch(Exception ex)
                                { }
                            }
                            btnButton.lbTitle.Text = info.sServiceName;
                            if (!string.IsNullOrEmpty(btnModel.sTitleFtStyle))
                            {
                                fsStyle = (FontStyle)Enum.Parse(typeof(FontStyle), btnModel.sTitleFtStyle);
                            }
                            btnButton.lbTitle.Font = new Font(btnModel.sTitleFtFamily, btnModel.iTitleFtSize,fsStyle);
                            btnButton.lbTitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sTitleFtColor)); 

                            btnButton.lbSubtitle.Font = new Font(btnModel.sSubtitleFtFamily, btnModel.iSubtitleFtSize);
                            btnButton.lbSubtitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sSubtitleFtColor));

                            btnButton.lbTitle.Visible = btnModel.bIsShowTitle;
                            //ckTitleShow.Checked = btnModel.bIsShowTitle;
                            btnButton.lbSubtitle.Visible = btnModel.bIsShowSubtitle;
                            //ckSubtitleShow.Checked = btnModel.bIsShowSubtitle;
                             
                            this.MyDesigner.HostFrame.Controls.Add(btnButton);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
