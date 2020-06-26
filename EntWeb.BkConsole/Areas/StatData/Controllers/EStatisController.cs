using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;
using EntWeb.BkConsole.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.StatData.Controllers
{
    public class EStatisController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return "";
                }
                return temp.ToString();
            }
        }

        // GET: StatData/TStatis
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: StatData/TFlows
        public override ActionResult List()
        {
            try
            {
                int totalCount = 0; 
                StafferInfoCollections infoList = getStaffsList();

                List<EvalStatsData> statsList = new List<EvalStatsData>();
                EvalStatsData evalStats = null;

                if (infoList != null && infoList.Count > 0)
                {
                    foreach (StafferInfo info in infoList)
                    {
                        evalStats = new EvalStatsData();

                        evalStats.sStafferNo = info.sStafferNo;
                        evalStats.sLoginId = info.sLoginId;
                        evalStats.sStafferName = info.sStafferName;
                        evalStats.iTotalScore = getTotalScore_ByStafferNo(info.sStafferNo, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iVGoodNum = getTotalCount_ByStafferNo(info.sStafferNo, 3, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iGoodNum = getTotalCount_ByStafferNo(info.sStafferNo, 2, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iNormalNum = getTotalCount_ByStafferNo(info.sStafferNo, 1, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iBadNum = getTotalCount_ByStafferNo(info.sStafferNo, 0, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iUnknownNum = getTotalCount_ByStafferNo(info.sStafferNo, -1, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.sComments = "";

                        statsList.Add(evalStats);
                    }
                }

                //StafferInfoCollections staffs = getStaffsList();
                PagerHelper pager = new PagerHelper(1, 100, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", statsList);
                //stackHolder.Add("staffList", staffs);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }

        private int getTotalScore_ByStafferNo(string StafferNo, DateTime startTime, DateTime endTime)
        {
            int count = 0;
            int totalScore = 0;
            string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And EvaluateTime Between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' And '" + endTime.ToString("yyyy-MM-dd 00:00:00") + "' And EvalStafferNo='" + StafferNo + "' ";

            EvaluateFlowsBLL infoBLL = new EvaluateFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            EvaluateFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1000, sWhere);

            if (infoColl != null && infoColl.Count > 0)
            {
                foreach (EvaluateFlows info in infoColl)
                {
                    totalScore += info.iEvalResult;
                }
            }

            return totalScore;
        }

        private int getTotalCount_ByStafferNo(string StafferNo, int levelNo, DateTime startTime, DateTime endTime)
        {

            string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And  EvalStafferNo='" + StafferNo + "' And EvaluateTime Between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' And '" + endTime.ToString("yyyy-MM-dd 00:00:00") + "' ";
            switch (levelNo)
            {
                case 3:
                    sWhere += " And VeryGood=1 "; break;
                case 2:
                    sWhere += " And Good=1 "; break;
                case 1:
                    sWhere += " And Normal=1 "; break;
                case 0:
                    sWhere += " And Bad=1 "; break;
                case -1:
                    sWhere += " And Unknown=1 "; break;
                default: break;

            }

            EvaluateFlowsBLL infoBLL = new EvaluateFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            int count = infoBLL.GetCountByCondition(sWhere);

            return count;
        }

        private StafferInfoCollections getStaffsList()
        {
            try
            {
                int count = 0;
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return  infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "' ");

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //
        // GET: /System/Role/Search
        //[(Message = "信息查询(Search)")]
        public override ActionResult Search()
        {
            //sWhere = "1=1 "; 

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }


        public DataTable ConvertToDataTable()
        {
            try
            {
                ///
                ///将Collection装为DataTable
                ///
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;
                dc = new DataColumn("序号");
                dt.Columns.Add(dc);
                dc = new DataColumn("员工编号");
                dt.Columns.Add(dc);
                dc = new DataColumn("员工姓名");
                dt.Columns.Add(dc);
                dc = new DataColumn("总评分");
                dt.Columns.Add(dc);
                dc = new DataColumn("优秀次数");
                dt.Columns.Add(dc);
                dc = new DataColumn("好评次数");
                dt.Columns.Add(dc);
                dc = new DataColumn("良好次数");
                dt.Columns.Add(dc);
                dc = new DataColumn("差评次数");
                dt.Columns.Add(dc);
                  
                StafferInfoCollections infoList = getStaffsList();

                List<EvalStatsData> statsList = new List<EvalStatsData>();
                EvalStatsData evalStats = null;

                if (infoList != null && infoList.Count > 0)
                {
                    foreach (StafferInfo info in infoList)
                    {
                        evalStats = new EvalStatsData();

                        evalStats.sStafferNo = info.sStafferNo;
                        evalStats.sLoginId = info.sLoginId;
                        evalStats.sStafferName = info.sStafferName;
                        evalStats.iTotalScore = getTotalScore_ByStafferNo(info.sStafferNo, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iVGoodNum = getTotalCount_ByStafferNo(info.sStafferNo, 3, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iGoodNum = getTotalCount_ByStafferNo(info.sStafferNo, 2, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iNormalNum = getTotalCount_ByStafferNo(info.sStafferNo, 1, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iBadNum = getTotalCount_ByStafferNo(info.sStafferNo, 0, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.iUnknownNum = getTotalCount_ByStafferNo(info.sStafferNo, -1, DateTime.Now, DateTime.Now.AddDays(1));
                        evalStats.sComments = "";

                        statsList.Add(evalStats);
                    }
                }

                int i = 1;
                foreach (EvalStatsData info in statsList)
                {
                    dr = dt.NewRow();
                    dr["序号"] = i.ToString();
                    dr["员工编号"] = info.sLoginId;
                    dr["员工姓名"] = info.sStafferName;
                    dr["总评分"] = info.iTotalScore;
                    dr["优秀次数"] = info.iVGoodNum;
                    dr["好评次数"] = info.iGoodNum;
                    dr["良好次数"] = info.iNormalNum;
                    dr["差评次数"] = info.iBadNum;
                    dt.Rows.Add(dr);
                    i++;
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult ExportXls()
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
                DataTable dt = ConvertToDataTable();
                //生成Excel资源流
                MemoryStream ms = ExcelHelper.RenderDataTableToExcel(dt, "服务评价统计表", "服务评价统计表", "服务评价统计表", "", "打印人：" + sSuNo, "打印时间：" + DateTime.Now.ToString()) as MemoryStream;
                //输出到指定文件夹
                string fileName = CommonHelper.Get_New12ByteGuid("Xls") + ".xls";
                return File(ms, "application/Excel", fileName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult ExportPdf()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
                string sPdfFile = "/Uploads/Temps/" + CommonHelper.Get_New12ByteGuid("Pdf") + ".pdf";
                string filePath = Server.MapPath(sPdfFile);
                DataTable dt = ConvertToDataTable();

                PdfileHelper pdfile = new PdfileHelper("A4b", 10, 10, 30, 50);
                pdfile.Open(new FileStream(filePath, FileMode.Create));
                string fontPath = Environment.GetEnvironmentVariable("WINDIR") + "\\FONTS\\SIMSUN.TTC,0";
                pdfile.SetBaseFont(fontPath);
                pdfile.AddParagraph("服 务 评 价 统 计 表", 20, 1, 10, 0, 0);

                pdfile.AddTable(dt, 12);

                sb.Clear();
                sb.Append("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                pdfile.AddParagraph(sb.ToString(), 12, 0, 0, 0, 0);

                pdfile.Close();

                return File(sPdfFile, "application/pdf");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}