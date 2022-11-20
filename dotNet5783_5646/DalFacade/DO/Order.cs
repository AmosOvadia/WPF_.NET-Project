using System.Diagnostics;

namespace DO;

public struct Order
{
    public int Id { get; set; }
    public string CostomerName { get; set; }
    public string CostomerEmail { get; set; }
    public string CostomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
    Id : {Id} 
    Costomer Name : {CostomerName}
    Costomer Email : {CostomerEmail}
    CostomerAdress : {CostomerAdress}
    Order Date : {OrderDate}   
    Ship Date : {ShipDate}
    Delivery Date : {DeliveryDate}
    ";

}

