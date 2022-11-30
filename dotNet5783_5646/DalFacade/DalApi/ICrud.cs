using DO;
namespace DalApi;


public interface ICrud<T>
{
    int Add(T var);
    void Delete(int id);
    T Get(int id);
    void Update(T var);
    IEnumerable<T> GetList();
    int Leangth();

}

