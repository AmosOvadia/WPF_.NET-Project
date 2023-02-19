using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Dal;

internal class DalOrderItem : IOrderItem
{
    const string orderItemPath = "OrderItem";
    static XElement config = XmlTools.LoadConfig();

    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int Add(OrderItem ordItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        //If it already exists we will throw an exception
        if (listOrderItem.FirstOrDefault(orderItem => orderItem?.Id == ordItem.Id) != null)
            throw new DO.TheIDAlreadyExistsInTheDatabase("order item Id already exists");

        ordItem.Id = int.Parse(config.Element("OrderItemId")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderId", ordItem.Id);
        listOrderItem.Add(ordItem);//We will add the new order item to the list

        XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);

        return ordItem.Id;
    }
    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void Delete(int ordItemId)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (ListOrderItem.Any(orderItem => orderItem?.Id == ordItemId))
            ListOrderItem.Remove(Get(ordItemId)); //We will remove the order from the list
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("Order item not exist");

        XmlTools.SaveListToXMLSerializer(ListOrderItem, orderItemPath);
    }
    /// <summary>
    /// The operation returns the full array
    /// </summary>
    public IEnumerable<OrderItem?> GetList(Func<OrderItem?, bool>? func = null)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        var orderItems = ListOrderItem.Where(ordItem => func == null || func(ordItem)).ToList();
        return orderItems;
    }
    public OrderItem GetByDelegate(Func<OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (func != null)
        {
            var ordItem = ListOrderItem.FirstOrDefault(item => func(item));
            if (ordItem != null)
                return (DO.OrderItem)ordItem;
        } //If we did not find the order that meets the filter requirements
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("No object is of the delegate");
    }
    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem Get(int ordItemId)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        var orderItem = ListOrderItem.FirstOrDefault(x => x?.Id == ordItemId);
        if (orderItem == null)    //If the order item does not exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("order item id not found");
        else
            return (DO.OrderItem)orderItem;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int Leangth()
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        return ListOrderItem.Count();
    }
    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void Update(OrderItem ordItem)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        bool found = false;
        var foundOrderItem = ListOrderItem.FirstOrDefault(Item => Item?.Id == ordItem.Id);
        if (foundOrderItem != null)
        {
            found = true;
            int index = ListOrderItem.IndexOf(foundOrderItem);
            ListOrderItem[index] = ordItem;
        }
        if (found == false)//If we didn't update any order = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("order item id not found");
        XmlTools.SaveListToXMLSerializer(ListOrderItem, orderItemPath);
    }
}
