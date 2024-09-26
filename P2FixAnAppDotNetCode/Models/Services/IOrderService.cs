using P2FixAnAppDotNetCode.Models;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IOrderService
    {
        void SaveOrder(Order order);
    }
}
