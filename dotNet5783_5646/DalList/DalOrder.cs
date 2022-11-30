using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;

using static Dal.DataSource;


internal class DalOrder : IOrder
{
    public int Add(Order ord)
    {       
        foreach (Order order in orderList)
        {
            if (order.Id == ord.Id)
            {
                throw new TheIDAlreadyExistsInTheDatabase("The order already exist");
            }
        }
        orderList.Add(ord);
        return ord.Id;
    }
    public void Delete(int id)
    {
        bool check = false;
        foreach (Order order in orderList)
        {
            if (order.Id == id)
            {
                check = true;
                orderList.Remove(order);
            }
        }
        if (!check)
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");

    }

    public Order Get(int orderId)
    {
        foreach (Order temp in orderList)
        {
            if (temp.Id == orderId)
            {
                return temp;
            }
        }
        throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
    }
    public void Update(Order order)
    {
        bool check = false;
        Order item = new Order();
        foreach (Order temp in orderList)
        {
            if (temp.Id == order.Id)
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
        if (!check)
        {
            throw new TheIdentityCardDoesNotExistInTheDatabase("The order does not exist");
        }     
    }
   
    public  IEnumerable<Order> GetList()
    {
        return orderList;
    }
    public int Leangth()
    {
        return orderList.Capacity;
    }
}
