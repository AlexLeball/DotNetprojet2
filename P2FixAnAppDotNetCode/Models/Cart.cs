using System;
using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models.Services;


namespace P2FixAnAppDotNetCode.Models
{
    public class Cart : ICart
    {
        // call service to update product stocks
        private readonly IProductService _productService;

        //list to store cart lines
        private List<CartLine> cartLines = new List<CartLine>();

        // list to directly access cart lines
        public IEnumerable<CartLine> Lines => cartLines;

        //default constructor

        // constructor to initialize the product service
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
                throw new ArgumentException("Sorry, your product is not available or quantity is less than 1");
            }

            // Check if enough stock is available before adding to the cart
            if (product.Stock < quantity)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            var currentCartLine = FindCartLine(product.Id);
            if (currentCartLine != null)
            {
                // Increment the quantity if the product is already in the cart
                currentCartLine.Quantity += quantity;
            }
            else
            {
                // Add the new cart line if the product is not in the cart
                cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }

            // Update the stock of the product in the inventory after confirming addition
            _productService.UpdateProductStocks(product.Id, quantity);
        }


        /// Finds a product in the cart accessed by id and returns it.    
        public Product FindProductInCartLines(int productId)
        {
            var cartLine = FindCartLine(productId);
            return cartLine?.Product; 
            // Returns null if cartLine is not found
        }

        // Access CartLine where the product is stored. Function is private as it is only used internally for other functions.
        private CartLine FindCartLine(int productId)
        {
            return cartLines.FirstOrDefault(cartLine => cartLine.Product.Id == productId);
        }

        /// Removes a product from the cart by accessing it through the productId.
        public void RemoveLine(Product product) =>
            cartLines.RemoveAll(i => i.Product.Id == product.Id);

        /// Gets the total value of the cart by summing the price of all products in the cart.
        public double GetTotalValue()
        {
            return cartLines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);
        }

        /// Gets the average value of the cart
        public double GetAverageValue()
        {
            // Handles if cart is empty to avoid division by zero
            if (cartLines.Count == 0) return 0;
            // Returns the average value of the cart by dividing the total value by the number of products in the cart
            return GetTotalValue() / cartLines.Count;
        }

        /// Clears the cart of all added products using the Clear() method.
        public void Clear()
        {
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
