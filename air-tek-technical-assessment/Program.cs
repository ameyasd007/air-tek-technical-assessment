using air_tek_technical_assessment.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text.Json;

namespace air_tek_technical_assessment
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configuration.Build();


            // Load flight schedule
            var flightSchedule = new List<FlightInfo>
            {
                new FlightInfo { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 },
                new FlightInfo { FlightNumber = 2, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 1 },
                new FlightInfo { FlightNumber = 3, DepartureCity = "YUL", ArrivalCity = "YVR", Day = 1 },
                new FlightInfo { FlightNumber = 4, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 2 },
                new FlightInfo { FlightNumber = 5, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 2 },
                new FlightInfo { FlightNumber = 6, DepartureCity = "YUL", ArrivalCity = "YVR", Day = 2 },
            };
            foreach (var flight in flightSchedule)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.DepartureCity}, arrival: {flight.ArrivalCity}, day: {flight.Day}");
            }

            // Load orders from JSON file
            var orderFilePath = config.GetSection("OrdersFilePath").Value;
            var ordersJsonString = File.ReadAllText(orderFilePath);
            var ordersDictionary = JsonConvert.DeserializeObject<Dictionary<string, Order>>(ordersJsonString);
            var ordersList = ordersDictionary?.Select(x => new Order
            {
                OrderId = x.Key,
                Destination = x.Value.Destination
            }).ToList();


            // Schedule orders
            var schedulingService = new OrderSchedulingService();
            schedulingService.GenerateSchedule(flightSchedule, ordersList);
            Console.WriteLine();
            schedulingService.PrintSchedule();

        }
    }
}