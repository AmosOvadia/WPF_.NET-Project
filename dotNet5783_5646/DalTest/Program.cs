
using DO;
using DalApi;
using static DO.Enums;
using System;

namespace DalTest;

internal class Program
{

   public static DalApi.IDal? dal = DalApi.Factory.Get();

    static void Main(string[] args)
    {
        int op = 0; // User option
        Console.WriteLine("Enter 1 for the entity \"Product\" \n" +
            "Enter 2 for the entity \"Order\"\n" +
            "Enter 3 for the entity \"OrderItem\"\n" +
            "Enter 0 to exit\n");
       

     
        
        int.TryParse(Console.ReadLine(), out op);

        int tempInt;
        double tempDouble;

        while (op != 0) // 0 ends the program
        {

            switch (op)
            {
                case 0: break;
                case 1:

                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a prodoct\n " +
                        "Enter 2 to print product\n " +
                        "Enter 3 to see list of products\n " +
                        "Enter 4 to update product\n " +
                        "Enter 5 to delete product  ");

                    bool check;
                    int opProduct;
                    check = int.TryParse(Console.ReadLine(), out opProduct);

                    
                    if (opProduct == 0)
                        break;

                    switch (opProduct)
                    {
                        case 1://add product

                            Product newProduct = new Product();
                            Console.Write("Add a Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            newProduct.Id = tempInt;

                            Console.Write("Add Prodoct name: ");
                            newProduct.Name = Console.ReadLine();

                            Console.Write("Add Prodoct Category: ");
                            ProdactCategory categoryPro;
                            ProdactCategory.TryParse(Console.ReadLine(), out categoryPro);
                            newProduct.Category = categoryPro;

                            Console.Write("Add product price: ");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            newProduct.Price = tempDouble;

                            Console.Write("Add amount in stock: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            newProduct.InStock = tempInt;

                            try
                            {
                                
                                dal.Product.Add(newProduct);
                                Console.WriteLine("");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 2://print product

                            Product printProduct = new Product();
                            Console.Write("Enter Id: ");
                            int getId = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                printProduct = dal.Product.Get(getId);
                                Console.WriteLine(printProduct);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                           
                            break;

                        case 3://print all prodocts

                            tempInt = 0; // counter
                            List<Product?> listProducts = new List<Product?>(dal.Product.GetList());

                            foreach (Product product in listProducts)
                            {                                
                                Console.WriteLine(product);
                            }
                            break;

                        case 4://update
                            Product testUpdateProduct = new Product();

                            Console.Write("Enter Prodoct Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateProduct.Id = tempInt;

                            Console.Write("Enter Prodoct name: ");
                            testUpdateProduct.Name = Console.ReadLine();

                            Console.Write("Enter Prodoct category: ");
                            ProdactCategory categoryPr;
                            ProdactCategory.TryParse(Console.ReadLine(), out categoryPr);
                            testUpdateProduct.Category = categoryPr;


                            Console.Write("Enter product price: ");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            testUpdateProduct.Price = tempDouble;

                            Console.Write("Enter amount in stock: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateProduct.InStock = tempInt;
                            try
                            {
                                dal.Product.Update(testUpdateProduct);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 5://delete

                            Console.Write("Enter prodoct Id: ");
                            int Id = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                dal.Product.Delete(Id);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                    }
                    break;

                case 2: //Order

                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a order\n " +
                        "Enter 2 to print order\n " +
                        "Enter 3 to see list of order\n " +
                        "Enter 4 to update order\n " +
                        "Enter 5 to delet order  ");

                    int opOrder;
                    check = int.TryParse(Console.ReadLine(), out opOrder);

                    if (opOrder == 0)
                        break;

                    switch (opOrder)
                    {
                        case 1://add

                            Order testAddOrder = new Order();

                            Console.Write("Enter order Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testAddOrder.Id = tempInt;

                            Console.Write("Enter order customer name: ");
                            testAddOrder.CustomerName = Console.ReadLine();

                            Console.Write("Enter order customer email: ");
                            testAddOrder.CustomerEmail = Console.ReadLine();

                            Console.Write("Enter order customer adress: ");
                            testAddOrder.CustomerAdress = Console.ReadLine();

                            Console.Write("Enter Order date: ");
                            testAddOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Enter ship date: ");
                            testAddOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Enter delivory date");
                            testAddOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());

                            try
                            {
                                dal.Order.Add(testAddOrder);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 2://print

                            Order printOrder = new Order();
                            Console.WriteLine("Enter Id: ");
                            tempInt = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                printOrder = dal.Order.Get(tempInt);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine(printOrder);
                            break;
                        case 3://print all orders
                            List<Order?> arrOrders = new List<Order?>( dal.Order.GetList());
                            int counter = 0;
                            foreach (Order order in arrOrders)
                            {

                                if (counter == dal.Order.Leangth())
                                {
                                    break;
                                    
                                }
                                counter++;
                                Console.WriteLine(order);
                            }
                            break;

                        case 4://update

                            Order testupdateOrder = new Order();

                            Console.Write("Enter Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testupdateOrder.Id = tempInt;

                            Console.Write("Enter customer name: ");
                            testupdateOrder.CustomerName = Console.ReadLine();

                            Console.Write("Enter order customer adress: ");
                            testupdateOrder.CustomerAdress = Console.ReadLine();

                            Console.Write("Enter customer email: ");
                            testupdateOrder.CustomerEmail = Console.ReadLine();

                            Console.Write("Enter Order date: ");
                            testupdateOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Enter ship date: ");
                            testupdateOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Enter delivory date: ");
                            testupdateOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                            try
                            {
                                dal.Order.Update(testupdateOrder);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 5://delete
                            Console.Write("Enter Id: ");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                dal.Order.Delete(tempInt);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                    }
                    break;

                case 3:

                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a orderItem\n " +
                        "Enter 2 to print orderItem\n " +
                        "Enter 3 to see list of orderItem\n " +
                        "Enter 4 to update orderItem\n " +
                        "Enter 5 to delet orderItem  ");

                    int opOrderItem;
                    check = int.TryParse(Console.ReadLine(), out opOrderItem);

                    if (opOrderItem == 0)
                        break;

                    switch (opOrderItem) //OrderItem
                    {
                        case 1:

                            OrderItem testAddOrderItem = new OrderItem();

                            Console.WriteLine("Enter Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testAddOrderItem.Id = tempInt;

                            Console.Write("Enter order id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);

                            testAddOrderItem.OrderId = tempInt;
                            Console.Write("Enter product id: ");

                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testAddOrderItem.ProductId = tempInt;

                            Console.Write("Enter price: ");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            testAddOrderItem.Price = tempDouble;

                            Console.Write("Enter amount: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testAddOrderItem.Amount = tempInt;
                            try
                            {
                                dal.OrderItem.Add(testAddOrderItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 2://print

                            OrderItem printOrderItem = new OrderItem();
                            Console.Write("Enter Id: ");
                            tempInt = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                printOrderItem = dal.OrderItem.Get(tempInt);
                                Console.WriteLine(printOrderItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            break;

                        case 3://print OrderItem

                            List<OrderItem?> arrOrderItems = new List<OrderItem?> (dal.OrderItem.GetList());
                            int counter = 0;
                            foreach (OrderItem orderItem in arrOrderItems)
                            {
                                
                                if (counter == dal.OrderItem.Leangth())
                                {
                                    break;
                                    
                                }
                                counter++;
                                Console.WriteLine(orderItem);
                            }
                            break;

                        case 4://update

                            OrderItem testUpdateOrderItem = new OrderItem();

                            Console.Write("Enter Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateOrderItem.Id = tempInt;

                            Console.Write("Enter order Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateOrderItem.OrderId = tempInt;

                            Console.Write("Enter product Id: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateOrderItem.ProductId = tempInt;

                            Console.Write("Enter price: ");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            testUpdateOrderItem.Price = tempDouble;

                            Console.Write("Enter amount: ");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            testUpdateOrderItem.Amount = tempInt;
                            try
                            {
                                dal.OrderItem.Update(testUpdateOrderItem);
                                Console.WriteLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 5://delete
                            Console.Write("Enter Id: ");
                                    int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                dal.OrderItem.Delete(tempInt);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                    }

                    break;

                case 4:
                    XmlTools.SaveListToXMLSerializer(dal.Product.GetList().ToList(), "Product");
                    XmlTools.SaveListToXMLSerializer(dal.Order.GetList().ToList(), "Order");
                    XmlTools.SaveListToXMLSerializer(dal.OrderItem.GetList().ToList(), "OrderItem");

                    int lastOrderItemID = dal.OrderItem.GetList().Last()?.Id ?? 0;
                    int lastOrderID = dal.Order.GetList().Last()?.Id ?? 0;
                    XmlTools.SaveConfigXElement("OrderId", lastOrderID);
                    XmlTools.SaveConfigXElement("OrderItemId", lastOrderItemID);
                    break;
            }
            Console.WriteLine("Enter 0 to end\n " +
                            "Enter 1 for product\n " +
                            "Enter 2 for order\n " +
                            "Enter 3 for order item:");

            int.TryParse(Console.ReadLine(), out op);
        }
    }
}