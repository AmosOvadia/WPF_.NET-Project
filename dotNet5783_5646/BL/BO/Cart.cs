
using static BO.Enums;

namespace BO;

//The fields of the logical entity - cart
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? Items { get; set; }
    public double TotalPrice { get; set; }

    //to print the object
    public override string ToString()
    {
        string str = $@"
    Costomer Name : {CustomerName}
    Costomer Email : {CustomerEmail}
    CostomerAdress : {CustomerAdress}
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
