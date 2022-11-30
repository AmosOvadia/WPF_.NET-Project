using DalApi;
using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;
using DalApi;


internal class DalOrderItem : IOrderItem
{

    public int Add(OrderItem ordItem)
    {
        foreach (OrderItem temp in orderItemList)
        {
            if (temp.Id == ordItem.Id)
            {
                throw new TheIDAlreadyExistsInTheDatabase("The order item already exist");
            }
        }
        orderItemList.Add(ordItem);
        return ordItem.Id;
    }

    public void Delete(int id)
    {
        bool check = false;
        foreach (OrderItem temp in orderItemList)
        {
            if (temp.Id == id)
            {
                check = true;
                orderItemList.Remove(temp);
            }
        }
        if (!check)
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    public void Update(OrderItem orderItem)
    {
        int index = 0;
        bool check = false;
        foreach (OrderItem temp in orderItemList)
        {
            if (temp.Id == orderItem.Id)
            {
                orderItemList[index] = orderItem;
                check = true;
            }
            index++;
        }
        if (!check)
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }


    public OrderItem Get(int id)
    {
        foreach (OrderItem temp in orderItemList)
        {
            if (temp.Id == id)
            { 
                return temp;  
            }
        }
        throw new TheIdentityCardDoesNotExistInTheDatabase("The order item does not exist");
    }

    public  IEnumerable<OrderItem> GetList()
    {
        //bool check = false;
        //List<OrderItem> list = new List<OrderItem>();
        //foreach (OrderItem temp in orderItemList)
        //{
        //    list.Add(temp);
        //}
        //return list;
        return orderItemList;
    }


    public int Leangth()
    {
        return orderItemList.Capacity;
    }

}