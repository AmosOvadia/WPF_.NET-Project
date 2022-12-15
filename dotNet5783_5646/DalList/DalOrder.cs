using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;

using static Dal.DataSource;


internal class DalOrder : IOrder
{
    //A function that adds an order
    public int Add(Order ord)
    {       
        foreach (Order? order in orderList)
        {
            if (order?.Id == ord.Id)
            {
                throw new TheIDAlreadyExistsInTheDatabase("The order already exist");               
            }
        }
        orderList.Add(ord);
        return ord.Id;
    }

    //A function that deletes an order 
    public void Delete(int id)
    {
        bool check = false;
        foreach (Order? order in orderList)
        {
            if (order?.Id == id)
            {
                check = true;
                orderList.Remove(order);
            }
        }
        if (!check)
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");

    }

    //A function that returns order item by ID
    public Order Get(int orderId)
    {
        foreach (Order? temp in orderList)
        {
            if (temp?.Id == orderId)
            {
                return (Order)temp;
            }
        }
        throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }

    //A function that updates an order
    public void Update(Order order)
    {
        bool check = false;
        Order item = new Order();
        foreach (Order? temp in orderList)
        {
            if (temp?.Id == order.Id)
            {
                check = true;
                item.Id = order.Id;
                item.CostomerName = order.CostomerName;
                item.CostomerAdress = order.CostomerAdress;
                item.CostomerEmail = order.CostomerEmail;
                item.OrderDate = order.OrderDate;   
                if(order.ShipDate != null)
                    item.ShipDate = order.ShipDate;
                if (order.DeliveryDate != null)
                    item.DeliveryDate = order.DeliveryDate;
            }
        }
        if (!check) //If we didn't find the organ
        {
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
        }     
    }

    //A function that returns a list of order 
    public IEnumerable<Order?> GetList(Func<Order?, bool>? func)
    {
        if (func == null)
        {
            List<Order?> list = new List<Order?>();
            foreach (Order? item in orderList)
            {
                if (item?.Id > 0)
                    list.Add(item);
            }
            return list;
        }
        else
        {
            List<Order?> list = new List<Order?>();
            foreach (Order? item in orderList)
            {
                if (func(item))
                    list.Add(item);
            }
            return list;
        }
    }

    //A function that returns the length of the list
    public int Leangth()
    {
        return orderList.Capacity;
    }

    //A function that returns a order according to a certain filter
    Order ICrud<Order>.GetByDelegate(Func<Order?, bool>? func)
    {
        if (func != null)
        {
            foreach (Order temp in orderList)
            {
                if (func(temp))
                {
                    return temp;
                }
            }
        }
        //If we did not find the order that meets the filter requirements

        throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }

    }
