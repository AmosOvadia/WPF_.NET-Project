using BlImplementation;

using Dal;
using DalApi;
using DalList;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlTest;

internal class Program
{
    
    static BO.Cart cart = new BO.Cart();

    //When the user selects the Cart option
    static void optionCart()
    {
        Bl access = new Bl();
        int id = 0, Quantity = 0;
        char option = '1';
            Console.WriteLine(
                "a in order to adding an Product to cart\n" +
                "b in order to Update Product Quantity in cart\n" +
                "c in order to Confirme the Order \n" +
                "0 to exit\n");
            bool isRead = char.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case 'a':
                    Console.WriteLine("Enter the ID number of the product you want to add\n");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(access.BoCart.Add(cart, id));
                    break;


                case 'b':
                    Console.WriteLine("Enter the product ID number and the quantity the you want to change\n");
                    int.TryParse(Console.ReadLine(), out id);
                    int.TryParse(Console.ReadLine(), out Quantity);
                    Console.WriteLine(access.BoCart.UpdateProductQuantity(cart, id, Quantity));
                    break;


                case 'c':                    
                    access.BoCart.MakeAnOrder(cart);
                    break;
                
                case '0':
                    break;

                default:
                    Console.WriteLine("wrong input");
                    break;
            }
        


    }

    //When the user selects the Order option
    static void optionOrder()
    {
        Bl access = new Bl();
        BO.Order order = new BO.Order();
        Console.WriteLine("\nOrder\n" +
            "a in order to receive all orders\n" +
            "b in order to receive a certain order, according to Id\n" +
            "c in order to update the shipping date of the shipment \n" +
            "d in order to update the delivery date of the shipment \n" +
            "e in order to get the tracking of the order \n" +
            "any other letter in order to exit");
        bool isRead = char.TryParse(Console.ReadLine(), out char option);
        switch (option)
        {
            case 'a':
                List<BO.OrderForList> boOrderForLists = new List<BO.OrderForList>();
                boOrderForLists = (List<BO.OrderForList>)access.BoOrder.GetOrders();
                foreach (var boOrder in boOrderForLists)
                {
                    Console.WriteLine(boOrder);
                }
                break;


            case 'b':
                int id;
                Console.WriteLine("Enter the ID number of the order you want to get\n");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(access.BoOrder.GetOrder(id));
                break;


            case 'c':
                Console.WriteLine("Enter the ID of the order for which you want to change the shipping time\n");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(access.BoOrder.OrderShippingUpdate(id));
                break;

            case 'd':
                Console.WriteLine("Enter the ID of the order for which you want to change the delivery time\n");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(access.BoOrder.OrderDeliveryUpdate(id));
                break;

            case 'e':
                Console.WriteLine("Enter the order number you want to track\n");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(access.BoOrder.Order_tracking(id));

                break;

            default:
                break;
        }
    }

    //When the user selects the Product option
    static void optionProduct()
    {
        Bl access = new Bl();
        BO.Product product = new BO.Product();
        Console.WriteLine("\nProduct\n" +
    "a in order to view products catalog \n" +
    "b in order to view the details of a poduct by its id \n" +
    "c in order to view the details of a poduct by its id and cart\n" +    /////
    "d in order to add a prudoct\n" +
    "e in order to delete a product\n" +
    "f in order to update a product\n" +
    "any other letter in order to exit");
        char option;
        bool isRead = char.TryParse(Console.ReadLine(), out option);
        
        switch (option)
        {
            case 'a':
                IEnumerable<BO.ProductForList> products = access.BoProduct.GetProducts();
                foreach (var item in products)
                {
                    Console.WriteLine(item);
                };
                break;

            case 'b':
                Console.WriteLine("Enter the ID product you seek to view\n");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                product.Id = ID;
                Console.WriteLine(access.BoProduct.GetProductById(ID));
                break;


            case 'c':
                Console.WriteLine("Enter the ID product\n");
                isRead = int.TryParse(Console.ReadLine(), out int Id);

                 Console.WriteLine(access.BoProduct.GetProductItem(Id, cart)); 
                

                break;

            case 'd':
                Console.WriteLine("Enter the ID ,Name ,Price ,Category ,InStock  of product you seek to add\n");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                product.Id = ID;
                product.Name = Console.ReadLine();
                isRead = int.TryParse(Console.ReadLine(), out int price);
                product.Price = price;
                BO.Enums.ProdactCategory a;
                BO.Enums.ProdactCategory.TryParse(Console.ReadLine(), out a);
                product.Category = a;
                isRead = int.TryParse(Console.ReadLine(), out int inStock);
                product.InStock = inStock;
                access.BoProduct.Add(product);
                Console.WriteLine();
                break;

            case 'e':
                Console.WriteLine("Enter the product ID of the product you want to delete\n");                
                isRead = int.TryParse(Console.ReadLine(), out ID);
                access.BoProduct.Delete(ID);
                
                break;

            case 'f':


                Console.WriteLine("Enter the ID ,Name ,Price ,Category ,InStock  of product you seek to update\n");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                product.Id = ID;
                product.Name = Console.ReadLine();
                isRead = int.TryParse(Console.ReadLine(), out int _price);
                product.Price = _price;
                BO.Enums.ProdactCategory c;            
                BO.Enums.ProdactCategory.TryParse(Console.ReadLine(), out c);
                product.Category = c;
                isRead = int.TryParse(Console.ReadLine(), out  inStock);
                product.InStock = inStock;
                access.BoProduct.Update(product);

                break;
            default:
                break;
        }

    }

   

    static void Main(string[] args)
    {
        //The user is asked for his data to enter it in his shopping cart.
        Console.Write("Please enter your name: ");
        cart.CostomerName = Console.ReadLine();
        Console.WriteLine("\nPlease enter your address: ");
        cart.CostomerAdress = Console.ReadLine();

        EnterEmail();
        Console.WriteLine();

        IDal access = new DalList1(); // Access to the data layer



        //Catalog of all products
        IEnumerable<DO.Product> products = access.Product.GetList();
        foreach (var item in products)
        {
            Console.WriteLine(item);
        }

        // the purpose's program is to check the Dal layer
        Console.WriteLine("welcome, please enter your choise\n" +
            "0 - exit \n" +
            "1 - Cart \n" +
            "2 - Order \n" +
            "3 - product.\n" +
            "please enter a choice\n");

        string choice = "";

        while(choice != "0") // as long as the user haven't enter 0 continue to ask for new choice
        {
            try
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.WriteLine("goodbye");
                        break;
                    case "1":
                        optionCart();
                        break;
                    case "2":
                        optionOrder();
                        break;
                    case "3":
                        optionProduct();
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                }
            }
           // Each exception has its own "cathe".
            catch (BO.TheIdDoesNotExistInTheDatabase exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.TheIDAlreadyExistsInTheDatabase exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.TheVariableIsLessThanTheNumberZero exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.VariableIsNull exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.OutOfStock exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.InputError exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (BO.IsEmpty exe)
            {
                Console.WriteLine(exe.Message);
            }


            catch (DO.TheIdentityCardDoesNotExistInTheDatabase exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (DO.TheIDAlreadyExistsInTheDatabase exe)
            {
                Console.WriteLine(exe.Message);
            }
            catch (Exception messege)
            {
                Console.WriteLine(messege.Message);
            }

            if (choice != "0") // if the user haven't enter 0 ask for a new choice
            {
                Console.WriteLine("please enter another choice\n");
            }
        }
    }


    //Ask the user again and again until he enters a correct email address
    public static void EnterEmail()
    {
        do
        {
            Console.WriteLine("\nPlease enter your email address: ");
            cart.CostomerEmail = Console.ReadLine();
        } while (!IsValid(cart.CostomerEmail)); //Check if the email address entered by the user is correct.
    }


     // Auxiliary function for checking the correctness of an email address
     public  static bool IsValid(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}









