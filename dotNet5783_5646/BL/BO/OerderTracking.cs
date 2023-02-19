using static BO.Enums;
namespace BO;

//The fields of the logical entity - order tracking
public class OrderTracking
{
    public int Id { get; set; } 
    public OrderStatus? Status { get; set; }
    public List<(DateTime?, string?)>? Tracking { get; set; }

    //to print the object
    public override string ToString()
    {
        string str = "Id: " + Id + "\nStatus: " + Status + "\nTracking:\n ";
        int i = 1;
        foreach (var tracking in Tracking)
        {

            str += i + ":\n" + tracking.Item1;
            str += "\n" + tracking.Item2;
            i++;
        }
        return str; 
    }
}
