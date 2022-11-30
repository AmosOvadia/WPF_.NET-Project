using DO;
using System.ComponentModel;
using System;
using static DO.Enums;
using System.Transactions;
using DalApi;
using static Dal.DataSource;

namespace Dal;



internal static class DataSource
{

    static DataSource() { s_Initialize(); }
                          
    static readonly Random random = new Random();
    internal static List<Product> productList = new List<Product>();
    internal static List<Order> orderList = new List<Order>();
    internal static List<OrderItem> orderItemList = new List<OrderItem>();

    //internal static Prodcut[] productArr = new Prodcut[50];
    //internal static Order[] orderArr = new Order[100];
    //internal static OrderItem[] orderItemArr = new OrderItem[200];
    private static void s_addOrder()
    {
        string[] costomerName = { "Clay Blankenship",
"Angel Estrada",
"Ramone Gilmour",
"Michael Dean",
"Terry Rhodes",
"Amber Johnson",
"Elaina Cruz",
"Kimberley Rosales",
"Lola Meza",
"Ahmed Avila",
"Safwan Galindo",
"Louise Hirst",
"Indiana Taylor",
"Omer Barron",
"Caden Philip",
"Reuben Williams",
"Karina Bennett",
"Willard Riddle",
"Harvey-Lee Simon",
"Wallace Baxter"
};
        string[] CostomerEmail = { "ClayBlankenship@gmail.com",
"AngelEstrada@gmail.com",
"RamoneGilmour@gmail.com",
"MichaelDean@gmail.com",
"TerryRhodes@gmail.com",
"AmberJohnson@gmail.com",
"ElainaCruz@gmail.com",
"KimberleyRosales@gmail.com",
"LolaMeza@gmail.com",
"AhmedAvila@gmail.com",
"SafwanGalindo@gmail.com",
"LouiseHirst@gmail.com",
"IndianaTaylor@gmail.com",
"OmerBarron@gmail.com",
"CadenPhilip@gmail.com",
"ReubenWilliams@gmail.com",
"KarinaBennett@gmail.com",
"WillardRiddle@gmail.com",
"Harvey-LeeSimon@gmail.com",
"WallaceBaxter@gmail.com"
};
        string[] CostomerAdress = { "73 Bordesley Green East, Bordesley Green",
            "133 Ross, Rowley Regis",
            "19 Park Avenue, Polesworth",
            "19 Avery Croft, Birmingham",
            "154 Cole Valley Road, Hall Green",
            "7 Jesson Road, Sutton Coldfield",
            "27 Stornoway Road, Birmingham",
            "973 Kingsbury Road, Erdington",
            "Alcester Warren Farm, Whitemoor Lane, Sambourne",
            "8 Bamburgh, Dosthill",
            "268 Mary Vale Road, Birmingham",
            "1 Rossendale Close, Halesowen",
            "36 Malvern Road, Acocks Green",
            "7 Minster Close, Knowle",
            "4 Claremont Court, Cradley Heath",
            "Flat 4, Stirling House, Rectory Road, Sutton Coldfield",
            "50 Weston Lane, Birmingham",
            "35 Landor Road, Knowle",
            "53 Lichfield Road, Sutton Coldfield",
            "15 Felton Croft, Birmingham"
        };

        for (int i = 0; i < 20; i++)
        {
            Order temp = new Order();

            temp.Id = Config.getIdOreder;
            temp.CostomerName = costomerName[i];
            temp.CostomerEmail = CostomerEmail[i];
            temp.CostomerAdress = CostomerAdress[i];
            DateTime time = DateTime.Now;
            temp.OrderDate = time.AddDays(-5).AddHours(6).AddMinutes(7);
            if (i < 16)  // 80%
            {
                temp.ShipDate = time.AddDays(-3).AddHours(4).AddMinutes(5);
            }
            else
                temp.ShipDate = time.AddDays(-5).AddHours(6).AddMinutes(7);
            if (i <= 12) // 60%
            {
                temp.DeliveryDate = time.AddDays(-1).AddHours(6).AddMinutes(7);
            }
            else temp.DeliveryDate = null;
            orderList.Add(temp);    
        }


    }
    private static void s_addOrderItem()
    {
        OrderItem temp = new OrderItem();
        foreach (var item in orderList)      
        {
            int i = 0;
            foreach (Product productTemp in productList)
            {
                i++;
                temp.Id = Config.getIdOrederItem;
                temp.OrderId = item.Id;
                temp.ProductId = productTemp.Id;
                temp.Price = productTemp.Price;
                temp.Amount = random.Next(1, 5);
                orderItemList.Add(temp);
                if (i == 4) break;
            }
        }
    }
    private static void s_addProduct()
    {
        string[] name = { "Crocs", "Kennethcole", "Adidas", "Athletic", "Nike", "Puma", "Reebok", "Timberland", "Merrell", "Gucci" };
        int[] prices = { 100, 100, 200, 200, 300, 300, 400, 400, 500, 500 };
        int i = 0;
        int j;
        int k;
        for (; i < 4; i++)
        {
            Product temp = new Product();


            temp.Id =  Config.getIdProduct;
            temp.Name = name[i];
            temp.Price = prices[i];
            temp.Category = ProdactCategory.Shirts;
            temp.InStock = random.Next(10, 50);
      //      Config.indexProduct++;

            productList.Add(temp);  
        }
        for ( j = i; j < 8; j++)
        {
            Product temp = new Product();
            temp.Id = Config.getIdProduct;
            temp.Name = name[i];
            temp.Price = prices[i];
            temp.Category = ProdactCategory.Hats;
            temp.InStock = random.Next(10, 50);
            //      Config.indexProduct++;

            productList.Add(temp);
        }
        for ( k = j; k < 10; k++)
        {
            Product temp = new Product();
            temp.Id = Config.getIdProduct;
            temp.Name = name[i];
            temp.Price = prices[i];
            temp.Category = ProdactCategory.Shoes;
            temp.InStock = random.Next(10, 50);
            //      Config.indexProduct++;

            productList.Add(temp);
        }
    }





    public static void s_Initialize()
    {
        s_addProduct();
        s_addOrder();
        s_addOrderItem();  
    }
    internal static class Config
    {
        private static int idProduct = random.Next(100000, 999999);
        public static int getIdProduct
        {
            get
            {
                idProduct++;
                return idProduct;
            }
        }

        private static int idOreder = 100000;
        public static int getIdOreder
        {
            get
            {
                idOreder++;
                return idOreder;
            }
        }

        private static int idOrederItem = 100000;
        public static int getIdOrederItem
        {
            get
            {
                idOrederItem++;
                return idOrederItem;
            }
        }
    }

}

    

