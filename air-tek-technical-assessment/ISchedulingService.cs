using air_tek_technical_assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace air_tek_technical_assessment
{
    public interface ISchedulingService
    {
        void GenerateSchedule(List<FlightInfo> flights, List<Order> orders);
        void PrintSchedule();
    }
}
