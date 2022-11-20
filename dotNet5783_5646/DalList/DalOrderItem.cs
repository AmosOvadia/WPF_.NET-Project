using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;



public class DalOrderItem
{
 
    public int addOrderItem(OrderItem ordItem)
    {
        ordItem.Id = DataSource.Config.getIdOrederItem;
        DataSource.orderItemArr[DataSource.Config.indexOrderItem++] = ordItem;
        return ordItem.Id;
    }

    public void deleteOrderItem(int id)
    {
        int i;
        for (i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (orderItemArr[i].Id == id)
                break;
        }
        if (i == DataSource.Config.indexOrderItem)
            throw new Exception("The order item does not exist");
        else
        {
            for (; i < DataSource.Config.indexOrderItem; i++)
            {
                orderItemArr[i].Id = orderItemArr[i++].Id;
            }
            DataSource.Config.indexOrderItem--;
        }
    }

    public void updateOrderItem(OrderItem orderItem)
    {

        int i;
        bool check = false;
        for (i = 0; i < (DataSource.Config.indexOrderItem)&&(!check); i++)
        {
            if (orderItemArr[i].Id == orderItem.Id)
            {
                orderItemArr[i] = orderItem;
                 check = true;
                break;
            }
        }
        if (!check)
        {
            throw new Exception("The order item does not exist");
        }
    }

    public OrderItem getOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (orderItemArr[i].Id == id)
            {
                return orderItemArr[i];
            }
        }
        throw new Exception("The order item does not exist");
    }

    public static OrderItem[] getOrderItemArray()
    {
        OrderItem[] orderItemArrey = new OrderItem[200];
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            orderItemArrey[i] = orderItemArr[i];
        }
        return orderItemArrey;
    }

    public int orderItemLeangth()
    {
        return DataSource.Config.indexOrderItem;
    }

}