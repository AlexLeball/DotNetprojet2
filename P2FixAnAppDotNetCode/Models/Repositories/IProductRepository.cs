using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        // Interface for the Product Repository class
        // Contains methods to get all products, update a product, and get a product by id
        List<Product> GetAllProducts();
        void UpdateProduct(Product product);
        Product GetProductById(int id);
    }
}

