﻿using Dal;
using DO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using static DO.Enums;

namespace DalTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int op = 0; // User option
            Console.WriteLine("Enter 1 for the entity \"Product\" \n" +
                "Enter 2 for the entity \"Order\"\n" +
                "Enter 3 for the entity \"OrderItem\"\n" +
                "Enter 0 to exit\n");
            DalProduct testProduct = new DalProduct();
            DalOrder testOrder = new DalOrder();
            DalOrderItem testOrderItem = new DalOrderItem();
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

                                Prodcut newProduct = new Prodcut();
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
                                    testProduct.addProduct(newProduct);
                                    Console.WriteLine("");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;
                            case 2://print product

                                Prodcut printProduct = new Prodcut();
                                Console.Write("Enter Id: ");
                                int getId = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    printProduct = testProduct.getProdcut(getId);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                Console.WriteLine(printProduct);
                                break;

                            case 3://print all prodocts

                                tempInt = 0; // counter
                                Prodcut[] arrProducts = DalProduct.getProductArray();
                                foreach (Prodcut product in arrProducts)
                                {
                                    if (tempInt == testProduct.productLeangth()) 
                                    {
                                        break;
                                    }
                                    tempInt++;
                                    Console.WriteLine(product);

                                }
                                break;

                            case 4://update
                                Prodcut testUpdateProduct = new Prodcut();

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
                                    testProduct.updateProduct(testUpdateProduct);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;

                            case 5://delete

                                Console.Write("Enter prodoct Id: ");
                                int Id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    testProduct.deleteProdoct(Id);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
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
                                testAddOrder.CostomerName = Console.ReadLine();

                                Console.Write("Enter order customer email: ");
                                testAddOrder.CostomerEmail = Console.ReadLine();

                                Console.Write("Enter order customer adress: ");
                                testAddOrder.CostomerAdress = Console.ReadLine();

                                Console.Write("Enter Order date: ");
                                testAddOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());

                                Console.Write("Enter ship date: ");
                                testAddOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());

                                Console.WriteLine("Enter delivory date");
                                testAddOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());

                                try
                                {
                                    testOrder.addOrder(testAddOrder);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;
                            case 2://print

                                Order printOrder = new Order();
                                Console.WriteLine("Enter Id: ");
                                tempInt = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    printOrder = testOrder.getOrder(tempInt);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                Console.WriteLine(printOrder);
                                break;
                            case 3://print all orders

                                Order[] arrOrders = DalOrder.getOrderArray();
                                int counter = 0;
                                foreach (Order order in arrOrders)
                                {

                                    if (counter == testOrder.orderLeangth())
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
                                testupdateOrder.CostomerName = Console.ReadLine();

                                Console.Write("Enter order customer adress: ");
                                testupdateOrder.CostomerAdress = Console.ReadLine();

                                Console.Write("Enter customer email: ");
                                testupdateOrder.CostomerEmail = Console.ReadLine();

                                Console.Write("Enter Order date: ");
                                testupdateOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());

                                Console.Write("Enter ship date: ");
                                testupdateOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());

                                Console.Write("Enter delivory date: ");
                                testupdateOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                                try
                                {
                                    testOrder.updateOrder(testupdateOrder);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;
                            case 5://delete
                                Console.Write("Enter Id: ");
                                int.TryParse(Console.ReadLine(), out tempInt);
                                try
                                {
                                    testOrder.deleteOrder(tempInt);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
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
                                    testOrderItem.addOrderItem(testAddOrderItem);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;

                            case 2://print

                                OrderItem printOrderItem = new OrderItem();
                                Console.Write("Enter Id: ");
                                tempInt = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    printOrderItem = testOrderItem.getOrderItem(tempInt);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                Console.WriteLine(printOrderItem);
                                break;

                            case 3://print OrderItem

                                OrderItem[] arrOrderItems = DalOrderItem.getOrderItemArray();
                                int counter = 0;
                                foreach (OrderItem orderItem in arrOrderItems)
                                {
                                    
                                    if (counter == testOrderItem.orderItemLeangth())
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
                                    testOrderItem.updateOrderItem(testUpdateOrderItem);
                                    Console.WriteLine();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;

                            case 5://delete
                                Console.Write("Enter Id: ");
                                        int.TryParse(Console.ReadLine(), out tempInt);
                                try
                                {
                                    testOrderItem.deleteOrderItem(tempInt);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                break;
                        }

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
}