using DalApi;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;

using static Dal.DataSource;


internal class DalOrder : IOrder
{
    //A function that adds an order
    public int Add(DO.Order ord)
    {
        
        var check = (from p in orderList
                     select p?.Id).Where(temp => temp == ord.Id);


        if (check.Count() == 0)
        {
            DataSource.orderList.Add(ord); //We will add the new order to the list
            return ord.Id;
        } //If it already exists we will throw an exception
        throw new DO.TheIDAlreadyExistsInTheDatabase("The order already exist");
    }


    //A function that deletes an order 
    public void Delete(int id)
    {
        var check = (from p in orderList
                     select p?.Id).Where(temp => temp == id);
        if (check.Count() == 1)
        {
            DataSource.orderList.Remove(orderList.FirstOrDefault(item => item?.Id == id)); //We will remove the order from the list
        }
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }




    //A function that returns order item by ID
    public DO.Order Get(int orderId)
    {
        var order = orderList.FirstOrDefault(p => p?.Id == orderId);
        if (order != null)
            return (DO.Order)order;
        //If the order does not exist
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }



    //A function that updates an order
    public void Update(DO.Order order)
    {
        var temp = orderList.FirstOrDefault(p => p?.Id == order.Id);
        int i = orderList.IndexOf(temp);
        if (i != -1)
            orderList[i] = order;
        else //If we didn't update any order = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");    
    }

    //A function that returns a list of order 
    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? func)
    {
        if (func == null) // the full list
        {
            return orderList;
        }
        else
        {
            var list = orderList.Select(p => p).Where(temp => func(temp)).ToList();
            return list;
        }
    }

    //A function that returns the length of the list
    public int Leangth()
    {
        return orderList.Capacity;
    }

    //A function that returns a order according to a certain filter
    DO.Order ICrud<DO.Order>.GetByDelegate(Func<DO.Order?, bool>? func)
    {
        if (func != null)
        {
            var p = orderList.FirstOrDefault(p => func(p));
            if (p != null)
                return (DO.Order)p;
        }
        //If we did not find the order that meets the filter requirements
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }

}
