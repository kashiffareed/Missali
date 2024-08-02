using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppMenu;
using Hands.Service.AppUser;
using Hands.Service.AssignMenuToApp;
using Hands.Service.MultiProjectAppDetails;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using AppUser = Hands.WebAPI.Models.AppUser;


namespace Hands.WebAPI.Controllers
{
    public class LoginApiController : ApiController
    {
        private IAppUserService _userService;
        private IRegionService _regionService;
        private ITaluqaService _taluqaService;
        private IUnionCouncilService _unionCouncilService;
        private IMultiProjectAppDetailsService _multiProjectAppDetailsService;
        private IAssignMenuToAppService _assignMenuToAppService;
        private IAppMenuService _appMenuService;
        private ApiResponse respoonse;
        private AppUser appUser;

        public LoginApiController()
        {
            _userService = new AppUserService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();
            _assignMenuToAppService = new AssignMenuToAppService();
            _multiProjectAppDetailsService = new MultiProjectAppDetailsService();
            _appMenuService = new AppMenuService();
            respoonse = new ApiResponse();
            appUser = new AppUser();
        }

        // GET: LoginApi
        public IHttpActionResult LoginAppUser(string username, string password)
        {
            var userData = _userService.GetLoginAppUser(username, password);
            if (userData != null)
            {
                appUser.GetMapAppUser(userData, _regionService, _taluqaService, _unionCouncilService, _userService);
                appUser.PrimaryColor = _multiProjectAppDetailsService.GetAll().Where(x => x.ProjectId == userData.ProjectId).Select(x=>x.PrimaryColor).FirstOrDefault();
                appUser.SecoundaryColor = _multiProjectAppDetailsService.GetAll().Where(x => x.ProjectId == userData.ProjectId).Select(x => x.SecoundaryColor).FirstOrDefault();
                appUser.HeadingColor = _multiProjectAppDetailsService.GetAll().Where(x => x.ProjectId == userData.ProjectId).Select(x => x.HeadingColor).FirstOrDefault();
                appUser.SubHeadingColor = _multiProjectAppDetailsService.GetAll().Where(x => x.ProjectId == userData.ProjectId).Select(x => x.SubHeadingColor).FirstOrDefault();
                respoonse.Success = true;
                respoonse.Result = appUser;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Message = CommonConstant.ResponseEnum.InvalidCredentials.ToString();
            return Json(respoonse); 
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetMultiProjectAppDetail(string RoleName,int? projectId = null)
        {
            var response = new ApiResponse();
            var projectMenu = _assignMenuToAppService.GetAll().Where(x => x.ProjectId == projectId && x.ParentId != 0 && x.RoleName == RoleName).Select(x => new
            {
                x.ProjectId,
                x.RoleName,
                x.MenuId,
                x.Title,
                TitleUrdu = _appMenuService.GetAll().Where(a=>a.Id == x.MenuId).Select(a=>a.TitleUrdu).FirstOrDefault(),
                TitleSindhi = _appMenuService.GetAll().Where(a => a.Id == x.MenuId).Select(a => a.TitleSindhi).FirstOrDefault(),
                x.ClickEvent,
                x.NavImg
            }).ToList();

            var multiProjectAppDetails = _multiProjectAppDetailsService.GetMultiProjectAppDetails(projectId).FirstOrDefault();
            var ProjectId = multiProjectAppDetails.ProjectId;
            var projectName = multiProjectAppDetails.ProjectName;
            var primaryColor = multiProjectAppDetails.PrimaryColor;
            var secoundaryColor = multiProjectAppDetails.SecoundaryColor;
            var headingColor = multiProjectAppDetails.HeadingColor;
            var subHeadingColor = multiProjectAppDetails.SubHeadingColor;
            var projectImagePath = multiProjectAppDetails.ProjectImagePath;

            if (projectMenu != null)
            {
                response.Success = true;
                response.Result = new
                {
                    projectMenu,
                    ProjectId,
                    projectName,
                    primaryColor,
                    secoundaryColor,
                    headingColor,
                    subHeadingColor,
                    projectImagePath
                };
                response.Success = true;
                return Json(response);
            }
            return Json(response);
        }
    }
}