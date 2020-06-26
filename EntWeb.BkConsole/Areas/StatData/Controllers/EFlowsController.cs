using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.StatData.Controllers
{
    public class EFlowsController : frmMainController
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
                    sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And EvaluateTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                }


                EvaluateFlowsBLL infoBLL = new EvaluateFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluateFlowsCollections flowsList = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, PageSize, sWhere);
                int totalCount = infoBLL.GetCountByCondition(sWhere);


                StafferInfoCollections staffs = getStaffsList();
                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", flowsList);
                stackHolder.Add("staffList", staffs);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }

        private StafferInfoCollections getStaffsList()
        {
            try
            { 
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetAllRecords();

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
            sWhere = "  BranchNo='" + PublicHelper.Get_BranchNo() + "' ";

            string sStafferNo = Request.Form["sStafferNo"].ToString();
            int StatDays = int.Parse(Request.Form["dStatTime"].ToString());
            string sKeyword = Request.Form["sKeyword"].ToString();

            if (!sStafferNo.Equals("00000000"))
            {
                sWhere += " And EvalStafferNo='" + sStafferNo + "' ";
            }

            sWhere += " And EvaluateTime Between '" + DateTime.Now.AddDays(-StatDays).ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";

            //if (!string.IsNullOrEmpty(sKeyword))
            //{
            //    sWhere += " And TicketNo Like '%" + sKeyword + "%' ";
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
                dc = new DataColumn("票号");
                dt.Columns.Add(dc);
                dc = new DataColumn("业务名称");
                dt.Columns.Add(dc);
                dc = new DataColumn("评价时间");
                dt.Columns.Add(dc);
                dc = new DataColumn("窗口名称");
                dt.Columns.Add(dc);
                dc = new DataColumn("员工姓名");
                dt.Columns.Add(dc);
                dc = new DataColumn("评价得分");
                dt.Columns.Add(dc);
                dc = new DataColumn("备注");
                dt.Columns.Add(dc); 

                int pageCount = 0;
                int pageSize = 100000;
                EvaluateFlowsBLL infoBLL = new EvaluateFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluateFlowsCollections infoList = infoBLL.GetRecordsByPaging(ref pageCount, 1, pageSize, sWhere);

                int i = 1;
                foreach (EvaluateFlows info in infoList)
                {
                    dr = dt.NewRow();
                    dr["序号"] = i.ToString();
                    dr["票号"] = info.sPFlowNo;
                    dr["业务名称"] = info.sPFlowNo;
                    dr["评价时间"] = info.dEvaluateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["窗口名称"] = PageHelper.getCounterInfoNameByNo(info.sEvalCounterNo);
                    dr["员工姓名"] = PageHelper.getStafferInfoNameByNo(info.sEvalStafferNo);
                    dr["评价得分"] = info.iEvalResult;
                    dr["备注"] = info.sComments;
                    dt.Rows.Add(dr);
                    i++;
                }
                return dt;
            }
            catch(Exception ex)
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
                MemoryStream ms = ExcelHelper.RenderDataTableToExcel(dt, "员工服务评价表", "员工服务评价表", "员工服务评价表", "", "打印人：" + sSuNo, "打印时间：" + DateTime.Now.ToString()) as MemoryStream;
                //输出到指定文件夹
                string fileName = CommonHelper.Get_New12ByteGuid("Xls") + ".xls";
                return File(ms, "application/Excel", fileName);
            }
            catch(Exception ex)
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
                pdfile.AddParagraph("员 工 服 务 评 价 表", 20, 1, 10, 0, 0);

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