using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.AppUser;
using Hands.Service.Mwra;
using Hands.Service.Session;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class DashboardController : ApiController
    {
        // GET: Dashboard 
        private readonly ISessionService _sessionService;
        private readonly IAppUserService _userService;
        private readonly IMwraService _mwraService;
        private ApiResponse respoonse;

        public DashboardController()
        {
            _sessionService = new SessionService();
            _userService = new AppUserService();
             respoonse = new ApiResponse();
            _mwraService = new MwraService();
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetCountsByLhvId(int lhvId, int? projectId = null)
        {
            if (lhvId > 0)
            {
                Dashboard dashboard = new Dashboard();
                var userType = _userService.GetByUSerType(lhvId,projectId)?.UserType;
                 
                if (userType == "lhv")
                {
                    dashboard.MarviCount = _userService.GetMwrviCountByLhvId(lhvId, projectId).Count();
                    var mwra = _mwraService.GetMwrasByLhvIdCount(lhvId, projectId);
                    dashboard.MwraCount = mwra.Count();
                    dashboard.MwraclientCount = mwra.Count(x => x.is_client == 1);
                    dashboard.Lhvcount = 0;
                }
                else if(userType == "realtime")
                {
                    //var appUsers = _userService.GetAllActive().ToList();
                    //var mwraclient = ;
                    dashboard.Lhvcount = _userService.GetAll("lhv", projectId).Count();
                    dashboard.MarviCount = _userService.GetAll("marvi", projectId).Count();
                    dashboard.MwraCount = 0;
                    dashboard.MwraclientCount = _mwraService.GetAllActive().Where(x => x.IsClient == 1 && x.ProjectId == projectId).Count();
                }
                else if (userType == "marvi")
                {

                    dashboard.Lhvcount = 0;
                    dashboard.MarviCount = 0;
                    var mwra = _mwraService.GetMwrasByMarviIdCount(lhvId, projectId);
                    var sessionCount = _sessionService.GetAll().Where(x=>x.IsActive == true && x.ProjectId == projectId && x.MarviId == lhvId);
                    dashboard.MwraCount = mwra.Count();
                    dashboard.MwraclientCount = mwra.Count(x => x.is_client == 1);
                    dashboard.sessionCount = sessionCount.Count();
                }
                respoonse = new ApiResponse();
                respoonse.Success = true;
                respoonse.Result = dashboard;
                return Json(respoonse);
            }
            return NotFound();
        }

    }
}
