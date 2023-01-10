using System.Diagnostics;

namespace DO;

public struct Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
    Id: {Id} 
    Costomer Name: {CustomerName}
    Costomer Email: {CustomerEmail}
    CostomerAdress: {CustomerAdress}
    Order Date: {OrderDate}   
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}
    ";

   
}

