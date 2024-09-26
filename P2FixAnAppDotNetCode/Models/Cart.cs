using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    public class Cart : ICart
    {
        // List to hold the cart lines
        private List<CartLine> cartLines = new List<CartLine>();
        public IEnumerable<CartLine> Lines => cartLines; // Directly use cartLines

        /// Adds a product to the cart or increments its quantity if already added.
       
        public void AddItem(Product product, int quantity)
        {
            // Validate product and quantity
            if (product == null || quantity < 1)
            {
                //if nothing throw an exception
                throw new ArgumentException("Sorry, your product is not available or quantity is less than 1");
            }
            // Find the current cart line
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
        }

        /// Finds a product in the cart and returns it.    
        public Product FindProductInCartLines(int productId)
        {
            var cartLine = FindCartLine(productId);
            return cartLine?.Product; // Return null if cartLine is not found
        }

        // Access CartLine where the product is stored
        private CartLine FindCartLine(int productId)
        {
            return cartLines.FirstOrDefault(cartLine => cartLine.Product.Id == productId);
        }

        /// Removes a product from the cart.
       
        public void RemoveLine(Product product) =>
            cartLines.RemoveAll(l => l.Product.Id == product.Id); // Directly use cartLines
        
        /// Gets the total value of the cart.
        public double GetTotalValue()
        {
            return cartLines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);
        }

        /// Gets the average value of the cart.
        public double GetAverageValue()
        {
            if (cartLines.Count == 0) return 0; // Handle empty cart case
            return GetTotalValue() / cartLines.Count;
        }

        /// Gets a specific cart line by its index.
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ElementAt(index);
        }

        /// Clears the cart of all added products.
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
