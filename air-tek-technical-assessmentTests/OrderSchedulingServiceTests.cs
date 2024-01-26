using Microsoft.VisualStudio.TestTools.UnitTesting;
using air_tek_technical_assessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using air_tek_technical_assessment.Model;
using Newtonsoft.Json;

namespace air_tek_technical_assessment.Tests
{
    [TestClass()]
    public class OrderSchedulingServiceTests
    {
        [TestMethod()]
        public void GenerateScheduleTest_WithMoreThan20OrdersPerDestination()
        {
            var flightSchedule = new List<FlightInfo>
            {
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 },
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 1 },
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 2 },
            };

            var orders = new List<Order>();
            for(int i = 1; i <= 50;  i++)
            {
                orders.Add(new Order { OrderId = "order-" + i, Destination = "YYZ"});
            }
            for(int i = 51; i <= 80;  i++)
            {
                orders.Add(new Order { OrderId = "order-" + i, Destination = "YYC"});
            }

            var schedulingService = new OrderSchedulingService();
            schedulingService.GenerateSchedule(flightSchedule, orders);

            var notScheduledOrderCount = schedulingService.OrderSchedule.Where(o => o.Value is null).Count();
            Assert.AreEqual(20, notScheduledOrderCount);

            var yyzOrderCount = schedulingService.OrderSchedule.Where(o => o.Value?.ArrivalCity == "YYZ").Count();
            Assert.AreEqual(40, yyzOrderCount);

            var yycOrderCount = schedulingService.OrderSchedule.Where(o => o.Value?.ArrivalCity == "YYC").Count();
            Assert.AreEqual(20, yycOrderCount);
        }

        [TestMethod()]
        public void GenerateScheduleTest_WithNoFlightsToDestination()
        {
            var flightSchedule = new List<FlightInfo>
            {
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 },
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 1 },
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 2 },
            };

            var orders = new List<Order>();
            for(int i = 1; i <= 50;  i++)
            {
                orders.Add(new Order { OrderId = "order-" + i, Destination = "YYZ"});
            }
            for(int i = 51; i <= 60;  i++)
            {
                orders.Add(new Order { OrderId = "order-" + i, Destination = "YVR"});
            }

            var schedulingService = new OrderSchedulingService();
            schedulingService.GenerateSchedule(flightSchedule, orders);

            var notScheduledOrderCount = schedulingService.OrderSchedule.Where(o => o.Value is null).Count();
            Assert.AreEqual(40, notScheduledOrderCount);

            var yyzOrderCount = schedulingService.OrderSchedule.Where(o => o.Value?.ArrivalCity == "YYZ").Count();
            Assert.AreEqual(20, yyzOrderCount);

            var yycOrderCount = schedulingService.OrderSchedule.Where(o => o.Value?.ArrivalCity == "YYC").Count();
            Assert.AreEqual(0, yycOrderCount);
        }
    }
}