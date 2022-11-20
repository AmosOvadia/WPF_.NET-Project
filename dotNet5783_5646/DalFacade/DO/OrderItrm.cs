using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    Id : {Id} 
    Order Id : {OrderId}
    Product Id : {ProductId}
    Price : {Price}
    Amount : {Amount}
    ";
}
