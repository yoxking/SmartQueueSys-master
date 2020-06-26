using EntFrm.Framework.RabbitMq;
using EntFrm.Framework.RabbitMq.Config;
using EntFrm.Framework.RabbitMq.Model;
using System;

namespace EntFrm.MainService.Services
{
    class IRabbitMqService
    {
        private volatile static IRabbitMqService _instance = null;
        private static readonly object lockHelper = new object();

        private readonly RabbitMqService _rabbitMqProxy;

        public static IRabbitMqService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IRabbitMqService();

                }
            }
            return _instance;
        }

        private IRabbitMqService()
        {
            string serverip = IUserContext.GetConfigValue("MqServerip");
            string username = IUserContext.GetConfigValue("MqUsername");
            string password = IUserContext.GetConfigValue("MqPassword");

            try
            {
                _rabbitMqProxy = new RabbitMqService(new MqConfig
                {
                    AutomaticRecoveryEnabled = true,
                    HeartBeat = 60,
                    NetworkRecoveryInterval = new TimeSpan(60),
                    Host = serverip,
                    UserName = username,
                    Password = password
                });
            }
            catch(Exception ex)
            {
                //throw ex;
            }
        }

        public void StartMqService()
        {
            try
            {
                bool mqenable = bool.Parse(IUserContext.GetConfigValue("MqEnable"));
                if (!mqenable)
                {
                    return;
                }

                    string appcode = IUserContext.GetConfigValue("AppCode");
                string branchno = IUserContext.GetConfigValue("BranchNo");
                string queuename = "Q" + appcode;

                if (_rabbitMqProxy != null)
                {
                    _rabbitMqProxy.RpcService<RpcMssgModel>(queuename, queuename, true, msg =>
                    {
                    //Console.WriteLine("接受信息：");
                    //Console.WriteLine(msg.ToJson()); 
                    string[] cmdArgs = msg.Msg.Split('|');
                        string result = "";

                        switch (cmdArgs[0])
                        {
                            case "helloMessage":
                                result = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                break;
                            case "getRegbranchs":
                                result = IContractService.CreateInstance().getRegbranchs();
                                break;
                            case "getAdvertises":
                                result = IContractService.CreateInstance().getAdvertises(cmdArgs[1]);
                                break;
                            case "getDeptProfile":
                                result = IContractService.CreateInstance().getDeptProfile();
                                break;
                            case "getUserProfile":
                                result = IContractService.CreateInstance().getUserProfile(cmdArgs[1]);
                                break;
                            case "getRegServices":
                                result = IContractService.CreateInstance().getRegServices(cmdArgs[1], cmdArgs[2]);
                                break;
                            case "getRegHistories":
                                result = IContractService.CreateInstance().getRegHistories(cmdArgs[1], cmdArgs[2]);
                                break;
                            case "postRegistInfo":
                                result = IContractService.CreateInstance().postRegistInfo(cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], cmdArgs[5], cmdArgs[6], cmdArgs[7], cmdArgs[8], cmdArgs[9], cmdArgs[10], cmdArgs[11]);
                                break;
                            default: break;
                        }

                        msg.CreateDateTime = DateTime.Now;
                        msg.Msg = result;

                        return msg;
                    }, false);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "消息服务启动完成...");
                }
                else
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "消息服务启动失败...");
                }
            }
            catch(Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "消息服务启动失败...");
            }
        }

        public bool StopMqService()
        {
            if (_rabbitMqProxy != null)
            {
                _rabbitMqProxy.Dispose();
            }
            return true;
        }


    }
}
