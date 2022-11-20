using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;
using static Dal.DataSource;


public class DalOrder
{
    public int addOrder(Order ord)
    {
        ord.Id = DataSource.Config.getIdOreder;
        DataSource.orderArr[DataSource.Config.indexOrder++] = ord;
        return ord.Id;
    }

    public void deleteOrder(int id)
    {
        int i;
        for (i = 0; i < DataSource.Config.indexOrder; i++)
        {
            if (orderArr[i].Id == id)
                break;
        }
        if (i == DataSource.Config.indexOrder)
            throw new Exception("The order does not exist");
        else
        {
            for (; i < DataSource.Config.indexOrder; i++)
            {
                orderArr[i] = orderArr[i++];
            }
            DataSource.Config.indexOrder--;
        }
    }

    public void updateOrder(Order order)
    {
        int i;
        bool check = false;
        for (i = 0; i < (DataSource.Config.indexOrder) && (!check); i++)
        {
            if (orderArr[i].Id == order.Id)
            {
                orderArr[i] = order;
                check = true;
                break;
            }
        }
        if (!check)
        {
            throw new Exception("The order does not exist");
        }
    }
    public Order getOrder(int orderId)
    {
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            if (orderArr[i].Id == orderId)
                return orderArr[i];
        }
        throw new Exception("The order does not exist");
    }


    public static Order[] getOrderArray()
    {
        Order[] orderArrey = new Order[100];
        for (int i = 0; i < DataSource.Config.indexOrder; i++)
        {
            orderArrey[i] = orderArr[i];
        }
        return orderArrey;
    }
  
    public int orderLeangth()
    {
        return DataSource.Config.indexOrder;
    }
}
