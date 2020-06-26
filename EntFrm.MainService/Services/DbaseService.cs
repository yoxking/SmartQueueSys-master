using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.MainService.Services
{
    public class DbaseService
    {
        private volatile static DbaseService _instance = null;
        private static readonly object lockHelper = new object();
        public static DbaseService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new DbaseService();
                }
            }
            return _instance;
        }


        public void doClearQueueData()
        {
            try
            {
                string where = " BranchNo='"+IUserContext.GetBranchNo()+"' And AddDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                EvaluateFlowsBLL evalBoss = new EvaluateFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                 
                ProcessFlowsBLL procBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                RegistFlowsBLL regBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                TicketFlowsBLL ticBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  

                evalBoss.HardDeleteByCondition(where); 
                procBoss.HardDeleteByCondition(where);
                regBoss.HardDeleteByCondition(where);
                ticBoss.HardDeleteByCondition(where);

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "清空排队数据完成..."); 
            }
            catch (Exception ex)
            { 
            }
        } 

        public void doClearAllData()
        {
            try
            {
                //创建任务
                Task task = new Task(() =>
                {
                    //获得文件的完整路径（包括名字后后缀）
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("EntFrm.MainService.Resources.clearalldata.csql");


                    ArrayList mylist = IDbaseHelper.GetSqlFile(stream);
                    IDbaseHelper.ExecuteCmd(mylist, IUserContext.GetConnStr());

                });
                //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
                task.Start();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "清空所有数据完成...");
                 
            }
            catch (Exception ex)
            { 
            }
        }

        public void doBakupData()
        {
            try
            {
                //string localFilePath, fileNameExt, newFileName, FilePath; 
                SaveFileDialog sfd = new SaveFileDialog();
                //设置文件类型 
                sfd.Filter = "数据库备份文件（*.bak）|*.bak";

                //设置默认文件类型显示顺序 
                sfd.FilterIndex = 1;

                //保存对话框是否记忆上次打开的目录 
                sfd.RestoreDirectory = true;

                //点了保存按钮进入 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName.ToString(); //获得文件路径 
                    string dbaseName = IDbaseHelper.GetDataBaseName(IUserContext.GetConnStr());
                    string cmdText = @"backup database " + dbaseName + " to disk='" + fileName + "'";
                    IDbaseHelper.BakReductSql(IUserContext.GetConnStr(), dbaseName, cmdText, true);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "备份数据库完成..."); 
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "备份数据库失败：" + ex.Message); 
            }
        }

        public void doRecoverData()
        {
            try
            {
                //创建一个对话框对象
                OpenFileDialog ofd = new OpenFileDialog();
                //为对话框设置标题
                ofd.Title = "请选择bak文件";
                //设置筛选的图片格式
                ofd.Filter = "bak格式|*.bak";
                //设置是否允许多选
                ofd.Multiselect = false;
                //如果你点了“确定”按钮
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //获得文件的完整路径（包括名字后后缀） 
                    string fileName = ofd.FileName;
                    string dbaseName = IDbaseHelper.GetDataBaseName(IUserContext.GetConnStr());
                    string cmdText = @"restore database " + dbaseName + " from disk='" + fileName + "' WITH REPLACE";
                    IDbaseHelper.BakReductSql(IUserContext.GetConnStr(), dbaseName, cmdText, false);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "恢复数据库完成..."); 
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "恢复数据库失败：" + ex.Message);
            }
        }

        public void doMigrateData()
        {
            try
            {
                doMigrateProcessFlow();
                doMigrateRegisteFlow();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "历史数据迁移完成...");
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "历史数据迁移失败：" + ex.Message);
            }
        }

        private bool doMigrateProcessFlow()
        {
            int count = 0;
            string where = " BranchNo='" + IUserContext.GetBranchNo() + "' And ModDate <'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' ";
            ProcessHistoryBLL phistoryBLL = new ProcessHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ViewTicketFlowsBLL ticketsBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ProcessFlowsBLL pflowsBLL = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            TicketFlowsBLL tflowsBLL = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ProcessHistory phistory = null;

            ViewTicketFlowsCollections ticketsColl = ticketsBLL.GetRecordsByPaging(ref count, 1, 1000, where);
            if (ticketsColl != null && ticketsColl.Count > 0)
            {
                foreach(ViewTicketFlows ticket in ticketsColl)
                {
                    phistory = new ProcessHistory(); 
                    phistory.sHFlowNo = CommonHelper.Get_New12ByteGuid(); ;
                    phistory.iDataFlag =1;
                    phistory.sTicketNo = ticket.sTicketNo;
                    phistory.sRUserNo = ticket.sRUserNo;
                    phistory.sCnName = ticket.sCnName;
                    phistory.iAge = ticket.iAge;
                    phistory.iSex = ticket.iSex;
                    phistory.sServiceNo = ticket.sServiceNo;
                    phistory.sCounterNos = ticket.sCounterNos;
                    phistory.sWFlowsNo = ticket.sWFlowsNo;
                    phistory.iWFlowsIndex = ticket.iWFlowsIndex;
                    phistory.dEnqueueTime = ticket.dEnqueueTime;
                    phistory.dBeginTime = ticket.dBeginTime;
                    phistory.dFinishTime = ticket.dFinishTime;
                    phistory.iProcessState = ticket.iPauseState;
                    phistory.sProcessFormat = ticket.sProcessFormat;
                    phistory.iProcessIndex = ticket.iProcessIndex;
                    phistory.iPriorityType = ticket.iPriorityType;
                    phistory.iOrderWeight = ticket.iOrderWeight;
                    phistory.iPauseState = ticket.iPauseState;
                    phistory.iDelayType = ticket.iDelayType;
                    phistory.iDelayTimeValue = ticket.iDelayTimeValue;
                    phistory.iDelayStepValue = ticket.iDelayStepValue;
                    phistory.dProcessedTime = ticket.dProcessedTime;
                    phistory.sProcessedCounterNo = ticket.sProcessedCounterNo;
                    phistory.sProcessedStafferNo = ticket.sProcessedStafferNo;

                    phistory.sBranchNo = IUserContext.GetBranchNo();
                    phistory.sAddOptor = "";
                    phistory.dAddDate = DateTime.Now;
                    phistory.sModOptor = "";
                    phistory.dModDate = DateTime.Now;
                    phistory.iValidityState = 1;
                    phistory.sComments = "";
                    phistory.sAppCode = IUserContext.GetAppCode() + "; ";

                    phistoryBLL.AddNewRecord(phistory);
                }

                //
                pflowsBLL.HardDeleteByCondition(where);
                tflowsBLL.HardDeleteByCondition(where); 
            } 

            return true;
        }

        private bool doMigrateRegisteFlow()
        {
            int count = 0;
            string where = " BranchNo='" + IUserContext.GetBranchNo() + "' And ModDate <'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' ";
            RegistHistoryBLL rhistoryBLL = new RegistHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ViewRegistFlowsBLL registeBLL = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            RegistFlowsBLL rflowsBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            RegistHistory rhistory = null;

            ViewRegistFlowsCollections registesColl = registeBLL.GetRecordsByPaging(ref count, 1, 1000, where);
            if (registesColl != null && registesColl.Count > 0)
            {
                foreach (ViewRegistFlows registe in registesColl)
                {
                    rhistory = new RegistHistory();
                    rhistory.sHFlowNo = CommonHelper.Get_New12ByteGuid(); 
                    rhistory.iDataFlag = 1;
                    rhistory.sRUserNo = registe.sRUserNo;
                    rhistory.sCnName = registe.sCnName;
                    rhistory.iAge = registe.iAge;
                    rhistory.iSex = registe.iSex;
                    rhistory.iRegistType = registe.iRegistType;
                    rhistory.sDataFrom = registe.sDataFrom;
                    rhistory.dRegistDate = registe.dRegistDate;
                    rhistory.sServiceNo = registe.sServiceNo;
                    rhistory.sCounterNo = registe.sCounterNo;
                    rhistory.iWorkTime = registe.iWorkTime;
                    rhistory.dStartDate = registe.dStartDate;
                    rhistory.dEnditDate = registe.dEnditDate;
                    rhistory.iRegistState = registe.iRegistState;

                    rhistory.sBranchNo = IUserContext.GetBranchNo();
                    rhistory.sAddOptor = "";
                    rhistory.dAddDate = DateTime.Now;
                    rhistory.sModOptor = "";
                    rhistory.dModDate = DateTime.Now;
                    rhistory.iValidityState = 1;
                    rhistory.sComments = "";
                    rhistory.sAppCode = IUserContext.GetAppCode() + "; ";

                    rhistoryBLL.AddNewRecord(rhistory);
                }

                //
               rflowsBLL.HardDeleteByCondition(where);
            }

            return true;
        }
    }
}
