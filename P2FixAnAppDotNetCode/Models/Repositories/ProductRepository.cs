using System;
using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    /// <summary>
    /// The class that manages product data
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();
            GenerateProductData(); 
        }

        /// <summary>
        /// Generate the default list of products
        /// </summary>
        private void GenerateProductData()
        {
            int id = 0;
            _products.Add(new Product(++id, 10, 92.50, "Echo Dot", "(2nd Generation) - Black")); // 10 in stock
            _products.Add(new Product(++id, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable")); // 20 in stock
            _products.Add(new Product(++id, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear")); // 30 in stock
            _products.Add(new Product(++id, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone")); // 40 in stock
            _products.Add(new Product(++id, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone ")); // 50 in stock
        }


        /// Get all products from the inventory <summary>
        /// change the return type from array to list
        public List<Product> GetAllProducts()
        {
            return _products.Where(p => p.Stock > 0).OrderBy(p => p.Name).ToList();
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }


        public void UpdateProduct(Product product)
        {
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
