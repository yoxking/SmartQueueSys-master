using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.MainService.Services
{
    public class IMessageService
    {
        private volatile static IMessageService _instance = null;
        private static readonly object lockHelper = new object();

        private static int topn = 3;

        public static IMessageService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IMessageService();
                }
            }
            return _instance;
        }

        private IMessageService()
        {
        }

        public void SendWaitingMessage(object counterNo)
        {
            //string serverurl = IUserContext.GetConfigValue("MqServerurl");
            //string args = "userOpenId=2343&message=2342324";
            //WebHttpUtils.HttpPost(serverurl+"/IServiceController/postWxMessage", args);
        }

        public void SendWelcomeMessage(string registNo)
        {
            //string serverurl = IUserContext.GetConfigValue("MqServerurl");
            //string args = "userOpenId=2343&message=2342324";
            //WebHttpUtils.HttpPost(serverurl+"/IServiceController/postWxMessage", args);
        }
    }
}
