  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
  using Hands.Common.Common;
  using Hands.Service.BlmisSells;
using Hands.WebAPI.Models;
  using Hands.WebAPI.Properties;

namespace Hands.WebAPI.Controllers
{
    public class BIBSellController : ApiController
    {
        // GET: BIBSell
        private readonly IBlmisSellsService _blmisSellsService;

        public BIBSellController()
        {
            _blmisSellsService = new BlmisSellsService();
        }
     
        public IHttpActionResult CreateBibSell(List<ViewModels.Models.BlmisSells.BlmisSells> model)
        {
            var respoonse = new ApiResponse();
            if (model != null && model.Count != 0)
            {
                foreach (var blmissell in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.BlmisSellHistory ModelToSAve = new Data.HandsDB.BlmisSellHistory();

                        ModelToSAve.SellDate = blmissell.SellDate;
                        ModelToSAve.Amount = blmissell.Amount;
                        ModelToSAve.CreatedAt = DateTime.Now;
                        ModelToSAve.UserId = blmissell.UserId;
                        ModelToSAve.ProductId = blmissell.ProductId;

                        ModelToSAve.YesterdaySelldate =DateTime.Parse(blmissell.YesterdayDate);
                        ModelToSAve.DayWiseAmount = blmissell.DayWiseAmount;
                        ModelToSAve.IsActive = true;
                        ModelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _blmisSellsService.Insert(ModelToSAve);
                    

                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                _blmisSellsService.SaveChanges();
                respoonse.Success = true;
                respoonse.Result = null;
                return Json(respoonse);
            }
            respoonse = new ApiResponse();
            respoonse.Success = false;
            respoonse.Result = null;
            respoonse.Message = "Invalid Requset";
            return Json(respoonse);

        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetBibSellData(int? projectId =null, int? marviId = null)
        {
            var respoonse = new ApiResponse();
            var bibsell = _blmisSellsService.GetBlmisSells(projectId,marviId).OrderByDescending(x => x.Id);
            if (bibsell != null)
            {
                respoonse.Success = true;
                respoonse.Result = bibsell;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
    }
}