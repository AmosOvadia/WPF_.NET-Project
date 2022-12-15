using static BO.Enums;
namespace BO;

//The fields of the logical entity - product for list
public class ProductForList
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ProdactCategory? Category { get; set; }


    //to print the object
    public override string ToString() => $@"
    Product Id - {Id} 
    Name: {Name} 
    Category: {Category}
    Price: {Price}
    ";
}
