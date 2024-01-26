using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace air_tek_technical_assessment.Model
{
    public class FlightInfo
    {
        public Aircraft Aircraft { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
        public int Day { get; set; }

        private List<Order> _loadedOrders;

        public FlightInfo()
        {
            Aircraft = new Aircraft();
            _loadedOrders = new List<Order>();
        }

        public bool IsFlightFull() => _loadedOrders.Count >= Aircraft.Capacity;

        public void LoadOrder(Order order) => _loadedOrders.Add(order);


    }
}
