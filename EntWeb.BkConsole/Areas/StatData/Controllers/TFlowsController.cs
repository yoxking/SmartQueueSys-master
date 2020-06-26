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
    public class TFlowsController : frmMainController
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

        // GET: StatData/TFlows
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: StatData/TFlows
        public override ActionResult List()
        {
            try
            {
                int PageCount = 0;
                int PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                int PageSize = 20;
                if (string.IsNullOrEmpty(sWhere))
                {
                    sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                }


                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsCollections flowsList = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, PageSize, sWhere);
                int totalCount = infoBLL.GetCountByCondition(sWhere);

                List<TicketFlowData> ticketsList = new List<TicketFlowData>();
                TicketFlowData ticket = null;

                if (flowsList != null && flowsList.Count > 0)
                {
                    foreach (ViewTicketFlows flow in flowsList)
                    {
                        ticket = new TicketFlowData();

                        ticket.sTicketNo = flow.sTicketNo;
                        ticket.sServiceName = PageHelper.getServiceInfoNameByNo(flow.sServiceNo);
                        //ticket.dQueueTime = flow.dQueueTime;
                        //ticket.sProcessStatus = flow.iProcessStatus.ToString();
                        //ticket.dStartTime = flow.dStartTime;
                        //ticket.dFinishedTime = flow.dFinishedTime;
                        ticket.sProcessedCounterName = PageHelper.getCounterInfoNameByNo(flow.sProcessedCounterNo);
                        ticket.sProcessedStafferName = PageHelper.getStafferInfoNameByNo(flow.sProcessedStafferNo);
                        ticket.sComments = flow.sComments;

                        ticketsList.Add(ticket);
                    }
                }

                ServiceInfoCollections services = getServicesList();
                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", ticketsList);
                stackHolder.Add("serviceList", services);
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
                return infoBLL.GetRecordsByPaging(ref count, 1, 100, "  BranchNo='" + PublicHelper.Get_BranchNo() + "' And HaveChild=0 ");

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
            sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' ";

            string sServiceNo = Request.Form["sServiceNo"].ToString();
            int StatDays = int.Parse(Request.Form["dStatTime"].ToString());
            string sKeyword = Request.Form["sKeyword"].ToString();

            if(!sServiceNo.Equals("00000000"))
            {
                sWhere += " And ServiceNo='"+ sServiceNo + "' ";
            }

            sWhere += " And EnqueueTime Between '" + DateTime.Now.AddDays(-StatDays).ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
            
            if(!string.IsNullOrEmpty(sKeyword))
            {
                sWhere += " And TicketNo Like '%"+ sKeyword + "%' ";
            }

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
                dc = new DataColumn("票号");
                dt.Columns.Add(dc);
                dc = new DataColumn("业务名称");
                dt.Columns.Add(dc);
                dc = new DataColumn("取票时间");
                dt.Columns.Add(dc);
                dc = new DataColumn("处理结果");
                dt.Columns.Add(dc);
                dc = new DataColumn("办理时间");
                dt.Columns.Add(dc);
                dc = new DataColumn("办理窗口");
                dt.Columns.Add(dc);
                dc = new DataColumn("办理人员");
                dt.Columns.Add(dc);

                int pageCount = 0;
                int pageSize = 100000;
                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsCollections infoList = infoBLL.GetRecordsByPaging(ref pageCount, 1, pageSize, sWhere);

                int i = 1;
                foreach (ViewTicketFlows info in infoList)
                {
                    dr = dt.NewRow();
                    dr["序号"] = i.ToString();
                    //dr["票号"] = PublicHelper.getVTicketNoByNo(info.sPFlowNo);
                    dr["票号"] = "";
                    dr["业务名称"] = PageHelper.getServiceInfoNameByNo(info.sServiceNo);
                    dr["取票时间"] = info.dEnqueueTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //dr["处理结果"] = info.iProcessStatus;
                    //dr["办理时间"] = info.dStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["办理窗口"] = PageHelper.getCounterInfoNameByNo(info.sProcessedCounterNo);
                    dr["办理人员"] = PageHelper.getStafferInfoNameByNo(info.sProcessedStafferNo);
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
                MemoryStream ms = ExcelHelper.RenderDataTableToExcel(dt, "业务服务报表", "业务服务报表", "业务服务报表", "", "打印人：" + sSuNo, "打印时间：" + DateTime.Now.ToString()) as MemoryStream;
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
                pdfile.AddParagraph("业 务 服 务 报 表", 20, 1, 10, 0, 0);

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