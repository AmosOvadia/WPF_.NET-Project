using DO;
using System.ComponentModel;
using System;
using static DO.Enums;

namespace Dal;



internal static class DataSource
{
    
    static DataSource() { //s_Initialize();
                          }
    static readonly Random random = new Random();
    internal static Prodcut[] productArr = new Prodcut[50];
    internal static Order[] orderArr = new Order[100];
    internal static OrderItem[] orderItemArr = new OrderItem[200];



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
            orderArr[i].Id = Config.getIdOreder;
            Config.indexOrder++;
            orderArr[i].CostomerName = costomerName[i];
            orderArr[i].CostomerEmail = CostomerEmail[i];
            orderArr[i].CostomerAdress = CostomerAdress[i];
            DateTime time = DateTime.Now;  
            orderArr[i].OrderDate = time.AddDays(-5).AddHours(6).AddMinutes(7);
            if (i < 16)  // 80%
            {
                orderArr[i].ShipDate = time.AddDays(-3).AddHours(4).AddMinutes(5);
            }
            else
             orderArr[i].ShipDate = time.AddDays(-5).AddHours(6).AddMinutes(7);
            if (i <= 12) // 60%
            {
                orderArr[i].DeliveryDate = time.AddDays(-1).AddHours(6).AddMinutes(7);
            }
            else orderArr[i].DeliveryDate = null;
        }


    }




    private static void s_addOrderItem()
    {
        OrderItem orderItem = new OrderItem();
        int k = 0;
        for (int i = 0; i < 10; i++)
        {
            Prodcut productTemp = productArr[i]; 
            for (int J = 0; J < 4; J++,k++)
            {
                orderItemArr[k].Id = Config.getIdOrederItem;
                orderItemArr[k].OrderId = Config.getIdOreder;
                orderItemArr[k].ProductId = productTemp.Id;
                orderItemArr[k].Price = productTemp.Price;
                orderItemArr[k].Amount = random.Next(1, 5);
                Config.indexOrderItem++;
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
            
            productArr[i].Id =  Config.getIdProduct;
            productArr[i].Name = name[i];
            productArr[i].Price = prices[i];    
            productArr[i].Category = ProdactCategory.Shirts;
            productArr[i].InStock = random.Next(10, 50);
            Config.indexProduct++;
        }
        for ( j = i; j < 8; j++)
        {
            productArr[j].Id = Config.getIdProduct;
            productArr[j].Name = name[j];
            productArr[j].Price = prices[j];
            productArr[j].Category = ProdactCategory.Hats;
            productArr[j].InStock = random.Next(10, 50);
            Config.indexProduct++;
        }
        for ( k = j; k < 10; k++)
        {
            productArr[k].Id = Config.getIdProduct;
            productArr[k].Name = name[k];
            productArr[k].Price = prices[k];
            productArr[k].Category = ProdactCategory.Shoes;
            productArr[k].InStock = random.Next(10, 50);
            Config.indexProduct++;
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
        internal static int indexOrder = 0;
        internal static int indexOrderItem = 0;
        internal static int indexProduct = 0;



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

    

