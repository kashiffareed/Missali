using System.Linq;
using System.Web.Mvc;
using Hands.Service.ShopKeeperLocation;

namespace Hands.Web.Controllers
{
    public class ShopkeeperLocationController : ControllerBase
    {
       private IShopKeeperLocationService _shopKeeperLocation;
 
        public ShopkeeperLocationController()
        {
            _shopKeeperLocation = new ShopKeeperLocationService();        
        }
        public ActionResult ShopkeeperLocations()
        {
            var model = new Hands.ViewModels.Models.ShopkeeperLocation.ShopkeeperLocation();
            model.AppUsers = _shopKeeperLocation.GetAllShopkeeperLocations().ToList();
            return View(model);
        }
        public ActionResult LhvLocations()
        {
            var model = new Hands.ViewModels.Models.ShopkeeperLocation.ShopkeeperLocation();
            model.AppUsers = _shopKeeperLocation.GetAllLhvLocations().ToList();
            return View(model);
        }
        public ActionResult MarviLocations()
        {
            var model = new Hands.ViewModels.Models.ShopkeeperLocation.ShopkeeperLocation();
            model.AppUsers = _shopKeeperLocation.GetAllMarviLocations().ToList();
            return View(model);
        }
        public ActionResult HcpLocations()
        {
            var model = new Hands.ViewModels.Models.ShopkeeperLocation.ShopkeeperLocation();
            model.AppUsers = _shopKeeperLocation.GetAllHcpLocations().ToList();
            return View(model);
        }
    }
}