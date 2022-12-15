
using BlApi;


namespace BlImplementation;

internal class BL : IBl
{
    public IProduct BoProduct => new BoProduct();
    public IOrder BoOrder => new BoOrder();
    public ICart BoCart => new BoCart();

    IProduct? IBl.Product => new BoProduct(); //=> throw new BO.TheIdDoesNotExistInTheDatabase("Error");
    IOrder IBl.Order => new BoOrder();//=> throw new BO.TheIdDoesNotExistInTheDatabase("Error");
    ICart IBl.Cart => new BoCart();//=> throw new BO.TheIdDoesNotExistInTheDatabase("Error");
}
    /// <summary>
    /// 
    /// </summary>
    //IProduct IBl.Product => throw new NotImplementedException();

    //IOrder IBl.Order => throw new NotImplementedException();

    //ICart IBl.Cart => throw new NotImplementedException();


