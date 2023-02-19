using BlApi;
using DalApi;
using System.Diagnostics;
using static BO.Enums;
using static DO.Enums;
using System.Linq;
using System.Security.Cryptography;

namespace BlImplementation;

//The implementation of the interface of the logical entity - Order

internal class BoOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    // The function returns a list of all orders
    public IEnumerable<BO.OrderForList?> GetOrders(Func<DO.Order?, bool>? filter )
    {
       // List<BO.OrderForList?> orderForList = new List<BO.OrderForList?>(); 
        List<DO.Order?> DoOrders ; 
        List<DO.OrderItem?> DoOrderItems = new List<DO.OrderItem?>();

        DoOrders = dal.Order.GetList().ToList();
        DoOrderItems = (List<DO.OrderItem?>)dal.OrderItem.GetList();


        if (filter == null)
        {
            var ordersForList = DoOrders.Select(doOrder => new BO.OrderForList
            {
                Id = (int)(doOrder?.Id)!,
                CostomerName = doOrder?.CustomerName,
                Status = doOrder?.DeliveryDate != null ? OrderStatus.Delivered : doOrder?.ShipDate != null ? OrderStatus.Sent : OrderStatus.Confirmed,
                TotalPrice = (double)DoOrderItems.Where(item => item?.OrderId == doOrder?.Id).Sum(item => item?.Price * (int)item?.Amount!)!,
                AmountOfItems = DoOrderItems.Where(item => item?.OrderId == doOrder?.Id).Sum(item => (int)item?.Amount!)
            }).ToList();
            return ordersForList!;
        }
        else
        {
            var ordersForList = DoOrders.Where(o => filter(o) == true).Select(doOrder => new BO.OrderForList
            {
                Id = (int)(doOrder?.Id)!,
                CostomerName = doOrder?.CustomerName,
                Status = doOrder?.DeliveryDate != null ? OrderStatus.Delivered : doOrder?.ShipDate != null ? OrderStatus.Sent : OrderStatus.Confirmed,
                TotalPrice = (double)DoOrderItems.Where(item => item?.OrderId == doOrder?.Id).Sum(item => item?.Price * (int)item?.Amount!)!,
                AmountOfItems = DoOrderItems.Where(item => item?.OrderId == doOrder?.Id).Sum(item => (int)item?.Amount!)
            }).ToList();
            return ordersForList!;
        }
    }


    // The function returns the logical entity - order, by Id
    public BO.Order GetOrder(int id)
    {
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem?> DoOrderItem = new List<DO.OrderItem?>();
        List<DO.Product?> DoProducts = new List<DO.Product?>();
       // List<BO.OrderItem?> boOrderItems = new List<BO.OrderItem?>();
        BO.Order? BoOrder = new BO.Order();
        if (id > 0) //Check if the ID is valid
        {
            int i = 0;
            double finalTotalPrice = 0;
            DoOrder = dal.Order.Get(id);
            DoOrderItem = (List<DO.OrderItem?>)dal.OrderItem.GetList();
            DoProducts = dal.Product.GetList().ToList();

            BoOrder.Id = DoOrder.Id;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAdress = DoOrder.CustomerAdress;
            BoOrder.OrderDate = (DateTime)DoOrder.OrderDate!;
            if (DoOrder.ShipDate != null) //Check if the date exists
                BoOrder.ShipDate = (DateTime)DoOrder.ShipDate;
            if (DoOrder.DeliveryDate != null) //Check if the date exists
                BoOrder.DeliveryDate = (DateTime)DoOrder.DeliveryDate;


            var boOrderItems = DoOrderItem
       .Where(item => item?.OrderId == id)
       .Select(item =>
       {
           int? amount = item?.Amount;
           return new BO.OrderItem
           {
               Id = (int)(item?.OrderId)!,
               ProductId = (int)(item?.ProductId)!,
               Price = (double)(item?.Price)!,
               Amount = (int)(item?.Amount)!,
               TotalPrice = (double)(item?.Price * amount)!,
               Name = DoProducts.First(product => product?.Id == item?.ProductId)?.Name
           };
       })
       .ToList();

            finalTotalPrice = (double)boOrderItems.Sum(item => item?.TotalPrice)!;
            BoOrder.Items = boOrderItems;
            BoOrder.TotalPrice = finalTotalPrice;
            if (DoOrder.DeliveryDate <= DateTime.Now)
            {
                BoOrder.Status = OrderStatus.Delivered;
            }
            else if (DoOrder.ShipDate <= DateTime.Now)
            {
                BoOrder.Status = OrderStatus.Sent;
            }
            else
            {
                BoOrder.Status = OrderStatus.Confirmed;
            }

            return BoOrder;
        }
        throw new BO.TheVariableIsLessThanTheNumberZero("id is smaller than 0");
    }


    // The function returns the tracking of the order
    public BO.OrderTracking Order_tracking(int id)
    {
        string Item;
        List<DO.Order?> DoOrders;
        DoOrders = dal.Order.GetList().ToList();
        BO.OrderTracking BoOrderTracking = new BO.OrderTracking();
        bool check = false; //Does such an ID exist?


        var query = DoOrders.Where(x => x?.Id == id)
          .Select(x => new BO.OrderTracking
          {
              Id = (int)(x?.Id)!,
              Status = (x?.DeliveryDate != null && x?.DeliveryDate <= DateTime.Now) ? OrderStatus.Delivered :
          (x?.ShipDate != null && x?.ShipDate <= DateTime.Now) ? OrderStatus.Sent : OrderStatus.Confirmed,
              Tracking = new List<(DateTime? myTime, string? Name)>
           {
            (DateTime.Now, (x?.DeliveryDate != null && x?.DeliveryDate <= DateTime.Now) ? "the package was deliverd" :
            (x?.ShipDate != null && x?.ShipDate <= DateTime.Now) ? "the package was sent" : "the order is confirmed in the system")
           }
          }).FirstOrDefault();
        if (query != null)
        {
            check = true;
            BoOrderTracking = query;
        }
        if (!check) //If there was no such ID
        {
            throw new BO.TheIdDoesNotExistInTheDatabase("The Id Does Not Exist");
        }
        return BoOrderTracking;

    }



    // The function updates the dispatch date of the shipment
    public BO.Order OrderShippingUpdate(int id)
    {
        List<BO.OrderItem?> boOrderItems = new List<BO.OrderItem?>();
        List<DO.Product?> DoProducts;
        DoProducts = dal.Product.GetList().ToList();
        double finalTotalPrice = 0;
        List<DO.OrderItem?> orderItems;
        orderItems = dal.OrderItem.GetList().ToList();

        List<DO.Order?> DoOrders;
        DoOrders = (List<DO.Order?>)dal.Order.GetList().ToList();

        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool check = false;
        var doOrder = DoOrders.Where(o => o?.Id == id).FirstOrDefault();
        if (doOrder != null)
        {
            check = true;
            if (doOrder?.ShipDate == null)
                BoOrder.ShipDate = temp.ShipDate = DateTime.Now;
            else
                BoOrder.ShipDate = temp.ShipDate = doOrder?.ShipDate;
            BoOrder.Id = temp.Id = (int)(doOrder?.Id)!;
            BoOrder.CustomerName = temp.CustomerName = doOrder?.CustomerName;
            BoOrder.CustomerAdress = temp.CustomerAdress = doOrder?.CustomerAdress;
            BoOrder.CustomerEmail = temp.CustomerEmail = doOrder?.CustomerEmail;
            BoOrder.OrderDate = temp.OrderDate = doOrder?.OrderDate;

            if (doOrder?.DeliveryDate != null)
                BoOrder.DeliveryDate = temp.DeliveryDate = doOrder?.DeliveryDate;
            BoOrder.Status = OrderStatus.Sent;

            var orderItemsForOrder = orderItems.Where(i => i?.OrderId == id);
            foreach (var item in orderItemsForOrder)
            {
                BO.OrderItem boOrderItem = new BO.OrderItem();
                boOrderItem.Id = (int)(item?.OrderId)!;
                boOrderItem.ProductId = (int)(item?.ProductId)!;
                boOrderItem.Price = (double)(item?.Price)!;
                boOrderItem.Amount = (int)(item?.Amount)!;
                boOrderItem.TotalPrice = (double)(item?.Price * item?.Amount)!;
                finalTotalPrice += (double)item?.Price! * (int)item?.Amount!;
                var product = DoProducts.Where(p => p?.Id == boOrderItem.ProductId).FirstOrDefault();
                if (product != null)
                    boOrderItem.Name = product?.Name;
                boOrderItems.Add(boOrderItem);
            }
        }
        if (check)
        {
            BoOrder.TotalPrice = finalTotalPrice;
            BoOrder.Items = boOrderItems;
            dal.Order.Update(temp);
            BoOrder.TotalPrice = finalTotalPrice;
            return BoOrder;
        }
        throw new BO.TheIdDoesNotExistInTheDatabase("No Id in list");      
    }


    //  The function updates the shipment's arrival date
    public BO.Order OrderDeliveryUpdate(int id)
    {

        List<BO.OrderItem?> boOrderItems = new List<BO.OrderItem?>();
        List<DO.Product?> DoProducts;
        DoProducts = dal.Product.GetList().ToList();
        double finalTotalPrice = 0;
        List<DO.OrderItem?> orderItems;
        orderItems = dal.OrderItem.GetList().ToList();
        List<DO.Order?> DoOrders ;
        DoOrders = dal.Order.GetList().ToList();
        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool check = false;




        var boOrder = DoOrders.Where(x => x?.Id == id)
.Select(x => new BO.Order
{
    DeliveryDate = x?.DeliveryDate ?? DateTime.Now,
    Id = (int)(x?.Id)!,
    CustomerName = x?.CustomerName,
    CustomerAdress = x?.CustomerAdress,
    CustomerEmail = x?.CustomerEmail,
    OrderDate = x?.OrderDate,
    ShipDate = x?.ShipDate,
    Status = OrderStatus.Sent,
    TotalPrice = (double)orderItems.Where(y => y?.OrderId == id)
.Select(y => y?.Price * y?.Amount)
.Sum()!,
    Items = orderItems.Where(y => y?.OrderId == id)
.Select(y => new BO.OrderItem
{
    Id = (int)(y?.OrderId)!,
    ProductId = (int)y?.ProductId!,
    Price = (double)y?.Price!,
    Amount = (int)y?.Amount!,
    TotalPrice = (double)y?.Price! * (double)y?.Amount!,
    Name = DoProducts.Where(z => z?.Id == y?.ProductId)
.Select(z => z?.Name)
.FirstOrDefault()
}).ToList()!
}).FirstOrDefault();

        if (boOrder != null)
        {
            dal.Order.Update(new DO.Order
            {
                DeliveryDate = boOrder.DeliveryDate,
                Id = boOrder.Id,
                CustomerName = boOrder.CustomerName,
                CustomerAdress = boOrder.CustomerAdress,
                CustomerEmail = boOrder.CustomerEmail,
                OrderDate = boOrder.OrderDate,
                ShipDate = boOrder.ShipDate,
                //Status = boOrder.Status
            });
            return boOrder;
        }

        throw new BO.TheIdDoesNotExistInTheDatabase("No Id in list");
    }


    //Possibility for the administrator to change product details in the order
    public void UpdateOrederManager(int amount, int orderId, int prodId)
    {
        







        BO.Order ord = GetOrder(orderId);
        List<DO.Product> products ;
        products = (List<DO.Product>)dal.Product.GetList();
        DO.Product product = new DO.Product();
        BO.OrderItem order_item = new BO.OrderItem();
        List<BO.OrderItem> order_items = new List<BO.OrderItem>();

        if (ord.Items == null) // If there are no products
        {
            throw new BO.VariableIsNull("can't find the list of items");
        }

        foreach (BO.OrderItem orderItem in ord.Items)
        {
            if (prodId == orderItem.ProductId)
            {
                if (orderItem.Amount + amount < 0) //Is the amount the manager entered possible
                {
                    throw new BO.TheVariableIsLessThanTheNumberZero("The quantity is less than the quantity to be reduced");
                }

                DO.Product? prod = products.FirstOrDefault(p => p.Id == prodId);
                if (prod == null)
                {
                    throw new BO.TheIdDoesNotExistInTheDatabase("product id not exists");
                }

                if (amount > prod?.InStock) //Is the amount the manager entered possible
                {
                    throw new BO.TheIdDoesNotExistInTheDatabase("not existing enough items in stock");
                }

                product.Id = (int)prod?.Id!;
                product.Name = prod?.Name;
                product.Price = (double)prod?.Price!;
                product.InStock -= amount;
                product.Category = prod?.Category;
                dal.Product.Update(product);

                order_item.Price = orderItem.Price;
                order_item.Id = orderItem.ProductId;
                order_item.Amount = orderItem.Amount + amount;
                order_item.Id = orderItem.Id;
                order_item.Name = orderItem.Name;
                order_item.TotalPrice = orderItem.TotalPrice + amount * order_item.Price;
                order_items.Add(order_item);
                ord.TotalPrice += amount * order_item.Price;
            }
            else
            {
                order_items.Add(orderItem);
            }
        }
        ord.Items = order_items;
    }
    public int? PriorityOrder(Func<DO.Order?, bool>? filter = null)
    {
        DO.Order order = new DO.Order();

        if (filter != null)
        {
            order = dal.Order.GetByDelegate(filter);
        }
        else
        {
            DO.Order? OrderOrderDate = dal?.Order.GetList(x => x?.ShipDate == null).MinBy(x => x?.OrderDate);
            DO.Order? OrderShipDate = dal?.Order.GetList(x => x?.DeliveryDate == null).MinBy(x => x?.ShipDate);

            if (OrderOrderDate != null && OrderOrderDate?.OrderDate < OrderShipDate?.ShipDate)
            {
                return OrderOrderDate?.Id;
            }
            else if (OrderShipDate != null)
            {
                return OrderShipDate?.Id;
            }

            return null;
        }


        return order.Id;
    }


}









