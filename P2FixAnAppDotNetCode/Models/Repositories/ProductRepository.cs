using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public ProductRepository()
        {
            // Initialize the in-memory product list
            _products = GenerateProductData();
        }

        private List<Product> GenerateProductData()
        {
            // Initialize the in-memory product list
            int id = 0;
            return new List<Product>
            {
                new Product(++id, 10, 92.50, "Echo Dot", "(2nd Generation) - Black"),
                new Product(++id, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable"),
                new Product(++id, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear"),
                new Product(++id, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone"),
                new Product(++id, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone")
            };
        }

        // Get all products in the repository
        public List<Product> GetAllProducts()
        {
            return _products.Where(p => p.Stock > 0).OrderBy(p => p.Name).ToList();
        }

        // Get a product by its id from the repository (or return null if not found)
        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        // Update a product in the repository (or throw an exception if not found) 
        public void UpdateProduct(Product product)
        {
            // Find the product by id
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock; // Update stock if needed
            }
            else
            {
                throw new ArgumentException("Product not found.");
            }
        }
    }
}
