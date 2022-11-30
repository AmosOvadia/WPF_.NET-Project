using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

//The fields of the logical entity - order item
public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    //to print the object
    public override string ToString() => $@"
    Id: {Id} 
    Costomer Name: {Name}
    Product Id: {ProductId}
    Price: {Price}
    Amount: {Amount}
    TotalPrice: {TotalPrice}
    ";
}
