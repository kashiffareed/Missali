using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Hands.Service.AspnetUsers;
using Hands.Service.Menu;
using Hands.Service.UserMenuAccess;
using Hands.ViewModels.Models.Model;
using Hands.ViewModels.Models.UserAccessMenuModel;

namespace Hands.Web.Controllers
{
    public class UserAccessMenuController : ControllerBase
    {
        // GET: UserAccessMenu

        private readonly IMenuService _menuService;
        private readonly IAspnetUserService _userService;
        private readonly IUserMenuAccessService _menuAccessService;

        public UserAccessMenuController()
        {
            _menuService = new MenuService();
            _menuAccessService = new UserMenuAccessService();
            _userService = new AspnetUserService();

        }
        public ActionResult Index()
        {
            var model = new UserAccessMenu();
            model.Users = _userService.GetAllActive();
            return View(model);
        }   


        [HttpPost]
        public ActionResult Indexx(string data1, string UserId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            int[] selectedMenus = js.Deserialize<int[]>(data1);
            List<Hands.ViewModels.Models.UserAccessMenuModel.UserAccessMenu> userAccessMenus = new List<Hands.ViewModels.Models.UserAccessMenuModel.UserAccessMenu>();

            List<Hands.Data.HandsDB.UserMenuAccess> access = _menuAccessService.getAllByUserId(UserId).ToList();
            if (access.Count > 0)
            {
                foreach (var remove in access)
                {
                    _menuAccessService.Remove(remove);
                }

            }

            foreach (var menus in selectedMenus)
            {
                var model = new Hands.Data.HandsDB.UserMenuAccess();
                model.UserId = UserId;
                model.MenuId = menus;
                model.IsActive = true;
                _menuAccessService.Insert(model);
            }
            _menuAccessService.SaveChanges();
            return RedirectToAction("Index");
        }

        public string DocumentCategoryHierarchy()
        {
            var menus = _menuService.GetAll();
            var list = (
                from c in menus
                select new Menu
                {
                    id = c.Id.ToString(),
                    parent = c.ParentId == (int?)0 ? "#" : c.ParentId.ToString(),
                    text = c.Tittle
                }).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }
    }
}