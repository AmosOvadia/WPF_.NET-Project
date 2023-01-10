using DalApi;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;
using DalApi;
using System;
using System.Security.Cryptography;

internal class DalOrderItem : IOrderItem
{   

    //A function that adds an order item
    public int Add(DO.OrderItem ordItem)
    {
        var check = (from p in orderItemList
                     select p?.Id).Where(temp => temp == ordItem.Id);


        if (check.Count() == 0)
        {
            DataSource.orderItemList.Add(ordItem); //We will add the new order item to the list
            return ordItem.Id;
        } //If it already exists we will throw an exception
        throw new DO.TheIDAlreadyExistsInTheDatabase("The order item already exist");
    }

    //A function that deletes an order item
    public void Delete(int id)
    {
        var check = (from p in orderItemList
                     select p?.Id).Where(temp => temp == id);
        if (check.Count() == 1)
        {
            DataSource.orderItemList.Remove(orderItemList.FirstOrDefault(item => item?.Id == id)); //We will remove the order from the list
        }
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }



    //A function that updates an order item
    public void Update(DO.OrderItem orderItem)
    {
        var temp = orderItemList.FirstOrDefault(p => p?.Id == orderItem.Id);
        int i = orderItemList.IndexOf(temp);
        if (i != -1)
            orderItemList[i] = orderItem;
        else //If we didn't update any order = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    //A function that returns order item by ID
    public DO.OrderItem Get(int id)
    {
        var orderItem = orderItemList.FirstOrDefault(p => p?.Id == id);
        if (orderItem != null)
            return (DO.OrderItem)orderItem;
        //If the order item does not exist
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    //A function that returns a list of order Item
    public IEnumerable<DO.OrderItem?> GetList(Func<DO.OrderItem?, bool>? func)
    {
        if (func == null)
        { 
            return orderItemList;
        }
        else
        {
            var list = orderItemList.Select(p => p).Where(temp => func(temp)).ToList();
            return list;
        }
    }


    //A function that returns the length of the list
    public int Leangth()
    {
        return orderItemList.Capacity;
    }


    //A function that returns a order item according to a certain filter
    DO.OrderItem ICrud<DO.OrderItem>.GetByDelegate(Func<DO.OrderItem?, bool>? func)
    {
        if (func != null)
        {
            var p = orderItemList.FirstOrDefault(p => func(p));
            if (p != null)
                return (DO.OrderItem)p;
        }
        //If we did not find the order that meets the filter requirements
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }
}