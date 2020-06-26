using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.StatData.Controllers
{
    public class SheetController : frmMainController
    {
        //
        // GET: /StatData/Sheet/

        public override ActionResult Index()
        {
            return View();
        }

    }
}
