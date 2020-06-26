using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class frmLoadForm : Form
    {
        private BackgroundWorker bkWorker = new BackgroundWorker();
        //private bool bBgImagesFinished = false;
        //private bool bServicesFinished = false;
        //private string xx = "";

        public frmLoadForm()
        {
            InitializeComponent();
        }

        private void frmLoadForm_Load(object sender, EventArgs e)
        {
            // 使用BackgroundWorker时不能在工作线程中访问UI线程部分，  
            // 即你不能在BackgroundWorker的事件和方法中操作UI，否则会抛跨线程操作无效的异常  
            // 添加下列语句可以避免异常。  
            CheckForIllegalCrossThreadCalls = false;

            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            IPublicHelper.IsLoadFinished = false;

            //检测升级程序、下载最新数据
            bkWorker.RunWorkerAsync();

        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }

        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                this.myProgress.Value = e.ProgressPercentage;
                if (e.UserState!=null)
                {
                    int dotLength = int.Parse(e.UserState.ToString()) % 6;
                    lbMessage.Text = ("正在连接主机服务中......").Substring(0, 9 + dotLength);
                }
                else
                {
                    lbMessage.Text = "正在更新取票信息中......";
                }
            }
            catch(Exception ex)
            { }
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        { 
            IPublicHelper.IsLoadFinished = true;
            this.Close(); //关闭登陆窗体
        }

        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            //判断是否请求了取消后台操作
            if (bkWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    string helloStr = "";
                    int dotNum = 1;

                    do
                    {
                        try
                        {
                            helloStr = IUserContext.OnExecuteCommand_Xp("doHelloQueue", null);
                        }
                        catch (Exception ex)
                        {
                        }

                        bkWorker.ReportProgress(0,dotNum++);
                        Thread.Sleep(1000);

                    } while (string.IsNullOrEmpty(helloStr));


                    //window 7以上版本
                    //*********************************************************************************************************************

                    //int precentNum = 2;
                    //string autoStretch=IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { "BgAutoStretch", "Background" });
                    //try
                    //{
                    //    IPublicHelper.BgAutoStretch = int.Parse(autoStretch);
                    //}
                    //catch (Exception ex)
                    //{ }

                    //IUserContext.OnExecuteCommandAsync_Xp("getAllServices", null, Services_LoadFinished);

                    //string bgFile=IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { "BgImage", "Background" });
                    //if (!string.IsNullOrEmpty(bgFile))
                    //{
                    //    IUserContext.OnExecuteCommandAsync_Xp("getImageFrom", new string[] { bgFile }, BgImages_LoadFinished);
                    //}

                    //while ((precentNum) < 100)
                    //{
                    //    bkWorker.ReportProgress(precentNum);
                    //    if (bServicesFinished && bBgImagesFinished)
                    //    {
                    //        IPublicHelper.IsLoadFinished = true;
                    //        break;
                    //    }

                    //    precentNum += 2;
                    //    Thread.Sleep(100);
                    //}
                    //********************************************************************************************************************

                    bkWorker.ReportProgress(10);

                    //Windows xp版本
                    //*********************************************************************************************************************
                    string autoStretch = IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { "BgAutoStretch", "Background" });
                    try
                    {
                        IPublicHelper.BgAutoStretch = int.Parse(autoStretch);
                    }
                    catch (Exception ex)
                    { }

                    IPublicHelper.RegisteModel= IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { "RegisteModel", "Other" });

                    string sResult = IUserContext.OnExecuteCommand_Xp("getAllServices", null);
                    IPublicHelper.serviceList=JsonConvert.DeserializeObject<List<ServiceInfo>>(sResult);

                    //string sCondition = "";
                    //string sResult = "";
                    //int iCount = int.Parse(IUserContext.OnExecuteCommand_Xp("getServicesCount", new string[] { sCondition }));
                    //if (iCount > 0)
                    //{
                    //    int pageSize = 5;
                    //    int pageCount = PubHelper.GetRoundingDevision(iCount, pageSize);
                    //    for (int i = 0; i < pageCount; i++)
                    //    {
                    //        sResult = IUserContext.OnExecuteCommand_Xp("getServicesByPaging", new string[] { (i + 1).ToString(), pageSize.ToString(), sCondition });
                    //        IPublicHelper.serviceList.AddRange(JsonConvert.DeserializeObject<List<ServiceInfo>>(sResult));
                    //    }
                    //}
                     
                    string bgFile = IUserContext.OnExecuteCommand_Xp("getParamValue", new string[] { "BgImage", "Background" });
                    sResult = IUserContext.OnExecuteCommand_Xp("getImageFrom", new string[] { bgFile });
                    if (!string.IsNullOrEmpty(sResult))
                    {
                        try
                        {
                            Image bgImage = ImageConvert.FromBaseString(sResult);
                            using (MemoryStream Ms = new MemoryStream())
                            {
                                bgImage.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                Image.FromStream(Ms).Save(IPublicHelper.BackgroundImage);                                
                            }
                            //bgImage.Save(IPublicHelper.BackgroundImage);
                        }
                        catch (Exception ex)
                        {
                            IPublicHelper.BackgroundImage =Application.StartupPath + "\\AppImages\\BackgroundImage.jpg";
                        }
                    }
                    bkWorker.ReportProgress(50);

                    Thread.Sleep(2000);
                    //**********************************************************************************************************************
                }
                catch (Exception ex)
                { 
                    e.Cancel = true;
                }
            }

            return -1;
        }

        private void lbMessage_Click(object sender, EventArgs e)
        {
            frmLoginForm login = new frmLoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                ContextDialog dlg = new ContextDialog();
                if (dlg.ShowDialog() == DialogResult.Yes)
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                }
            } 
        }

        //private void BgImages_LoadFinished(object sender, OnExecuteCommandCompletedEventArgs e)
        //{
        //    bBgImagesFinished = true;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(e.Result))
        //        {
        //            Image bgImage = ImageConvert.FromBaseString(e.Result);
        //            bgImage.Save(IPublicHelper.BackgroundImage);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //private void Services_LoadFinished(object sender, OnExecuteCommandCompletedEventArgs e)
        //{
        //    bServicesFinished = true;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(e.Result))
        //        {
        //            IPublicHelper.serviceList = JsonConvert.DeserializeObject<List<ServiceInfo>>(e.Result);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}