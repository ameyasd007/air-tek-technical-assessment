using air_tek_technical_assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace air_tek_technical_assessment
{
    public class OrderSchedulingService : ISchedulingService
    {
        public readonly Dictionary<Order, FlightInfo?> OrderSchedule;

        public OrderSchedulingService()
        {
            OrderSchedule = new Dictionary<Order, FlightInfo?>();
        }

        public void GenerateSchedule(List<FlightInfo> flights, List<Order> orders)
        {
            foreach (var order in orders.OrderBy(o => o.OrderPriority))
            {
                var scheduledFlight = FindNextAvailableFlight(flights, order.Destination);
                
                scheduledFlight?.LoadOrder(order);

                OrderSchedule.Add(order, scheduledFlight);
            }
        }

        public void PrintSchedule()
        {
            foreach (var (order, flight) in OrderSchedule)
            {
                if(flight is null)
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: not scheduled");
                }
                else
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: {flight.FlightNumber}, departure: YUL, arrival: {order.Destination}, day: {flight.Day}");
                }
            }
        }

        private static FlightInfo? FindNextAvailableFlight(List<FlightInfo> flights, string destination)
        {
            return flights
                .Where(f => !f.IsFlightFull())
                .OrderBy(f => f.Day)
                .FirstOrDefault(f => f.ArrivalCity == destination);
           
        }
    }
}
