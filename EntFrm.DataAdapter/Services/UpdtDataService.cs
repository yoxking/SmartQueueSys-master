using EntFrm.DataAdapter.Business;
using System;
using System.Threading;

namespace EntFrm.DataAdapter.Services
{
    public class UpdtDataService
    {
        private volatile static UpdtDataService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;

        public static UpdtDataService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new UpdtDataService();
                }
            }
            return _instance;
        }
        private UpdtDataService() { }

        public void StartUpdateTask()
        {

            MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "数据采集服务启动完成...");
            IAdapterBusiness adapterBoss = AdapterFactory.Create();

            //while (adapterBoss != null)
            //{
            //    if (isQuitFlag)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(10000);

            //    try
            //    {
                    

            //    }
            //    catch (Exception ex)
            //    {
            //        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "病人信息更新失败," + ex.Message);
            //    }
            //}
        }

        public void StopUpdateTask()
        {
            isQuitFlag = true;
        }
    }
}
