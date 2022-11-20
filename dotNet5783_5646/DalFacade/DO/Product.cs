using System.ComponentModel;
using static DO.Enums;

namespace DO;

public struct Prodcut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public ProdactCategory Category { get; set; }
    public int InStock { get; set; }


    public override string ToString() => $@"
    Product Id - {Id}: 
    Name: {Name}, 
    Category : {Category}
    Price : {Price}
    Amount in stock: {InStock}
    ";

}
