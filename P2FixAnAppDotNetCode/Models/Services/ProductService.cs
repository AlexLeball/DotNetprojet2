using System;
using System.Collections.Generic;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// This class provides services to manage the products
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// Get all products from the inventory
        public List<Product> GetAllProducts()
        {
            Console.WriteLine("Fetching all products from the repository.");
            return _productRepository.GetAllProducts();
        }

        /// Get a product from the inventory by its ID
        public Product GetProductById(int id)
        {
            Console.WriteLine($"Fetching product with ID: {id}");
            return _productRepository.GetProductById(id);
        }

        public void UpdateProductStocks(int productId, int quantity)
        {
            Console.WriteLine($"Attempting to update stock for Product ID: {productId}, Quantity: {quantity}");

            var product = GetProductById(productId);
            if (product != null && product.Stock >= quantity)
            {
                product.Stock -= quantity;
                Console.WriteLine($"Updated stock for Product ID: {productId}. New Stock: {product.Stock}");

                _productRepository.UpdateProduct(product); // Ensure this saves to the DB
                Console.WriteLine($"Product ID: {productId} stock saved to the repository.");
            }
            else
            {
                Console.WriteLine($"Not enough stock available for Product ID: {productId}. Current Stock: {product?.Stock ?? 0}");
            }
        }

        public void UpdateProduct(Product product)
        {
            Console.WriteLine($"Updating product ID: {product.Id}");
            _productRepository.UpdateProduct(product);
        }

        public void UpdateProductQuantities(Cart cart)
        {
            Console.WriteLine("Updating product quantities based on the cart.");
            foreach (var line in cart.Lines)
            {
                UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }
    }
}
