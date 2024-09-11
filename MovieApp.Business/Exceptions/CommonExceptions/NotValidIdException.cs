namespace MovieApp.Business.Exceptions.CommonExceptions;

public class NotValidIdException : Exception
{
    public int StatusCode { get; set; }
    public NotValidIdException()
    {

    }
    public NotValidIdException(string? message) : base(message)
    {

    }

    public NotValidIdException(int statusCode,string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}
