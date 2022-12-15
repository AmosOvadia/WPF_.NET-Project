using DalApi;
using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;
using DalApi;
using System;

internal class DalOrderItem : IOrderItem
{

    //A function that adds an order item
    public int Add(OrderItem ordItem)
    {
        foreach (OrderItem? temp in orderItemList)
        {
            if (temp?.Id == ordItem.Id)
            {
                throw new TheIDAlreadyExistsInTheDatabase("The order item already exist");
            }
        }
        orderItemList.Add(ordItem);
        return ordItem.Id;
    }

    //A function that deletes an order item
    public void Delete(int id)
    {
        bool check = false;
        foreach (OrderItem? temp in orderItemList)
        {
            if (temp?.Id == id)
            {
                check = true;
                orderItemList.Remove(temp);
            }
        }
        if (!check)
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    //A function that updates an order item
    public void Update(OrderItem orderItem)
    {
        int index = 0;
        bool check = false;
        foreach (OrderItem? temp in orderItemList)
        {
            if (temp?.Id == orderItem.Id)
            {
                orderItemList[index] = orderItem;
                check = true;
            }
            index++;
        }
        if (!check) //If we didn't find the organ
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    //A function that returns order item by ID
    public OrderItem Get(int id)
    {
        foreach (OrderItem? temp in orderItemList)
        {
            if (temp?.Id == id)
            { 
                return (OrderItem)temp;  
            }
        }
        throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    //A function that returns a list of order Item
    public IEnumerable<OrderItem?> GetList(Func<OrderItem?, bool>? func)
    {
        if (func == null)
        {
            List<OrderItem?> list = new List<OrderItem?>();
            foreach (OrderItem? temp in orderItemList)
            {
                if (temp?.Id > 0) 
                list.Add(temp);//
            }
            return list;
        }
        else
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (OrderItem temp in orderItemList)
            {
                if(func(temp))
                   list.Add(temp);
            }
            return (IEnumerable<OrderItem?>)list;
        }
    }

    //A function that returns the length of the list
    public int Leangth()
    {
        return orderItemList.Capacity;
    }


  //A function that returns a order item according to a certain filter
    OrderItem ICrud<OrderItem>.GetByDelegate(Func<OrderItem?, bool>? func)
    {
        if (func != null)
        {
            foreach (OrderItem temp in orderItemList)
            {
                if (func(temp))
                {
                    return (OrderItem)temp;
                }
            }
        }
        //If we did not find the order item that meets the filter requirements
        throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }
}