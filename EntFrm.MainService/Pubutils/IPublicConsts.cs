using EntFrm.Framework.Utility;
using System.Collections.Generic;

namespace EntFrm.MainService
{
    public class IPublicConsts
    {
        public const string DOGTYPE = "160148364901";//大厅排队程序
        public const string ENCKEY = "123FFF7890ABCDEF1234567890ABCDEF";
        public const string WHKey = "123FFFFF";
        public const string WLKey = "456FFFFF";
        public const string RHKey = "666FFFFF";
        public const string RLKey = "777FFFFF";
        public const int SPACEVAL = 2;

        public const int REGISTETYPE1 = 1;   //今日取号
        public const int REGISTETYPE2 = 2;   //预约取号 

        public const int PRIORITY_TYPE0 = 0; //普通
        public const int PRIORITY_TYPE1 = 1; //优先 


        public const int PROCSTATE_OUTQUEUE = 0;//未入队 
        public const int PROCSTATE_WAITING = 1;   //等候中 
        public const int PROCSTATE_CALLING = 2;   //叫号中
        public const int PROCSTATE_PROCESSING = 3;   //处理中
        public const int PROCSTATE_FINISHED = 5;   //已完成 
        public const int PROCSTATE_NOTCOME = 6;   //未到
        public const int PROCSTATE_ABORT = 7;   //中止处理
        public const int PROCSTATE_EVALUATE = 8;   //完成评价
        public const int PROCSTATE_ARCHIVE = 9;   //归档

        public const string DEF_CALLVOICEENABLE = "CallVoiceEnable";
        public const string DEF_CALLVOICEFORMAT = "CallVoiceFormat";
        public const string DEF_CALLVOICEVOLUME = "CallVoiceVolume";
        public const string DEF_CALLVOICERATE = "CallVoiceRate";
        public const string DEF_CALLVOICESTYLE = "CallVoiceStyle";

        public const string DEF_REGISTEMODE = "RegisteModel";


        public static List<ItemObject> GetRegisteModel()
        {
            List<ItemObject> styles = new List<ItemObject>();

            styles.Add(new ItemObject("自动报到模式", "AutoRegiste"));
            styles.Add(new ItemObject("刷卡报到模式", "ScanRegiste")); 

            return styles;
        }
    }
}
