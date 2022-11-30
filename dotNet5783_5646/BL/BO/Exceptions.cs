namespace BO;



// In case the id is less than zero -> error
public class TheIdDoesNotExistInTheDatabase : Exception
{
    public TheIdDoesNotExistInTheDatabase(string msg) : base(msg) { }
}



//In the case where we want to add, but such a member already exists -> error
public class TheIDAlreadyExistsInTheDatabase : Exception
{
    public TheIDAlreadyExistsInTheDatabase(string msg) : base(msg) { }
}





//VariableIsSmallerThanZeroExeption
public class TheVariableIsLessThanTheNumberZero : Exception
{
    public TheVariableIsLessThanTheNumberZero(string msg) : base(msg) { }
}


public class VariableIsNull : Exception
{
    public VariableIsNull(string msg) : base(msg) { }
}

public class OutOfStock : Exception
{
    public OutOfStock(string msg) : base(msg) { }
}




    public class InputError : Exception
{
    public InputError(string msg) : base(msg) { }
}

public class IsEmpty : Exception
{
    public IsEmpty(string msg) : base(msg) { }
}