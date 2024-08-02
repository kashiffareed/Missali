using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Mwra;
using Hands.Service.Real;
using Hands.ViewModels.Models.RealTimeCheckList;
using Hands.WebAPI.Models;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class RealTimeCheckListController : ApiController
    {
        private IRealService _realService;
        public RealTimeCheckListController()
        {
            _realService = new RealService();
        }
        // GET: RealTime
        [ProjectIdAttributeController]
        public IHttpActionResult GetRealTimeCheckListData(int userId, int regionId,int? projectId = null   )
        {
            var response = new ApiResponse();
            var realTimeCheckListData = _realService.GetRealTimeCheckListDataForService(userId, regionId,projectId);

            var clientCount = realTimeCheckListData.ResultSet1.Count;
            var lhvCount = realTimeCheckListData.ResultSet2.Count;
            var noorCount = realTimeCheckListData.ResultSet3.Count;

            var clientList = realTimeCheckListData.ResultSet1;
            var lhvList = realTimeCheckListData.ResultSet2;
            var noorList = realTimeCheckListData.ResultSet3;


            var result = new { clientCount, lhvCount, noorCount, clientList, noorList, lhvList };
            if (realTimeCheckListData != null)
            {
                response = new ApiResponse();
                response.Success = true;
                response.Result = result;

                return Json(response, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetClientlist( int regionId, int? projectId = null)
            {
            var response = new ApiResponse();
            var realTimeCheckListData = _realService.GetCleintList( regionId, projectId);

            var clientCount = realTimeCheckListData.ResultSet1.Count;
            var lhvCount = realTimeCheckListData.ResultSet2.Count;
            var noorCount = realTimeCheckListData.ResultSet3.Count;

            var clientList = realTimeCheckListData.ResultSet1;
            var lhvList = realTimeCheckListData.ResultSet2;
            var noorList = realTimeCheckListData.ResultSet3;


            var result = new { clientCount, lhvCount, noorCount, clientList, noorList, lhvList };
            if (realTimeCheckListData != null)
            {
                response = new ApiResponse();
                response.Success = true;
                response.Result = result;

                return Json(response, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }



    }
}