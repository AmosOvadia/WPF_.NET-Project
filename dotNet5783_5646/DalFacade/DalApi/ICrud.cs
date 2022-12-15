using DO;
namespace DalApi;


public interface ICrud<T> where T : struct
{
    int Add(T var);
    void Delete(int id);
    T Get(int id);
    void Update(T var);
    IEnumerable<T?> GetList(Func<T?, bool>? func = null);
    int Leangth();
    T GetByDelegate(Func<T?, bool>? func);

}

