using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Noor;
using Hands.Service.Regions;

namespace Hands.Web.Controllers
{
    public class testController : ControllerBase
    {
        // GET: test
     
        private readonly IRegionService _regionService;
        private readonly INoorService _noorService;


        public testController()
        {
     
            _regionService = new RegionService();
                   _noorService = new NoorService();
            


        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult content()
        {
            var model = new Hands.ViewModels.Models.Mwra();
            model.Regions = _regionService.GetAll();
            model.Marvis = _noorService.GetAll();
            return View(model);
          
        }
    }
}