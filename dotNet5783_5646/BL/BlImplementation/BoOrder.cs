using BlApi;
using Dal;
using DalApi;
using static BO.Enums;
using static DO.Enums;

namespace BlImplementation;

//The implementation of the interface of the logical entity - Order

internal class BoOrder : BlApi.IOrder
{
    private IDal dal = new Dal.DalList1();

    // The function returns a list of all orders
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>(); 
        List<DO.Order> DoOrders = new List<DO.Order>(); 
        List<DO.OrderItem> DoOrderItems = new List<DO.OrderItem>();

        DoOrders = (List<DO.Order>)dal.Order.GetList();
        DoOrderItems = (List<DO.OrderItem>)dal.OrderItem.GetList();



        foreach (DO.Order DoOrder in DoOrders) //We will go through all the products from the data layer
        {
            BO.OrderForList orderForList1 = new BO.OrderForList();
            orderForList1.Id = DoOrder.Id;
            orderForList1.CostomerName = DoOrder.CostomerName;
            if (DoOrder.DeliveryDate != null) //check that the date exists
            {
                orderForList1.Status = OrderStatus.Delivered;
            }
            else if (DoOrder.ShipDate != null)//check that the date exists
            {
                orderForList1.Status = OrderStatus.Sent;
            }
            else
            {
                orderForList1.Status = OrderStatus.Confirmed;
            }
            foreach (var item in DoOrderItems) //We will go over all order items from the data layer
            {
                if (item.OrderId == orderForList1.Id)
                {
                    orderForList1.TotalPrice += item.Price * item.Amount;
                    orderForList1.AmountOfItems = item.Amount;
                }
            }

            orderForList.Add(orderForList1); //We will add to the list of orders in the logical layer
        }
        return orderForList;
    }


    // The function returns the logical entity - order, by Id
    public BO.Order GetOrder(int id)
    {
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem> DoOrderItem = new List<DO.OrderItem>();
        List<DO.Product> DoProducts = new List<DO.Product>();
        List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
        BO.Order? BoOrder = new BO.Order();
        if (id > 0) //Check if the ID is valid
        {
            int i = 0;
            double finalTotalPrice = 0;
            DoOrder = dal.Order.Get(id);
            DoOrderItem = (List<DO.OrderItem>)dal.OrderItem.GetList();
            DoProducts = (List<DO.Product>)dal.Product.GetList();

            BoOrder.Id = DoOrder.Id;
            BoOrder.CostomerName = DoOrder.CostomerName;
            BoOrder.CostomerEmail = DoOrder.CostomerEmail;
            BoOrder.CostomerAdress = DoOrder.CostomerAdress;
            BoOrder.OrderDate = (DateTime)DoOrder.OrderDate;
            if (DoOrder.ShipDate != null) //Check if the date exists
                BoOrder.ShipDate = (DateTime)DoOrder.ShipDate;
            if (DoOrder.DeliveryDate != null) //Check if the date exists
                BoOrder.DeliveryDate = (DateTime)DoOrder.DeliveryDate;

            foreach (DO.OrderItem item in DoOrderItem) //We will go through all the OrderItem from the data layer
            {
                if (id == item.OrderId)
                {
                    BO.OrderItem boOrderItem = new BO.OrderItem();
                    boOrderItem.Id = item.OrderId;
                    boOrderItem.ProductId = item.ProductId;
                    boOrderItem.Price = item.Price;
                    boOrderItem.Amount = item.Amount;
                    boOrderItem.TotalPrice = item.Price * item.Amount;
                    finalTotalPrice += item.Price * item.Amount;
                    foreach (var product in DoProducts) //We will go over the entire product from the data layer
                    {
                        if (boOrderItem.ProductId == product.Id)
                        {
                            boOrderItem.Name = product.Name;
                            break;
                        }
                    }
                   
                    boOrderItems.Add(boOrderItem); //We will add to the list of order items in the logical layer     
                }

            }
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
    public BO.OerderTracking Order_tracking(int id)
    {
        string Item;
        List<DO.Order> DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetList();
        BO.OerderTracking BoOrderTracking = new BO.OerderTracking();
        bool check = false; //Does such an ID exist?
        foreach (DO.Order doOrder in DoOrders) // We will go through each order from the data layer
        {
            if (id == doOrder.Id)
            {
                check = true; //Such an ID exists
                BoOrderTracking.Id = (int)doOrder.Id;
                if (doOrder.DeliveryDate != null && doOrder.DeliveryDate <= DateTime.Now)
                {
                    BoOrderTracking.Status = OrderStatus.Delivered;
                    Item = "the package was deliverd";
                }
                else if (doOrder.ShipDate != null && doOrder.ShipDate <= DateTime.Now) 
                {
                    BoOrderTracking.Status = OrderStatus.Sent;
                    Item = "the package was sent";
                }
                else
                {
                    BoOrderTracking.Status = OrderStatus.Confirmed;
                    Item = "the order is confermed in the system";
                }
                var tupleList = new List<(DateTime? myTime, string Name)>
                {
                     (DateTime.Now, Item)
                };
                BoOrderTracking.Tracking = tupleList;
                break;
            }
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
        List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
        List<DO.Product> DoProducts = new List<DO.Product>();
        DoProducts = (List<DO.Product>)dal.Product.GetList();
        double finalTotalPrice = 0;
        List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
        orderItems = (List<DO.OrderItem>)dal.OrderItem.GetList();
        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetList();
        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool check = false;
        foreach (DO.Order doOrder in DoOrders) //We will go through each order from the data layer
        {
            if (id == doOrder.Id)
            {
                check = true;
                if (doOrder.ShipDate == null) //check that the date exists
                {
                    BoOrder.ShipDate = temp.ShipDate = DateTime.Now;
                }
                else
                {
                    BoOrder.ShipDate = temp.ShipDate = doOrder.ShipDate;
                }
                BoOrder.Id = temp.Id = doOrder.Id;
                BoOrder.CostomerName = temp.CostomerName = doOrder.CostomerName;
                BoOrder.CostomerAdress = temp.CostomerAdress = doOrder.CostomerAdress;
                BoOrder.CostomerEmail = temp.CostomerEmail = doOrder.CostomerEmail;
                BoOrder.OrderDate = temp.OrderDate = doOrder.OrderDate;


                if (doOrder.DeliveryDate != null) //check that the date exists
                    BoOrder.DeliveryDate = temp.DeliveryDate = doOrder.DeliveryDate;
                BoOrder.Status = OrderStatus.Sent;

                
                foreach (var item in orderItems) //We will go through each order item from the data layer
                {
                    if (id == item.OrderId)
                    {
                        BO.OrderItem boOrderItem = new BO.OrderItem();
                        boOrderItem.Id = item.OrderId;
                        boOrderItem.ProductId = item.ProductId;
                        boOrderItem.Price = item.Price;
                        boOrderItem.Amount = item.Amount;
                        boOrderItem.TotalPrice = item.Price * item.Amount;
                        finalTotalPrice += item.Price * item.Amount;
                        foreach (var product in DoProducts)
                        {
                            if (boOrderItem.ProductId == product.Id)
                            {
                                boOrderItem.Name = product.Name;
                                break;
                            }
                        }
                        
                        
                        
                        boOrderItems.Add(boOrderItem); //We will add order item to the logical layer

                    }
                }
            }
        }
            if (check)
            {
            BoOrder.TotalPrice = finalTotalPrice;
            BoOrder.Items = boOrderItems;
            dal.Order.Update(temp); //We will update the Order object in the data layer
            BoOrder.TotalPrice = finalTotalPrice;
                return BoOrder;
            }

            throw new BO.TheIdDoesNotExistInTheDatabase("No Id in list");
        
    }

    //  The function updates the shipment's arrival date
    public BO.Order OrderDeliveryUpdate(int id)
    {

        List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
        List<DO.Product> DoProducts = new List<DO.Product>();
        DoProducts = (List<DO.Product>)dal.Product.GetList();
        double finalTotalPrice = 0;
        List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
        orderItems = (List<DO.OrderItem>)dal.OrderItem.GetList();
        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetList();
        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool check = false;
        foreach (DO.Order doOrder in DoOrders) ////We will go through each order from the data layer
        {
            if (id == doOrder.Id) //Is this the order we are looking for?
            {
                check = true;
                if (doOrder.DeliveryDate == null) //Does the date exist?
                {
                    BoOrder.DeliveryDate = temp.DeliveryDate = DateTime.Now;
                }
                else
                {
                    BoOrder.DeliveryDate = temp.DeliveryDate = doOrder.DeliveryDate;
                }
                BoOrder.Id = temp.Id = doOrder.Id;
                BoOrder.CostomerName = temp.CostomerName = doOrder.CostomerName;
                BoOrder.CostomerAdress = temp.CostomerAdress = doOrder.CostomerAdress;
                BoOrder.CostomerEmail = temp.CostomerEmail = doOrder.CostomerEmail;
                BoOrder.OrderDate = temp.OrderDate = doOrder.OrderDate;


                if (doOrder.ShipDate != null) //Does the date exist?
                    BoOrder.ShipDate = temp.ShipDate = doOrder.ShipDate;
                BoOrder.Status = OrderStatus.Sent;


                foreach (var item in orderItems) //We will go through each order item from the data layer
                {
                    if (id == item.OrderId) //Is this the order item we are looking for?
                    {
                        BO.OrderItem boOrderItem = new BO.OrderItem();
                        boOrderItem.Id = item.OrderId;
                        boOrderItem.ProductId = item.ProductId;
                        boOrderItem.Price = item.Price;
                        boOrderItem.Amount = item.Amount;
                        boOrderItem.TotalPrice = item.Price * item.Amount;
                        finalTotalPrice += item.Price * item.Amount;
                        foreach (var product in DoProducts)  //We will go through each product from the data layer
                        {
                            if (boOrderItem.ProductId == product.Id)//Is this the product we are looking for?
                            {
                                boOrderItem.Name = product.Name;
                                break;
                            }
                        }



                        boOrderItems.Add(boOrderItem); //We will add order item to the logical layer

                    }
                }
            }
        }
        if (check)
        {
            BoOrder.TotalPrice = finalTotalPrice;
            BoOrder.Items = boOrderItems;
            dal.Order.Update(temp); //We will update the Order object in the data layer
            BoOrder.TotalPrice = finalTotalPrice;
            return BoOrder;
        }

        throw new BO.TheIdDoesNotExistInTheDatabase("No Id in list");
    }

 }









