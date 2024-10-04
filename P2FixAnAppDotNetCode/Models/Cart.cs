using System;
using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Models
{
    public class Cart : ICart
    {
        // Service to update product stocks
        public readonly IProductService _productService;

        // List to store cart lines
        private List<CartLine> cartLines = new List<CartLine>();

        // List to directly access cart lines
        public IEnumerable<CartLine> Lines => cartLines;

        // Constructor to initialize the product service
        public Cart(IProductService productService)
        {
            _productService = productService;
        }

        /// Adds a product to the cart or increments its quantity if already added.
        public void AddItem(Product product, int quantity)
        {
            // Validate product and quantity
            if (product == null || quantity < 1)
            {
                throw new ArgumentException("Sorry, your product is not available or quantity is less than 1.");
            }

            // Check if enough stock is available before adding to the cart
            if (product.Stock < quantity)
            {
                Console.WriteLine($"Not enough stock available for Product ID: {product.Id}. Current Stock: {product.Stock}");
                throw new InvalidOperationException("Not enough stock available.");
            }

            // Find the current cart line for the product
            var currentCartLine = FindCartLine(product.Id);

            if (currentCartLine != null)
            {
                // Increment the quantity if the product is already in the cart
                currentCartLine.Quantity += quantity;
                Console.WriteLine($"Increased quantity for Product ID: {product.Id} to {currentCartLine.Quantity}");

                // Only update stock once per addition
                _productService.UpdateProductStocks(product.Id, quantity); // Update only once if adding or increasing
            }
            else
            {
                // Add the new cart line if the product is not in the cart
                cartLines.Add(new CartLine { Product = product, Quantity = quantity });
                Console.WriteLine($"Added Product ID: {product.Id} to cart with quantity {quantity}");

                // Only update stock once when adding new item
                _productService.UpdateProductStocks(product.Id, quantity);
            }
        }


        /// Finds a product in the cart accessed by id and returns it.    
        public Product FindProductInCartLines(int productId)
        {
            var cartLine = FindCartLine(productId);
            return cartLine?.Product; // Returns null if cartLine is not found
        }

        // Access CartLine where the product is stored.
        private CartLine FindCartLine(int productId)
        {
            return cartLines.FirstOrDefault(cartLine => cartLine.Product.Id == productId);
        }

        /// Removes a product from the cart by accessing it through the productId.
        public void RemoveLine(Product product)
        {
            Console.WriteLine($"Removing Product ID: {product.Id} from the cart.");
            cartLines.RemoveAll(i => i.Product.Id == product.Id);
        }

        /// Gets the total value of the cart by summing the price of all products in the cart.
        public double GetTotalValue()
        {
            double totalValue = cartLines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);
            Console.WriteLine($"Total value of the cart: {totalValue}");
            return totalValue;
        }

        /// Gets the average value of the cart
        public double GetAverageValue()
        {
            // Handles if cart is empty to avoid division by zero
            if (cartLines.Count == 0) return 0;

            double averageValue = GetTotalValue() / cartLines.Count;
            Console.WriteLine($"Average value of the cart: {averageValue}");
            return averageValue;
        }

        /// Clears the cart of all added products.
        public void Clear()
        {
            Console.WriteLine("Clearing the cart.");
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
