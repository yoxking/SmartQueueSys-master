using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.TicketConsole
{
    public class IPublicConsts
    {

        public const int PROCSTATE_OUTQUEUE = 0;//未入队 
        public const int PROCSTATE_WAITING = 1;   //等候中 
        public const int PROCSTATE_CALLING = 2;   //叫号中
        public const int PROCSTATE_PROCESSING = 3;   //处理中
        public const int PROCSTATE_FINISHED = 5;   //已完成 
        public const int PROCSTATE_NOTCOME = 6;   //未到
        public const int PROCSTATE_ABORT = 7;   //中止处理
        public const int PROCSTATE_ARCHIVE = 9;   //归档
    }
}
