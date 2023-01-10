using static BO.Enums;
namespace BO;

//The fields of the logical entity - order
public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public OrderStatus? Status { get; set; } 
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem?>? Items { get; set; } 
    public double TotalPrice { get; set; }
    //to print the object
    public override string ToString()
    {
      string str =  $@"
    Id: {Id} 
    Costomer Name: {CustomerName}
    Costomer Email: {CustomerEmail}
    CostomerAdress: {CustomerAdress}
    Status: {Status}
    Order Date: {OrderDate}   
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}
    TotalPrice: {TotalPrice}
    Items:
    ";
        int i = 1;
        if (Items != null)
        {
            foreach (BO.OrderItem? item in Items)
            {
                str += $@" {i}:
            Id:{item?.Id}
            Name:{item?.Name}
            Price:{item?.Price}
            ProductId: {item?.ProductId}
            Amount: {item?.Amount}
            TotalPrice: {item?.TotalPrice}
            ";
                i++;
            }
        }
        return str;
    }
}
