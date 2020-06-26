using EntWeb.BkConsole.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EntWeb.BkConsole.WService
{
    /// <summary>
    /// QueueWService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://wservice.binet.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class QueueWService : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloService()
        //{
        //    return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //}

        [WebMethod]
        public string QueueService(string cmdName,string cmdArgs)
        {
            try
            {
                string result = "";
                string[] argList = cmdArgs.Split(',');
                switch (cmdName)
                {
                    case "getAllBranchs":
                        result = QueueServImpl.CreateInstance().getAllBranchs();
                        break;
                    case "getAllServices":
                        result = QueueServImpl.CreateInstance().getAllServices(argList[0]);
                        break;
                    case "getService":
                        result = QueueServImpl.CreateInstance().getService(argList[0]);
                        break;
                    case "getAllCounters":
                        result = QueueServImpl.CreateInstance().getAllCounters(argList[0]);
                        break;
                    case "getCounter":
                        result = QueueServImpl.CreateInstance().getCounter(argList[0]);
                        break;
                    case "getAllStaffers":
                        result = QueueServImpl.CreateInstance().getAllStaffers(argList[0]);
                        break;
                    case "getStaffer":
                        result = QueueServImpl.CreateInstance().getStaffer(argList[0]);
                        break;
                    case "getTicket":
                        result = QueueServImpl.CreateInstance().getTicket(argList[0],argList[1]);
                        break;
                    case "getWaitingNumByServiceId":
                        result = QueueServImpl.CreateInstance().getWaitingNumByServiceId(argList[0], argList[1]);
                        break;
                    case "getWaitingNumByCounterId":
                        result = QueueServImpl.CreateInstance().getWaitingNumByCounterId(argList[0], argList[1]);
                        break;
                    case "getWaitingNumByStafferId":
                        result = QueueServImpl.CreateInstance().getWaitingNumByStafferId(argList[0], argList[1]);
                        break;
                    case "getRegistFlowsByBranchId":
                        result = QueueServImpl.CreateInstance().getRegistFlowsByBranchId(argList[0], argList[1], argList[2], argList[3]);
                        break;
                    default:
                        result = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                }

                return result;
            }
            catch(Exception ex)
            {
                return "Error";
            }
        } 
    }
}
