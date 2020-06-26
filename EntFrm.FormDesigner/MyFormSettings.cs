using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MyFormDesinger
{
    public partial class MyFormSettings : DockContent
    {
        private string ButtonBgImage2 = "";
        private ServiceInfoBLL myBoss;
        private MyFormDesigner MyDesigner;

        public MyFormSettings()
        {
            InitializeComponent();
        }

        private void MyFormSettings_Load(object sender, EventArgs e)
        {
            myBoss = new ServiceInfoBLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode()); //业务逻辑层实例 
            frmMainFrame mainForm = (frmMainFrame)this.MdiParent;
            MyDesigner = mainForm.myDesigner;
            MyDesigner.MyDesignerControl.Overlayer.ClickEvent += new ClickDelegateHander(getMessage);//注册事件

            DoBindShowDialogs();
        }

        private void DoBindShowDialogs()
        {
            List<ItemObject> dlgList = IPublicEntity.GetShowDialogs();

            dpServiceRules.DataSource = dlgList;
            dpServiceRules.ValueMember = "Value";
            dpServiceRules.DisplayMember = "KeyName";
            dpServiceRules.SelectedIndex = 0;
        }
          
        private void Save_TicketButtons()
        {
            try
            {
                TicketButton btnModel = null;
                TicketButtonEx btnButton = null;
                Control.ControlCollection ctrlColl = MyDesigner.MyDesignerControl.HostFrame.Controls;  
                ServiceInfo info = null;
                string bgImage = "";
                bool bResult = true;

                if (ctrlColl != null && ctrlColl.Count > 0)
                {
                    foreach (Control ctrl in ctrlColl)
                    {
                        bgImage = "\\Uploads\\BtnImage";
                        info = myBoss.GetRecordByNo(ctrl.Name);
                        btnButton = (TicketButtonEx)ctrl;
                        if (info != null)
                        {
                            btnModel = new TicketButton();

                            btnModel.iButtonLeft = btnButton.Location.X;
                            btnModel.iButtonTop = btnButton.Location.Y;
                            btnModel.iButtonWidth = btnButton.Width;
                            btnModel.iButtonHeight = btnButton.Height;

                            btnModel.sButtonBgImage1 = "";
                            btnModel.sButtonBgImage2 = ButtonBgImage2;
                            btnModel.sButtonBgImage3 = "";

                            if (btnButton.BackgroundImage != null)
                            {
                                bgImage += CommonHelper.Get_New8ByteGuid() + ".jpg";
                                btnButton.BackgroundImage.Save(System.Windows.Forms.Application.StartupPath + bgImage);
                                btnModel.sButtonBgImage1 = bgImage;
                            }

                            btnModel.sButtonBgColor = btnButton.BackColor.ToArgb().ToString();

                            btnModel.iTitleFtSize = (int)btnButton.lbTitle.Font.Size;
                            btnModel.sTitleFtFamily = btnButton.lbTitle.Font.FontFamily.Name;
                            btnModel.sTitleFtStyle = btnButton.lbTitle.Font.Style.ToString();
                            btnModel.sTitleFtColor = btnButton.lbTitle.ForeColor.ToArgb().ToString();

                            btnModel.iSubtitleFtSize = (int)btnButton.lbSubtitle.Font.Size;
                            btnModel.sSubtitleFtFamily = btnButton.lbSubtitle.Font.FontFamily.Name;
                            btnModel.sSubtitleFtStyle = btnButton.lbSubtitle.Font.Style.ToString();
                            btnModel.sSubtitleFtColor = btnButton.lbSubtitle.ForeColor.ToArgb().ToString();

                            btnModel.bIsShowTitle = btnButton.lbTitle.Visible;
                            btnModel.bIsShowSubtitle = btnButton.lbSubtitle.Visible;

                            info.sTicketButtonFmt = JsonConvert.SerializeObject(btnModel);

                            if (!myBoss.UpdateRecord(info))
                            {
                                bResult = false;
                                break;
                            }
                        }
                    }
                }

                bgImage = "\\Uploads\\BkImage" + CommonHelper.Get_New8ByteGuid() + ".jpg";
                if (MyDesigner.MyDesignerControl.HostFrame.BackgroundImage != null)
                {
                    MyDesigner.MyDesignerControl.HostFrame.BackgroundImage.Save(System.Windows.Forms.Application.StartupPath + bgImage);
                    IUserContext.SetParamValueByName(IPublicEntity.DEF_BGIMAGE, bgImage, IPublicEntity.TYPE_BACKGROUND);
                }

                if (bResult)
                {
                    MessageBox.Show("保存成功!");
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save_TicketButtons();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPartlevel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ServiceInfo info = myBoss.GetRecordByNo(SelectedParentNo);
            //    if (info != null)
            //    { 
            //        MyDesigner.OnRefreshForm(info.sParentNo);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出错提示：" + ex.Message);
            //}
        }

        private void btnToplevel_Click(object sender, EventArgs e)
        { 
            MyDesigner.OnRefreshForm("00000000");
        }

        private void btnSublevel_Click(object sender, EventArgs e)
        {
            try
            {
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                { 
                    MyDesigner.OnRefreshForm(ctrl.Name);
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮再进入下一级设计界面");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    int x = ctrl.Location.X;
                    int y = ctrl.Location.Y;
                    switch (btn.Name)
                    {
                        case "btnAlignLeft":
                        case "btnAlignRight":
                            y = 0;
                            break;
                        case "btnAlignTop":
                        case "btnAlignBottom":
                            x = 0;
                            break;
                        default: break;

                    }


                    System.Windows.Forms.Control.ControlCollection ctrlColl = MyDesigner.MyDesignerControl.HostFrame.Controls;

                    if (ctrlColl != null && ctrlColl.Count > 0)
                    {
                        foreach (Control myctrl in ctrlColl)
                        {
                            if (x == 0)
                            {
                                myctrl.Location = new Point(myctrl.Location.X, y);
                            }
                            else if (y == 0)
                            {
                                myctrl.Location = new Point(x, myctrl.Location.Y);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSame_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    int width = ctrl.Width;
                    int height = ctrl.Height;
                    switch (btn.Name)
                    {
                        case "btnSameHeight":
                            width = 0;
                            break;
                        case "btnSameWidth":
                            height = 0;
                            break;
                        case "btnSameSize":
                            break;
                        default: break;

                    }


                    System.Windows.Forms.Control.ControlCollection ctrlColl = MyDesigner.MyDesignerControl.HostFrame.Controls;

                    if (ctrlColl != null && ctrlColl.Count > 0)
                    {
                        foreach (Control myctrl in ctrlColl)
                        {
                            if (width == 0)
                            {
                                myctrl.Height = height;
                            }
                            else if (height == 0)
                            {
                                myctrl.Width = width;
                            }
                            else
                            {
                                myctrl.Width = width;
                                myctrl.Height = height;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnButtonImage_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    //创建一个对话框对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //为对话框设置标题
                    ofd.Title = "请选择上传的图片";
                    //设置筛选的图片格式
                    ofd.Filter = "图片格式|*.jpg";
                    //设置是否允许多选
                    ofd.Multiselect = false;
                    //如果你点了“确定”按钮
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //获得文件的完整路径（包括名字后后缀）
                        string filePath = ofd.FileName;
                        //找到文件名比如“1.jpg”前面的那个“\”的位置
                        int position = filePath.LastIndexOf("\\");
                        //从完整路径中截取出来文件名“1.jpg”
                        string fileName =  "\\AppImages\\BtnImage" + CommonHelper.Get_New8ByteGuid() + ".jpg";
                        //读取选择的文件，返回一个流
                        using (Stream stream = ofd.OpenFile())
                        {
                            //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
                            //如果是绝对路径，放在那里都行，我用的是相对路径）
                            using (FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + fileName, FileMode.CreateNew))
                            {
                                //将得到的文件流复制到写入流中
                                stream.CopyTo(fs);
                                //将写入流中的数据写入到文件中
                                fs.Flush();
                            }
                            //PictrueBOx 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传 
                            //将文件路径显示在文本框中   
                            ctrl.BackgroundImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + fileName);
                            ctrl.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    System.Windows.Forms.FontDialog dlg = new FontDialog();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        ctrl.lbTitle.Font = dlg.Font;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnButtonBkcolor_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    System.Windows.Forms.ColorDialog dlg = new ColorDialog();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        ctrl.BackColor = dlg.Color;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnBkgroundImg_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个对话框对象
                OpenFileDialog ofd = new OpenFileDialog();
                //为对话框设置标题
                ofd.Title = "请选择上传的图片";
                //设置筛选的图片格式
                ofd.Filter = "图片格式|*.jpg";
                //设置是否允许多选
                ofd.Multiselect = false;
                //如果你点了“确定”按钮
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //获得文件的完整路径（包括名字后后缀）
                    string filePath = ofd.FileName;
                    //找到文件名比如“1.jpg”前面的那个“\”的位置
                    int position = filePath.LastIndexOf("\\");
                    //从完整路径中截取出来文件名“1.jpg”
                    string fileName = "\\AppImages\\BkImage" + CommonHelper.Get_New8ByteGuid() + ".jpg";
                    //读取选择的文件，返回一个流
                    using (Stream stream = ofd.OpenFile())
                    {
                        //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
                        //如果是绝对路径，放在那里都行，我用的是相对路径）
                        using (FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + fileName, FileMode.CreateNew))
                        {
                            //将得到的文件流复制到写入流中
                            stream.CopyTo(fs);
                            //将写入流中的数据写入到文件中
                            fs.Flush();
                        }
                        //PictrueBOx 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传 
                        //将文件路径显示在文本框中 
                        Image bgImg= Image.FromFile(System.Windows.Forms.Application.StartupPath + fileName);
                        //IUserContext.SetParamValueByName(IPublicEntity.DEF_BGIMAGE, fileName, IPublicEntity.TYPE_BACKGROUND);
                        MyDesigner.MyDesignerControl.HostFrame.BackgroundImage = bgImg;

                        //int width = bgImg.Width;
                        //int height = bgImg.Height;
                        //MyDesigner.MyDesignerControl.HostFrame.Size = new Size(width, height);
                        //MyDesigner.MyDesignerControl.Overlayer.setRecter(10,10,width, height);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    System.Windows.Forms.ColorDialog dlg = new ColorDialog();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        ctrl.lbTitle.ForeColor = dlg.Color;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MyFormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dresult = MessageBox.Show("是否保存内容?", "确认", MessageBoxButtons.YesNoCancel);
            if (dresult == DialogResult.Yes)
            {
                Save_TicketButtons(); 
            }
            else if (dresult == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            this.Dispose();
            Application.Exit();
        }

        private void btnSFontColor_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    System.Windows.Forms.ColorDialog dlg = new ColorDialog();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        ctrl.lbSubtitle.ForeColor = dlg.Color;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSubtitleFont_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    System.Windows.Forms.FontDialog dlg = new FontDialog();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        ctrl.lbSubtitle.Font = dlg.Font;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ckTitleShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    ctrl.lbTitle.Visible = ckTitleShow.Checked;
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ckSubtitleShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    ctrl.lbSubtitle.Visible = ckSubtitleShow.Checked;
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnButtonImage2_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    //创建一个对话框对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //为对话框设置标题
                    ofd.Title = "请选择上传的图片";
                    //设置筛选的图片格式
                    ofd.Filter = "图片格式|*.jpg";
                    //设置是否允许多选
                    ofd.Multiselect = false;
                    //如果你点了“确定”按钮
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //获得文件的完整路径（包括名字后后缀）
                        string filePath = ofd.FileName;
                        //找到文件名比如“1.jpg”前面的那个“\”的位置
                        int position = filePath.LastIndexOf("\\");
                        //从完整路径中截取出来文件名“1.jpg”
                        string fileName = "\\AppImages\\BtnImage" + CommonHelper.Get_New8ByteGuid() + ".jpg";
                        //读取选择的文件，返回一个流
                        using (Stream stream = ofd.OpenFile())
                        {
                            //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
                            //如果是绝对路径，放在那里都行，我用的是相对路径）
                            using (FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + fileName, FileMode.CreateNew))
                            {
                                //将得到的文件流复制到写入流中
                                stream.CopyTo(fs);
                                //将写入流中的数据写入到文件中
                                fs.Flush();
                            }
                            //PictrueBOx 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传 
                            //将文件路径显示在文本框中 
                            ButtonBgImage2 = fileName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
                if (ctrl != null)
                {
                    int x = ctrl.Location.X;
                    int y = ctrl.Location.Y;
                    int newX = 0;
                    int newY = 0;
                    int width = ctrl.Width;
                    int height = ctrl.Height;
                    int pnlWidth = MyDesigner.MyDesignerControl.HostFrame.Width;
                    int pnlHeight = MyDesigner.MyDesignerControl.HostFrame.Height;
                    switch (btn.Name)
                    {
                        case "btnMoveUp":
                            if (y > 5)
                            {
                                newX=x;
                                newY= y - 5;
                            }
                            else
                            {
                                newX=x;
                                newY= 0;
                            }
                            break;
                        case "btnMoveDown":
                            if (y < pnlHeight - height)
                            {
                                newX=x;
                                newY=y + 5;
                            }
                            else
                            {
                                newX=x;
                                newY= pnlHeight - height;
                            }
                            break;
                        case "btnMoveLeft":
                            if (x > 5)
                            {
                                newX=x - 5;
                                newY= y;
                            }
                            else
                            {
                                newX=0;
                                newY= y;
                            }
                            break;
                        case "btnMoveRight":
                            if (x < pnlWidth - width)
                            {
                                newX=x + 5;
                                newY= y;
                            }
                            else
                            {
                                newX=pnlWidth - width;
                                newY= y;
                            }
                            break;
                        default: break;

                    }

                    ctrl.Location = new Point(newX, newY);
                    MyDesigner.MyDesignerControl.Overlayer.setRecter(newX + 10, newY + 10, ctrl.Width, ctrl.Height);

                }
                else
                {
                    MessageBox.Show("请先选择一个按钮!");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dpServiceRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;

                ServiceInfo info = myBoss.GetRecordByNo(ctrl.Name);
                if (info != null)
                {
                    if (dpServiceRules.SelectedValue.ToString().Equals("Null"))
                    {
                        info.iIsShowDialog = 0;
                        info.sShowDialogName = "Null";
                    }
                    else
                    {
                        info.iIsShowDialog = 1;
                        info.sShowDialogName = dpServiceRules.SelectedValue.ToString();
                    }

                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;

                    myBoss.UpdateRecord(info);
                }
            }
            catch(Exception ex)
            { }
        }

        public void getMessage(string message)
        {
            try
            {
                TicketButtonEx ctrl = (TicketButtonEx)MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;

                ckTitleShow.Checked = ctrl.lbTitle.Visible;
                ckSubtitleShow.Checked = ctrl.lbSubtitle.Visible;

                ServiceInfo info = myBoss.GetRecordByNo(ctrl.Name);
                if (info != null)
                {  
                    dpServiceRules.SelectedValue = info.sShowDialogName;
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnClearImg_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            Control ctrl = MyDesigner.MyDesignerControl.Overlayer.CurrentCtrl;
            if (ctrl != null)
            {
                ctrl.BackgroundImage = null;
            }
        }

        private void btnClearBgImg_Click(object sender, EventArgs e)
        {
            MyDesigner.MyDesignerControl.HostFrame.BackgroundImage = null;
        }
    }
}
