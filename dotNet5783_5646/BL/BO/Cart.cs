
using static BO.Enums;

namespace BO;

//The fields of the logical entity - cart
public class Cart
{
    public string CostomerName { get; set; }
    public string CostomerEmail { get; set; }
    public string CostomerAdress { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }


    //to print the object
    public override string ToString()
    {
        string str = $@"
    Costomer Name : {CostomerName}
    Costomer Email : {CostomerEmail}
    CostomerAdress : {CostomerAdress}
    ";
        int i = 1;
        if (Items != null)
        {
            foreach (var item in Items)
            {
                str += $@" {i++}:
            Id:{item.Id}
            Name:{item.Name}
            Price:{item.Price}
            ProductId: {item.ProductId}
            Amount: {item.Amount}
            TotalPrice: {item.TotalPrice}
            ";
            }
            
        }
        return str;
    }
    

}
