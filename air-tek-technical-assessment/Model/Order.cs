using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace air_tek_technical_assessment.Model
{
    public class Order
    {
        public string OrderId { get; set; }

        private int orderPriority;
        public int OrderPriority
        {
            get 
            { 
                if(orderPriority == 0)
                    if(int.TryParse(OrderId.Replace("order-", ""), out int priority))
                        OrderPriority = priority;
                    else
                        OrderPriority = int.MaxValue;

                return orderPriority; 
            }
            set
            {
                orderPriority = value;
            }
        }

        public string Destination { get; set; }
    }
}
