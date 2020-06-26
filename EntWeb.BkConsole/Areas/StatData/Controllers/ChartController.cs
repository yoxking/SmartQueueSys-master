using ECharts;
using ECharts.Entities;
using ECharts.Entities.axis;
using ECharts.Entities.feature;
using ECharts.Entities.series;
using ECharts.Entities.series.data;
using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace EntWeb.BkConsole.Areas.StatData.Controllers
{
    public class ChartController : frmMainController
    {
        // GET: StatData/Chart
        public override ActionResult Index()
        {
            return View();
        }

        private ToolBox ToolBox(OrientType orient = OrientType.horizontal)
        {

            ToolBox tool = new ECharts.Entities.ToolBox();
            var feature = new Feature();
            feature.DataView().Show(true).ReadOnly(false);
            feature.Mark().Show(true);
            feature.Restore().Show(true);
            feature.MagicType().Show(true).Type("line", "bar", "stack", "tiled");
            feature.SaveAsImage().Show(true);
            tool.Show(true).SetFeature(feature);
            tool.Orient(orient);
            return tool;
        }

        [AcceptVerbs("GET", "POST")]
        public string getCounterStatistics()
        {
            try
            {
                IList<string> counterNoList = new List<string>();
                IList<string> counterNameList = new List<string>();

                CounterInfoBLL counterBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfoCollections counterColl = counterBLL.GetAllRecords();

                if (counterColl != null && counterColl.Count > 0)
                {
                    foreach (CounterInfo counter in counterColl)
                    {
                        counterNoList.Add(counter.sCounterNo);
                        counterNameList.Add(counter.sCounterAlias);
                    }
                };

                ChartOption option = new ChartOption();
                //option.Title().Text("窗口统计").SubText("虚构").X(HorizontalType.center);
                option.ToolTip().Trigger(TriggerType.item).Formatter(new JRaw(@"{a} <br/>{b} : {c} ({d}%)").ToString());
                option.Legend().Orient(OrientType.vertical).X(HorizontalType.left).SetData(new List<object>(counterNameList.ToList()));
                option.ToolBox(ToolBox(OrientType.vertical));
                option.ToolBox().X(HorizontalType.right).Y(HorizontalType.center);
                option.calculable = true;

                var pie = new Pie("窗口办理统计");

                pie.Radius("55%").Center(new List<string>() { "50%", "60%" });

                if (counterNoList.Count > 0)
                {
                    object[] myData = new object[counterNoList.Count];
                    int temp = 0;
                    for (int i = 0; i < counterNoList.Count; i++)
                    {
                        //temp = PublicHelper.getVTicketCountByCounterNo(counterNoList[i], DateTime.Now, DateTime.Now.AddDays(1), 0);
                        myData[i] = new PieData<int>(temp, counterNameList[i]);
                    }
                    pie.Data(myData);
                }

                option.Series(pie);

                var result = JsonTools.ObjectToJson2(option);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [AcceptVerbs("GET", "POST")]
        public string getEvaluateStatistics()
        {
            try
            {
                IList<string> StafferNoList = new List<string>();
                IList<string> StafferNameList = new List<string>();

                StafferInfoBLL staffBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfoCollections staffColl = staffBLL.GetAllRecords();

                if (staffColl != null && staffColl.Count > 0)
                {
                    foreach (StafferInfo staff in staffColl)
                    {
                        StafferNoList.Add(staff.sStafferNo);
                        StafferNameList.Add(staff.sStafferName);
                    }
                };

                ChartOption option = new ChartOption();
                //option.Title().Text("窗口统计").SubText("虚构").X(HorizontalType.center);
                option.ToolTip().Trigger(TriggerType.item).Formatter(new JRaw(@"{a} <br/>{b} : {c} ({d}%)").ToString());
                option.Legend().Orient(OrientType.vertical).X(HorizontalType.left).SetData(new List<object>(StafferNameList.ToList()));
                option.ToolBox(ToolBox(OrientType.vertical));
                option.ToolBox().X(HorizontalType.right).Y(HorizontalType.center);
                option.calculable = true;

                var pie = new Pie("员工评价统计");

                pie.Radius("55%").Center(new List<string>() { "50%", "60%" });

                if (StafferNoList.Count > 0)
                {
                    object[] myData = new object[StafferNoList.Count];
                    int temp = 0;
                    for (int i = 0; i < StafferNoList.Count; i++)
                    {
                        //temp = PublicHelper.getEvalScoreByStafferNo(StafferNoList[i], DateTime.Now, DateTime.Now.AddDays(1), -1);
                        myData[i] = new PieData<int>(temp, StafferNameList[i]);
                    }
                    pie.Data(myData);
                }

                option.Series(pie);

                var result = JsonTools.ObjectToJson2(option);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [AcceptVerbs("GET", "POST")]
        public string getServiceStatistics()
        {
            try
            {
                IList<string> weeks = new List<string>();
                IList<int> datas = new List<int>();
                Dictionary<string, object> dictValue = new Dictionary<string, object>();

                for (int i = 0; i < 7; i++)
                {
                    weeks.Add(CalendarHelper.GetCNDayOfWeek(DateTime.Now.AddDays(i - 6)));
                }

                int pageCount = 0;
                ServiceInfoBLL serviceBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections serviceColl = serviceBLL.GetRecordsByPaging(ref pageCount, 1, 100, " HaveChild=0 ");

                if (serviceColl != null && serviceColl.Count > 0)
                {
                    foreach (ServiceInfo service in serviceColl)
                    {
                        datas.Clear();
                        for (int i = 0; i < 7; i++)
                        {
                            //datas.Add(PublicHelper.getVTicketCountByServiceNo(service.sServiceNo, DateTime.Now.AddDays(i - 6).ToString(),DateTime.Now.AddDays(i - 5).ToString(), "-1"));
                        }
                        dictValue.Add(service.sServiceName, datas);
                    }
                }

                ChartOption option = new ChartOption();

                option.title = new Title()
                {
                    show = true,
                    text = "业务统计(周)",
                    subtext = "业务服务数据"
                };


                option.tooltip = new ToolTip()
                {
                    trigger = TriggerType.axis
                };

                option.calculable = true;

                option.xAxis = new List<Axis>()
            {
                new CategoryAxis()
                {                    
                    type = AxisType.category,                    
                    boundaryGap= false,
                    data = new List<object>(weeks)
                }
            };

                option.yAxis = new List<Axis>()
            {
                new ValueAxis()
                {
                    type = AxisType.value,
                    axisLabel = new AxisLabel(){
                     formatter=new JRaw("{value}").ToString()
                    }
                }
            };

                IList<object> legendList = new List<object>();
                IList<object> seriesList = new List<object>();
                Line myLine;

                foreach (string mykey in dictValue.Keys)
                {
                    legendList.Add(mykey);
                    myLine = new Line()
                    {
                        name = mykey,
                        type = ChartType.line,
                        data = dictValue[mykey],
                        markLine = new MarkLine()
                        {
                            data = new List<object>()
                                {
                                     new MarkData()
                                     {
                                          name = "平均值",
                                          type = MarkType.average
                                     }
                                }
                        }
                    };

                    seriesList.Add(myLine);
                }

                option.legend = new Legend()
                {
                    data = legendList
                };

                option.series = seriesList;

                var result = JsonTools.ObjectToJson2(option);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [AcceptVerbs("GET", "POST")]
        public string getDefaultStatistics()
        {
            try
            {
                int pageCount = 0;
                Dictionary<string, string> dictValue = new Dictionary<string, string>();
                ServiceInfoBLL serviceBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections serviceColl = serviceBLL.GetRecordsByPaging(ref pageCount, 1, 100, " HaveChild=0 ");

                if (serviceColl != null && serviceColl.Count > 0)
                {
                    foreach (ServiceInfo service in serviceColl)
                    {
                        dictValue.Add(service.sServiceNo, service.sServiceName);
                    }
                }

                ChartOption option = new ChartOption();
                option.Title().Text("综合评价统计").SubText("按评价级别统计");
                option.ToolTip().Trigger(TriggerType.axis);

                option.ToolBox(ToolBox(OrientType.vertical));
                option.calculable = true;

                var pd1 = new IndicatorData();
                pd1.Text("非常满意").Max(50000);
                var pd2 = new IndicatorData();
                pd2.Text("满意").Max(50000);
                var pd3 = new IndicatorData();
                pd3.Text("一般").Max(50000);
                var pd4 = new IndicatorData();
                pd4.Text("不满意").Max(50000);
                var pd5 = new IndicatorData();
                pd5.Text("总评分").Max(50000);
                var pd6 = new IndicatorData();
                pd6.Text("未评分").Max(50000);

                Polar myPolar = new Polar();
                myPolar.Indicator(pd1, pd2, pd3, pd4, pd5, pd6);


                IList<object> legendList = new List<object>();
                IList<PolarData> dataList = new List<PolarData>();

                option.Polar(myPolar);

                PolarData myPolarData;
                int i = 0;
                foreach (string mykey in dictValue.Keys)
                {
                    legendList.Add(dictValue[mykey]);
                    myPolarData = new PolarData(dictValue[mykey]);
                    myPolarData.Value(4300 + i, 10000 + i, 28000 + i, 35000 + i, 50000 + i, 19000 + i);
                    dataList.Add(myPolarData);
                    i += 100;
                }

                option.legend = new Legend()
                {
                    orient = OrientType.vertical,
                    x = HorizontalType.right,
                    y = VerticalType.bottom,
                    data = legendList
                };

                Radar myRadar = new Radar("综合评价统计");
                myRadar.Data(dataList.ToArray<PolarData>());
                option.series = myRadar;

                var result = JsonTools.ObjectToJson2(option);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}