namespace automato.Domain.Actions;

public class ActionName : ValueObject
{
    public static ActionName FromString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("An action name can not be empty.", nameof(value));
        }

        return new ActionName { Value = value };
    }

    public string Value { get; private set; } = string.Empty;

    public static ActionName Empty => new();

    public static implicit operator ActionName(string value) => FromString(value);
    public static implicit operator string(ActionName value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ActionName()
    {
        // Hide default constructor
    }
}
