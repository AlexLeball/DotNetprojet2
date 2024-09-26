using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    //needs to change type to list
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
