﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole
{
    public class PublicConsts
    {
        public static readonly string DEFAULT_PHOTOFILE = "/images/no_image.jpg";
        public static readonly string SYADMIN_APPCODE = "8cc8";
        public static readonly string DEFAULT_ROLENO = "20130601";

        public const int BRANCHTYPE1 = 1;//门诊
        public const int BRANCHTYPE2 =2;//检验
        public const int BRANCHTYPE3 = 3;//检查
        public const int BRANCHTYPE4 = 4;//药房

        public const int WORKTIMETYPE1 = 0;  //休息
        public const int WORKTIMETYPE2 = 1;  //全天
        public const int WORKTIMETYPE3 = 2;  //上午
        public const int WORKTIMETYPE4 = 3;  //下午

        public const int REGISTETYPE1 = 1;   //挂号
        public const int REGISTETYPE2 = 2;   //预约
        public const int ROTATYPE1 = 1;   //正常排班
        public const int ROTATYPE2 = 2;   //暂时排班

        public const int PRIORITY_TYPE0 = 0; //普通
        public const int PRIORITY_TYPE1 = 1; //老人优先
        public const int PRIORITY_TYPE2 = 2;  //幼儿优先
        public const int PRIORITY_TYPE3 = 3; //军人优先
        public const int PRIORITY_TYPE4 = 4;//离休优先
        public const int PRIORITY_TYPE5 = 5;//过号优先
        public const int PRIORITY_TYPE6 = 6; //其他优先 

        public const int OPERATE_STATE1 = 0;//准备手术
        public const int OPERATE_STATE2 = 3;//入诱导室
        public const int OPERATE_STATE3 = 4;//出诱导室
        public const int OPERATE_STATE4 = 5;//入手术室
        public const int OPERATE_STATE5 = 10;//麻醉开始
        public const int OPERATE_STATE6 = 15;//手术开始
        public const int OPERATE_STATE7 = 25;//手术结束
        public const int OPERATE_STATE8 = 30;//麻醉结束
        public const int OPERATE_STATE9 = 35;//出手术室
        public const int OPERATE_STATE10 = 40;//准备复苏
        public const int OPERATE_STATE11 = 45;//入复苏室
        public const int OPERATE_STATE12 = 55;//出复苏室
        public const int OPERATE_STATE13 = 60;//转入病房
        public const int OPERATE_STATE14 = 80;//病案归档
        public const int OPERATE_STATE15 = -80;//取消手术 

        public const int PROCSTATE_OUTQUEUE = 10;//未入队
        public const int PROCSTATE_DIAGNOSIS = 11;//初诊
        public const int PROCSTATE_TRIAGE = 12;   //分诊
        public const int PROCSTATE_EXCHANGE = 13;   //转诊
        public const int PROCSTATE_PASSTICKET = 14;   //过号初诊
        public const int PROCSTATE_DELAY = 15;   //延迟
        public const int PROCSTATE_REDIAGNOSIS = 16;   //复诊
        public const int PROCSTATE_WAITING = 20;   //综合区域-等候中
        public const int PROCSTATE_WAITAREA1 = 21;   //等候区域1-等候中
        public const int PROCSTATE_WAITAREA2 = 22;   //等候区域2-等候中
        public const int PROCSTATE_WAITAREA3 = 23;   //等候区域3-等候中
        public const int PROCSTATE_WAITAREA9 = 29;   //等候区域9-等候中
        public const int PROCSTATE_CALLING = 30;   //叫号中
        public const int PROCSTATE_PROCESSING = 31;   //就诊中
        public const int PROCSTATE_FINISHED = 32;   //已就诊
        public const int PROCSTATE_NONARRIVAL = 33;   //未到过号
        public const int PROCSTATE_HANGUP = 34;   //挂起
        public const int PROCSTATE_GREENCHANNEL = 35;   //绿色通道
        public const int PROCSTATE_ARCHIVE = 40;   //归档

        public const string DEF_WORKINGMODE = "WorkingMode";  //工作模式，按医生叫号模式（COUNTER），按科室叫号模式(STAFF)
        public const string DEF_WAITAREAMODE = "WaitAreaMode";   //多级候诊区模式(0,1,2,3)
        public const string DEF_CALLINGMODE = "CallingMode";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)
        public const string DEF_REDIAGNOSISINTERVAL = "RediagnosisInterval";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)

    }
}