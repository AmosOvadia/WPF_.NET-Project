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