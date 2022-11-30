using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Enums
    {
        //Represents the possible categories that the consumer can order
        public enum ProdactCategory { Shirts, Hats, Shoes };

        //Represents the order status of the consumer
        public enum OrderStatus { Confirmed, Sent, Delivered };
    }

}
