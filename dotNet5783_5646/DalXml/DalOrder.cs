using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
/// <summary>
/// class DalOrder: 
/// Implementation of add, delete, update and return operations
/// </summary>
internal class DalOrder : IOrder
{
    const string orderPath = "Order";
    static XElement config = XmlTools.LoadConfig();
    /// <summary>
    /// The operation accepts an order and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>

    //A function that adds an order
    public int Add(Order ord)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (ListOrder.FirstOrDefault(orderItem => orderItem?.Id == ord.Id) != null)
            throw new Exception("id already exist"); //If it already exists we will throw an exception

        ord.Id = int.Parse(config.Element("OrderId")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderId", ord.Id);
        ListOrder.Add(ord); //We will add the new order to the list

        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);

        return ord.Id;
    }
    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void Delete(int ordId)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (ListOrder.Any(order => order?.Id == ordId))
            ListOrder.Remove(Get(ordId));
        else //If we did not delete = the member did not exist, we will throw an exception
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("order does not exist");
        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);
    }
    /// <summary>
    /// The operation returns list of orders (maybe after sort)
    /// </summary>
    public IEnumerable<Order?> GetList(Func<Order?, bool>? func = null)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (func == null) // the full list
            return ListOrder.Select(lec => lec).OrderBy(lec => lec?.Id);
        else
            return ListOrder.Where(func).OrderBy(lec => lec?.Id);
    }


    //A function that returns a order according to a certain filter
    public Order GetByDelegate(Func<Order?, bool>? func)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        if (func != null)
        {
            var ord = ListOrder.FirstOrDefault(x => func(x));
            if (ord != null)
                return (DO.Order)ord;
        }      
        //If we did not find the order that meets the filter requirements
        throw new DO.TheIdentityCardDoesNotExistInTheDatabase("No object is of the delegate");
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Order Get(int ordId)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        var ord = ListOrder.FirstOrDefault(x => x?.Id == ordId);

        if (ord == null)         //If the order does not exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("Order Id not found");
        else
            return (DO.Order)ord;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int Leangth()
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        return ListOrder.Count();
    }
    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(Order order)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        bool found = false;
        var foundOrder = ListOrder.FirstOrDefault(ord => ord?.Id == order.Id);
        if (foundOrder != null)
        {
            found = true;
            int index = ListOrder.IndexOf(foundOrder);
            ListOrder[index] = order;
        }
        if (found == false) //If we didn't update any order = it didn't exist
            throw new DO.TheIdentityCardDoesNotExistInTheDatabase("Order Id not found");
        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);

    }
}
