namespace automato.Domain.Framework;

public class ValidationException : Exception
{
    public string PropertyName { get; }

    public ValidationException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}
