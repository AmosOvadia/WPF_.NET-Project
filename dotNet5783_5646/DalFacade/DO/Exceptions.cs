using System.Security.Principal;

namespace DO;

// IdNotExistException
public class TheIdentityCardDoesNotExistInTheDatabase : Exception
{
    public TheIdentityCardDoesNotExistInTheDatabase(string msg) : base(msg) { }
}



//IdAlreadyExistException
public class TheIDAlreadyExistsInTheDatabase : Exception
{
    public TheIDAlreadyExistsInTheDatabase(string msg) : base(msg) { }
}


[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
