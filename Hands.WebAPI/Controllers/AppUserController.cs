using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Hands.Common.Common;
using Hands.Service.AppUser;
using Hands.Service.Noor;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class AppUserController : ApiController
    {
        // GET: Noor
        private readonly IAppUserService _userService;
        private readonly IRegionService _regionService;
        private readonly ITaluqaService _taluqaService;
        private readonly IUnionCouncilService _unionCouncilService;
        private ApiResponse respoonse;
        private AppUser _noor;

        public AppUserController()
        {
            _userService = new AppUserService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();
            respoonse = new ApiResponse();
            _noor = new AppUser();
        }

        /// <summary>
        /// get app user by user Id
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>

        [ProjectIdAttributeController]
        public IHttpActionResult GetUserById(Int16 appUserId ,int? projectId = null)
        {
            respoonse = new ApiResponse();
            var appUser = _userService.GetByUSerType(appUserId, projectId);
            if (appUser != null)
            {
                _noor.GetMapAppUser(appUser, _regionService, _taluqaService, _unionCouncilService , _userService);
                respoonse.Success = true;
                respoonse.Result = _noor;
                return Json(respoonse);
            }
            respoonse.Result= new string[] { };
            respoonse.Success = true;
            return Json(respoonse);

        }
        /// <summary>
        /// this api is for get Active user bytype {marvi, lhv , real, shopkeeper, hcp}
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetByUserType(string userType, int? projectId = null)
        {
            respoonse = new ApiResponse();
            var appUser = new AppUser().PrepareViewList(_userService.GetAllActive(userType, projectId), _regionService, _taluqaService, _unionCouncilService , _userService);
            if (appUser != null)
            {
            
                respoonse.Success = true;
                respoonse.Result = appUser.OrderByDescending(x => x.AppUserId);
                return Json(respoonse);
            } 
            respoonse.Success = true;
            respoonse.Result = new string[] { }; 
            return Json(respoonse);
        }

        ///// Get marvi/noor by lhv ID
        [ProjectIdAttributeController]
        public IHttpActionResult GetBymarvibyLhvId(int lhvId, int? projectId = null)
        {
            respoonse = new ApiResponse();
            var marvisdata = _userService.GetMarviByLhvId(lhvId, projectId).OrderByDescending(x => x.marviId);
            if (marvisdata.Any())
            {
           
                respoonse.Success = true;
                respoonse.Result = marvisdata;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }



        [System.Web.Http.HttpPost]
        public IHttpActionResult Create(List<Hands.ViewModels.Models.appUser> model)
        {

            if (model != null && model.Count != 0)
            {
                foreach (var appUserdata in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.AppUser ModelToSAve = new Data.HandsDB.AppUser();
                        ModelToSAve.FullName = appUserdata.FullName;
                        ModelToSAve.Username = appUserdata.Username;
                        ModelToSAve.Pwd = appUserdata.Pwd;
                        ModelToSAve.Dob = appUserdata.Dob;
                        ModelToSAve.Address = appUserdata.Address;
                        ModelToSAve.ContactNumber = appUserdata.ContactNumber;
                        ModelToSAve.MaritalStatus = appUserdata.MaritalStatus;
                        ModelToSAve.FatherHusbandName = appUserdata.FatherHusbandName;
                        ModelToSAve.AgePerCnic = appUserdata.AgePerCnic;
                        ModelToSAve.Cnic = appUserdata.Cnic;
                        ModelToSAve.CnicValidtyEnd = appUserdata.CnicValidtyEnd;
                        ModelToSAve.Qualification = appUserdata.Qualification;
                        ModelToSAve.RegionId = appUserdata.RegionId;
                        ModelToSAve.TaluqaId = appUserdata.TaluqaId;
                        ModelToSAve.UnionCouncilId = appUserdata.UnionCouncilId;
                        ModelToSAve.UserType = appUserdata.UserType;
                        ModelToSAve.LhvAssigned = appUserdata.LhvAssigned;
                        ModelToSAve.FullNameSindhi = appUserdata.FullName;
                        ModelToSAve.FullNameUrdu = appUserdata.FullName;
                        ModelToSAve.PlainPassword = appUserdata.FullName;
                        ModelToSAve.IsActive = true;
                        ModelToSAve.PopulcationCovered = appUserdata.PopulcationCovered;
                        ModelToSAve.CreatedAt = DateTime.Now;
                        ModelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _userService.Insert(ModelToSAve);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }

                }
                _userService.SaveChanges();
                respoonse.Success = true;
                respoonse.Result = null;
                return Json(respoonse);
            }
            respoonse.Success = false;
            respoonse.Result = null;
            respoonse.Message = "Invalid Request";
            return Json(respoonse);

        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetAppUserById(int appUserId, int? projectId = null)
        {
            respoonse = new ApiResponse();
            var appUser = _userService.GetByAppUserId(appUserId, projectId).OrderByDescending(x => x.app_user_id);
            if (appUser.Any())
            {
             
                respoonse.Success = true;
                respoonse.Result = appUser;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { }; 
            return Json(respoonse);
        }

       
       
    }
}