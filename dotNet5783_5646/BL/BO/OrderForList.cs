using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

//The fields of the logical entity - order for list
public class OrderForList
{
    public int Id { get; set; }
    public string CostomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }  
    public double TotalPrice { get; set; }

    //to print the object
    public override string ToString() => $@"
    Id : {Id} 
    Costomer Name : {CostomerName}
    Status: {Status}
    Amount Of Items: {AmountOfItems}
    Total Price: {TotalPrice}
    ";

}
