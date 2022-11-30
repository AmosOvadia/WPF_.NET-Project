
using BlApi;


namespace BlImplementation;

sealed public class Bl : IBl
{
    public IProduct BoProduct => new BoProduct();
    public IOrder BoOrder => new BoOrder();
    public ICart BoCart => new BoCart();
    /// <summary>
    /// 
    /// </summary>
    IProduct IBl.Product => throw new NotImplementedException();

    IOrder IBl.Order => throw new NotImplementedException();

    ICart IBl.Cart => throw new NotImplementedException();
}

