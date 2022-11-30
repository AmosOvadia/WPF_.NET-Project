using BO;
namespace BlApi;

//Interface of the logical entity - Cart
public interface ICart
{
    //Adding a product to the shopping cart
    public BO.Cart Add(BO.Cart c, int id);
    public BO.Cart UpdateProductQuantity(BO.Cart C, int Id, int TheNewQuantity);
    public void MakeAnOrder(BO.Cart C);
}
