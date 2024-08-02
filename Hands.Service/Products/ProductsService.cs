using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.Products
{
   public class ProductsService : ServiceBase<Data.HandsDB.Product>, IProductsService
    {
        public IEnumerable<Data.HandsDB.Product> GetAllProductsByType(string userType,int? projectId, out int totalRecords, int pageNo = 1, int pageSize = 10)
        {
            var query= _db.Products.Where(au => au.Producttype == userType && au.ProjectId==projectId && au.IsActive).ToList();
            totalRecords = query.Count;
            return query.OrderBy(x=>x.ProductId).Skip(pageSize * (pageNo - 1)).Take(pageSize); 
        }

        public List<Data.HandsDB.Product> GetAllProductByType(string userType)
        {
            return _db.Products.Where(au => au.Producttype == userType && au.IsActive && au.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public List<GetAllProductsReturnModel> GetAllProducts()
        {
            return _db.GetAllProducts();
        }

        public List<SpGetAppBibProductsWithCategoriesReturnModel> GetAllBlimisProductsWithcategory(int? projectId)
        {
            return _db.SpGetAppBibProductsWithCategories(projectId);
        }
    }
}
