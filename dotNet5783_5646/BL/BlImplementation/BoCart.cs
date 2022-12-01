using BlApi;

using Dal;
using DalApi;
using System.Net.Mail;
using static BO.Enums;
using static DO.Enums;
//The implementation of the interface of the logical entity - Cart

namespace BlImplementation;

internal class BoCart : BlApi.ICart
{
    private IDal dal = new Dal.DalList1();


    //The function adds an item to the shopping cart
    public BO.Cart Add(BO.Cart cart, int id)
    {
       
        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetList();
        if (cart.Items == null)
        {
            foreach (var product in Do_Products)
            {
                if (product.Id == id)
                {
                    BO.OrderItem orderItem = new BO.OrderItem();
                    orderItem.ProductId = id;
                    orderItem.Id = dal.OrderItem.GetList().ElementAt(dal.OrderItem.GetList().Count() - 1).Id + 1;
                    orderItem.Price = product.Price;
                    orderItem.TotalPrice = product.Price;
                    if (product.InStock >= 1) //Check if the product is in stock
                    {
                        orderItem.Amount = 1;
                    }
                    else throw new BO.OutOfStock("Out of stock");
                    orderItem.Name = product.Name;
                    List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
                    boOrderItems.Add(orderItem);
                    cart.Items = boOrderItems;
                    //cart.Items.Add(orderItem); // Add the order item to the list
                    cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount
                    return cart;
                }
                  
            }
            throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
        }
        //In case the member already exists
        foreach (var item in cart.Items)
        {   
            if (item.Id == id)
            {
                foreach (var product in Do_Products)
                {
                    if (product.InStock > item.Amount)
                    {
                        item.TotalPrice = (++item.Amount) * (item.Price);
                        cart.TotalPrice += item.Price;
                    }
                    else throw new BO.OutOfStock("Out of stock");
                }
                return cart;
            }
        }
       // In case the item does not exist in the cart
        foreach (var product in Do_Products)
        {
            if (product.Id == id)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ProductId = id;
                orderItem.Id = dal.OrderItem.GetList().ElementAt(dal.OrderItem.GetList().Count() - 1).Id + 1;
                orderItem.Price = product.Price;
                orderItem.TotalPrice = product.Price;
                if (product.InStock >= 1) //Check if the product is in stock
                {
                    orderItem.Amount = 1;
                }
                else throw new BO.OutOfStock("Out of stock");
                orderItem.Name = product.Name;

                cart.Items.Add(orderItem); // Add the order item to the list
                cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount
            }

        }
        throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
    }


    //Create an order
    public void MakeAnOrder(BO.Cart cart)
    {
        if (cart.Items == null)
        {
            throw new BO.IsEmpty("The shopping cart is empty");
        }
        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetList();
        foreach (var item in cart.Items) //Check that all data is correct
        {
            if (item.Amount <= 0 || cart.CostomerName == null || cart.CostomerEmail == null || cart.CostomerAdress == null || !IsValid(cart.CostomerEmail))
            {
                throw new BO.InputError("Input error");
            }
            foreach (var product in Do_Products)
            {
                if (item.ProductId == product.Id)
                {
                    if (item.Amount > product.InStock)  //check that the quantity is less than the quantity in stock
                        throw new BO.OutOfStock("Out of stock");
                }
            }
        }

        DO.Order order = new DO.Order();
        order.Id = dal.Order.GetList().ElementAt(dal.Order.GetList().Count() - 1).Id + 1;
        order.CostomerName = cart.CostomerName;
        order.CostomerEmail = cart.CostomerEmail;
        order.CostomerAdress = cart.CostomerAdress;
        order.OrderDate = DateTime.Now;
        order.ShipDate = null;
        order.DeliveryDate = null;
        int orderId = dal.Order.Add(order); // Add an order to the data layer

        foreach (var item in cart.Items)
        {
            DO.OrderItem orderItem  = new DO.OrderItem();
            orderItem.Id = dal.OrderItem.GetList().ElementAt(dal.OrderItem.GetList().Count() - 1).Id + 1;
            orderItem.OrderId = orderId;
            orderItem.ProductId = item.ProductId;
            orderItem.Price = item.Price;
            orderItem.Amount = item.Amount;
            int k = dal.OrderItem.Add(orderItem); //We will add list details to the data layer
            foreach (var product in Do_Products)
            {
                if (product.Id == item.ProductId)
                {                   
                    DO.Product temp = new DO.Product();
                    temp.Id = product.Id;
                    temp.Price = product.Price;
                    temp.Category = product.Category;
                    temp.InStock = product.InStock - item.Amount;
                    temp.Name = product.Name;
                    dal.Product.Update(temp); //Updates the stock quantity in the data layer
                    break;
                }
            }
        }
    }





    // Auxiliary function for checking the correctness of an email address
   
    bool IsValid(string email)
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




    // The function updates the quantity
    public BO.Cart UpdateProductQuantity(BO.Cart cart, int Id, int TheNewQuantity)
    {
        if (cart.Items == null)
        {
            throw new BO.IsEmpty("The shopping cart is empty");
        }
        if (TheNewQuantity < 0)  //check for correctness
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("There is no such thing as a negative quantity");
        }
        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetList();
        foreach (var item in cart.Items)
        {
            if (item.ProductId == Id)
            {
                if (item.Amount < TheNewQuantity) //In case he wants to add
                {
                    cart.TotalPrice -= item.TotalPrice;
                    foreach (var product in Do_Products)
                    {
                        if (product.Id == Id)
                        {
                            if (product.InStock >= TheNewQuantity)
                            {
                                item.Amount = TheNewQuantity;
                                item.TotalPrice = item.Price * TheNewQuantity;
                            }
                            else throw new BO.OutOfStock("Out of stock");
                        }
                    }
                    cart.TotalPrice += item.TotalPrice;
                    return cart;
                }
                else
                {
                    if (item.Amount > TheNewQuantity) //In case he wants to subtract
                    {
                        if (TheNewQuantity == 0)
                        {
                            cart.TotalPrice -= item.TotalPrice;
                            cart.Items.Remove(item);
                        }
                        else
                        {
                            cart.TotalPrice -= item.TotalPrice;
                            item.Amount = TheNewQuantity;
                            item.TotalPrice = (item.Price * TheNewQuantity);
                            cart.TotalPrice += item.TotalPrice;
                        }
                    }
                    return cart;
                }
            }
        }
        throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist"); 

    }
}