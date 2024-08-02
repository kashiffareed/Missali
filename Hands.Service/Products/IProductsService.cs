using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.Products
{
    public interface IProductsService : IService<Data.HandsDB.Product>
    {

        IEnumerable<Data.HandsDB.Product> GetAllProductsByType(string userType, int? projectId, out int totalRecords,
            int pageNo = 1, int pageSize = 10);

        List<Data.HandsDB.Product> GetAllProductByType(string userType);
        List<Data.HandsDB.GetAllProductsReturnModel> GetAllProducts();

        List<Data.HandsDB.SpGetAppBibProductsWithCategoriesReturnModel> GetAllBlimisProductsWithcategory(int? projectId);

    }
}
