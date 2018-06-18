using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.ViewModels
{
    public class OrderAllViewModel
    {
        public int NumOrders { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
