namespace MovieApp.Business.Exceptions.GenreExceptions;

public class GenreAlreadyExistException : Exception
{
    public int StatusCode { get; set; }
    public string PropertyName { get; set; }
    public GenreAlreadyExistException()
    {
        
    }
    public GenreAlreadyExistException(string? message) : base(message) 
    {

    }
    public GenreAlreadyExistException(int statusCode,string propertyName,string? message) : base(message)
    {
        StatusCode = statusCode; 
        PropertyName = propertyName;

    }
}
