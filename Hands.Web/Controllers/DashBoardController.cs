using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.Dashboard;
using Hands.Service.DashboardCount;
using Hands.Service.Mwra;
using Hands.ViewModels.Models;

namespace Hands.Web.Controllers
{
    public class DashBoardController : ControllerBase
    {
        // GET: DashBoard
       private IMwraService _mwraService;
        private readonly IDashboardService _dashboardService;
        private readonly IDashboardCountService _dashboardCountService;

        public DashBoardController( )
        {
            _mwraService = new MwraService();
            _dashboardService = new DashboardService();
            _dashboardCountService =new DashboardCountService();;
        }


        public ActionResult Index()
        {
            var model = new DashBoard();
            model.mawra = _mwraService.GetMWRAsCount();
            return View(model);
        }


        //public JsonResult DashboardResult()
        //{
        //    var models = new Hands.ViewModels.Models.Dashboardcount.Dashboardcount();
        //    models.Dashboardlist = _dashboardCountService.GetDashboardList();
            
        //    return Json(models, JsonRequestBehavior.AllowGet);
        //}

    }
}

