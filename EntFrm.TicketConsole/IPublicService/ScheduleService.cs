using EntFrm.Framework.Utility;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public class ScheduleService
    {
        private volatile static ScheduleService _instance = null;
        private static readonly object lockHelper = new object();

        public static ScheduleService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ScheduleService();
                }
            }
            return _instance;
        }

        private ScheduleService() { }


        /// <summary>
        /// 启动定时任务
        /// </summary>
        public void StartSchedule()
        {
            try
            {
                JobManager.Initialize(new ScheduleFactory());
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public void StopSchedule()
        {
            try
            {
                JobManager.Stop();
            }
            catch (Exception ex)
            {

            }
        }

        internal class ScheduleFactory : Registry
        {
            public ScheduleFactory()
            {
                //关机
                int athour = int.Parse(IPublicHelper.Get_ShutAtHour());
                int atminute = int.Parse(IPublicHelper.Get_ShutAtMinute());

                Schedule<ShutdownJob>().ToRunEvery(1).Days().At(athour, atminute); //在每天的下午 1:00 分执行 

            }
        } 

        internal class ShutdownJob : IJob
        {
            void IJob.Execute()
            {
                IUserContext.OnExecuteCommand_Xp("doExitService", null);

                Thread.Sleep(5000);

                Application.Exit();
                WindowsHelper.DoExitWindows(WindowsHelper.ExitWindows.ShutDown);
            }
        } 
    }
}
