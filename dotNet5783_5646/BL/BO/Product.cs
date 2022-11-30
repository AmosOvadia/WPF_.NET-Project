

using static BO.Enums;

namespace BO;

//The fields of the logical entity - product
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public ProdactCategory Category { get; set; }
    public int InStock { get; set; }


    //to print the object
    public override string ToString() => $@"
    Product Id - {Id} 
    Name: {Name}, 
    Category: {Category}
    Price: {Price}
    Amount in stock: {InStock}
    ";
}
