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
    public class TStatisController : frmMainController
    {
        private string sStartDate
        {
            set { TempData["StartDate_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("StartDate_" + RouteData.Values["controller"].ToString());
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
                 
                ServiceInfoCollections serviceList = getServicesList();

                List<TicketStatsData> statsList = new List<TicketStatsData>();
                TicketStatsData ticketStats1 = null;

                if (string.IsNullOrEmpty(sStartDate))
                {
                    sStartDate = DateTime.Now.ToString("yyyy-MM-dd");
                } 

                if (serviceList != null && serviceList.Count > 0)
                {
                    foreach (ServiceInfo info in serviceList)
                    {
                        ticketStats1 = new TicketStatsData();

                        ticketStats1.sItemName = info.sServiceName;
                        ticketStats1.iTotalNum = 0;
                        //ticketStats1.iWaitingNum = PublicHelper.getVTicketCountByServiceNo(info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0");
                        //ticketStats1.iProcessingNum = PublicHelper.getVTicketCountByServiceNo(info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "1");
                        ticketStats1.iFinishedNum = 0;
                        ticketStats1.iAbsentNum = 0;
                        ticketStats1.iTransfNum = 0;
                        ticketStats1.dProcessedDate = DateTime.Now;
                        ticketStats1.sComments = "";

                        statsList.Add(ticketStats1);
                    }
                }

                //ServiceInfoCollections services = getServicesList();
                PagerHelper pager = new PagerHelper(1, 100, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", statsList);
                //stackHolder.Add("serviceList", services);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }

        private ServiceInfoCollections getServicesList()
        {
            try
            {
                int count = 0;
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "' And HaveChild=0 ");

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
             
            int StatDays = int.Parse(Request.Form["dStatTime"].ToString());

            sStartDate = DateTime.Now.AddDays(-StatDays).ToString("yyyy-MM-dd");

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
                dc = new DataColumn("业务名称");
                dt.Columns.Add(dc);
                dc = new DataColumn("总票数");
                dt.Columns.Add(dc);
                dc = new DataColumn("办理人数");
                dt.Columns.Add(dc);
                dc = new DataColumn("未到人数");
                dt.Columns.Add(dc);
                dc = new DataColumn("转移人数");
                dt.Columns.Add(dc);
                dc = new DataColumn("统计日期");
                dt.Columns.Add(dc);
                dc = new DataColumn("备注");
                dt.Columns.Add(dc);

                int totalCount = 0;
                ServiceInfoCollections serviceList = getServicesList();

                List<TicketStatsData> statsList = new List<TicketStatsData>();
                TicketStatsData ticketStats1 = null;

                if (serviceList != null && serviceList.Count > 0)
                {
                    foreach (ServiceInfo info in serviceList)
                    {
                        ticketStats1 = new TicketStatsData();

                        ticketStats1.sItemName = info.sServiceName;
                        ticketStats1.iTotalNum =0;
                        //ticketStats1.iWaitingNum = PublicHelper.getVTicketCountByServiceNo(info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0");
                        //ticketStats1.iProcessingNum = PublicHelper.getVTicketCountByServiceNo(info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "1");
                        ticketStats1.iFinishedNum = 0;
                        ticketStats1.iAbsentNum =0;
                        ticketStats1.iTransfNum =0;
                        ticketStats1.dProcessedDate = DateTime.Now;
                        ticketStats1.sComments = "";

                        statsList.Add(ticketStats1);
                    }
                }

                int i = 1;
                foreach (TicketStatsData info in statsList)
                {
                    dr = dt.NewRow();
                    dr["序号"] = i.ToString();
                    dr["业务名称"] = info.sItemName;
                    dr["总票数"] = info.iTotalNum;
                    dr["办理人数"] = info.iFinishedNum;
                    dr["未到人数"] = info.iAbsentNum ;
                    dr["转移人数"] = info.iTransfNum;
                    dr["统计日期"] = info.dProcessedDate.ToString("yyyy-MM-dd HH:mm:ss"); 
                    dr["备注"] = info.sComments;
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
                MemoryStream ms = ExcelHelper.RenderDataTableToExcel(dt, "业务服务统计表", "业务服务统计表", "业务服务统计表", "", "打印人：" + sSuNo, "打印时间：" + DateTime.Now.ToString()) as MemoryStream;
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
                pdfile.AddParagraph("业 务 服 务 统 计 表", 20, 1, 10, 0, 0);

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