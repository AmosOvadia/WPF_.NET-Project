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
    DalApi.IDal? dal = DalApi.Factory.Get();

    //The function adds an item to the shopping cart
    public BO.Cart Add(BO.Cart cart, int id)
    {
        List<DO.Product?> Do_Products = new List<DO.Product?>();
        Do_Products = (List<DO.Product?>)dal!.Product.GetList();
        var product = Do_Products.FirstOrDefault(p => p?.Id == id);
        int i = Do_Products.IndexOf(product);
        bool c = false;
        if (cart.Items == null)
        {

            if (product != null)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ProductId = id;
                orderItem.Id = dal.OrderItem.GetList().Last()?.Id + 1 ?? 0; ///+ 1;
                orderItem.Price = (double)product?.Price!;
                orderItem.TotalPrice = (double)product?.Price!;
                if (product?.InStock >= 1) //Check if the product is in stock
                {
                    orderItem.Amount = 1;
                    DO.Product temp = new DO.Product();
                    temp.Id = id;
                    temp.Name = product?.Name;
                    temp.Price = (double)product?.Price!;
                    temp.InStock = (int)product?.InStock! - 1;
                    Do_Products[i] = temp;
                }
                else throw new BO.OutOfStock("Out of stock");
                orderItem.Name = product?.Name;
                List<BO.OrderItem?> boOrderItems = new List<BO.OrderItem?>();
                boOrderItems.Add(orderItem);
                cart.Items = boOrderItems;
                cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount

                DO.Product p = dal.Product.Get(id);
                p.InStock--;
                dal.Product.Update(p);
                return cart;
            }
            throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
        }

        //In case the member already exists
        var item = cart.Items.FirstOrDefault(i => (int)i?.ProductId! == id);
        if (item != null)
        {
            var product1 = Do_Products.FirstOrDefault(p => p?.Id == id);
            if (product1?.InStock > item.Amount)
            {
                DO.Product temp = new DO.Product();
                temp.Id = id;
                temp.Name = product1?.Name;
                temp.Price = (double)product1?.Price!;
                temp.InStock = (int)product1?.InStock! - 1;
                Do_Products[i] = temp;
                item.TotalPrice = (++item.Amount) * (item.Price);
                cart.TotalPrice += item.Price;
            }
            else throw new BO.OutOfStock("Out of stock");

            DO.Product p = dal.Product.Get(id);
            p.InStock--;
            dal.Product.Update(p);
            return cart;
        }

        // In case the item does not exist in the cart
        var productToAdd = Do_Products.FirstOrDefault(p => p?.Id == id);
        if (productToAdd != null)
        {
            BO.OrderItem orderItem = new BO.OrderItem();
            orderItem.ProductId = id;
            orderItem.Id = dal.OrderItem.GetList().Last()?.Id + 1 ?? 0;
            orderItem.Price = (double)productToAdd?.Price!;
            orderItem.TotalPrice = (double)productToAdd?.Price!;
            if (productToAdd?.InStock >= 1) //Check if the product is in stock
            {
                orderItem.Amount = 1;
            }
            else throw new BO.OutOfStock("Out of stock");
            orderItem.Name = productToAdd?.Name;

            cart.Items.Add(orderItem); // Add the order item to the list
            cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount

            DO.Product p = dal.Product.Get(id);
            p.InStock--;
            dal.Product.Update(p);
            return cart;
        }
        throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
    
}


        //Create an order
        public void MakeAnOrder(BO.Cart cart)
    {
        if (cart.Items == null)
            throw new BO.VariableIsNull("the cart is empty");


        var Do_Products = dal.Product.GetList().ToList();

        if (cart.Items!.Any(item => item?.Amount <= 0 || cart.CustomerName == null || cart.CustomerEmail == null || cart.CustomerAdress == null || !IsValid(cart.CustomerEmail)))
        {
            throw new BO.InputError("Input error");
        }

        if (cart.Items!.Any(item => Do_Products.Any(product => product?.Id == item?.ProductId && item?.Amount > product?.InStock)))
        {
            throw new BO.OutOfStock("Out of stock");
        }

        var order = new DO.Order
        {
            Id = dal.Order.GetList().LastOrDefault()?.Id + 1 ?? 0,
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAdress = cart.CustomerAdress,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null
        };
        int orderId = dal.Order.Add(order);

        foreach (var item in cart.Items)
        {
            var orderItem = new DO.OrderItem
            {
                Id = dal.OrderItem.GetList().LastOrDefault()?.Id + 1 ?? 0,
                OrderId = orderId,
                ProductId = (int)item?.ProductId!,
                Price = item.Price,
                Amount = item.Amount
            };
            int k = dal.OrderItem.Add(orderItem);

            var product = Do_Products.FirstOrDefault(p => p?.Id == item.ProductId);
            if (product != null)
            {
                var temp = new DO.Product
                {
                    Id = (int)product?.Id!,
                    Price = (double)product?.Price!,
                    Category = product?.Category,
                    InStock = (int)product?.InStock! - item.Amount,
                    Name = product?.Name
                };
                dal.Product.Update(temp);
            }
        }
        cart.Items = null;
        cart.TotalPrice = 0;
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
        if (TheNewQuantity < 0) //check for correctness
        {
            throw new BO.TheVariableIsLessThanTheNumberZero("There is no such thing as a negative quantity");
        }
        List<DO.Product?> Do_Products = new List<DO.Product?>();
        Do_Products = (List<DO.Product?>)dal.Product.GetList();

        BO.OrderItem? item = cart.Items.Where(x => x.ProductId == Id).FirstOrDefault();
        if (item != null)
        {
            if (item.Amount < TheNewQuantity) //In case he wants to add
            {
                cart.TotalPrice -= item.TotalPrice;
                DO.Product product = dal.Product.Get(Id);
                    if (product.InStock >= TheNewQuantity)
                    {
                        product.InStock -= TheNewQuantity - item.Amount;
                        item.Amount = TheNewQuantity;
                        item.TotalPrice = item.Price * TheNewQuantity;
                    dal.Product.Update(product);
                    }
                    else throw new BO.OutOfStock("Out of stock");               
                cart.TotalPrice += item.TotalPrice;
                return cart;
            }
            else
            {
                if (item.Amount > TheNewQuantity) //In case he wants to subtract
                {
                    DO.Product product = dal.Product.Get(Id);
                    if (TheNewQuantity == 0)
                    {
                       //  = TheNewQuantity;
                        product.InStock += item.Amount;                        
                        cart.TotalPrice -= item.TotalPrice;
                        cart.Items.Remove(item);
                    }
                    else
                    {
                        product.InStock += item.Amount - TheNewQuantity;

                        cart.TotalPrice -= item.TotalPrice;
                        item.Amount = TheNewQuantity;
                        item.TotalPrice = (item.Price * TheNewQuantity);
                        cart.TotalPrice += item.TotalPrice;
                    }
                    dal.Product.Update(product);

                }
                return cart;
            }
        }
        throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
    }
}
