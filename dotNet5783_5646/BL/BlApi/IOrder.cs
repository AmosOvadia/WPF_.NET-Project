using BO;
using DalApi;
using static BO.Enums;


//Interface of the logical entity - Order
namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList?> GetOrders(); // The function returns a list of all orders
    public Order GetOrder(int Id); // The function returns the logical entity - order, by Id
    public Order OrderShippingUpdate(int Id);  // The function updates the dispatch date of the shipment
    public Order OrderDeliveryUpdate(int Id); //  The function updates the shipment's arrival date
    public OerderTracking Order_tracking(int Id);  // The function returns the tracking of the order
    public void UpdateOrederManager(int amount, int orderId, int prodId); // Possibility for the administrator to change product details in the order
}


