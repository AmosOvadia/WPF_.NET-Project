using BO;
namespace BlApi;


//A main interface that will bring together all the interfaces of the logical layer
public interface IBl
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public ICart Cart { get; }
}
