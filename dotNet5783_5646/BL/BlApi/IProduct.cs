using BO;
namespace BlApi;
using static BO.Enums;



//Interface of the logical entity - Product
public interface IProduct
{
    public IEnumerable<ProductForList?> GetProducts(Func<DO.Product?, bool>? func = null); ////The function returns all products
    public Product GetProductById(int Id); //The function returns the logical entity - product, according to the id
    public ProductItem GetProductItem(int Id, Cart c); //The function returns the product item
    public void Add(Product product);//The function adds a product to the logical layer
    public void Delete(int Id);  //The function deletes a product to the logical layer
    public void Update(Product product); //The function updates the logical entity


}
