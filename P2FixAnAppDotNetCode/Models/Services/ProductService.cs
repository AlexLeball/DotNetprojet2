using System.Collections.Generic;
using P2FixAnAppDotNetCode.Models.Repositories;
using System.Linq;
using System;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// This class provides services to manages the products
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// Get all product from the inventory
        ///Change the return type from array to list
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();                ;
        }

        /// Get a product form the inventory by its id 
       public Product GetProductById(int id)
        {
            return GetAllProducts().FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProductStocks(int productId, int quantityToRemove)
        {
            // Get the product from the repository
            var product = GetAllProducts().FirstOrDefault(p => p.Id == productId);

            // Validate product and quantity
            if (quantityToRemove < 1 || product == null)
            {
                throw new ArgumentException("Invalid product or quantity to remove.");
            }

            // Check if there is enough stock before reducing
            if (product.Stock < quantityToRemove)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            // Update the stock of the product
            product.Stock -= quantityToRemove;
        }


        //debugging currently 
        public void UpdateProductQuantities(Cart cart)
        // iterate through the cart and update the product quantities in the inventory
        {
            foreach (var line in cart.Lines)
            {
                UpdateProductStocks(line.Product.Id, line.Quantity); 
            }
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product); 
        }
    }
}
