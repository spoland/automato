using automato.Domain.Connections;

namespace automato.Domain.Actions;

/// <summary>
/// Class ConnectionId.
/// Implements the <see cref="ValueObject" />
/// </summary>
/// <seealso cref="ValueObject" />
public class ActionId : ValueObject
{
    /// <summary>
    /// Creates a new <see cref="ConnectionId"/> instance.
    /// </summary>
    /// <returns>ConnectionId.</returns>
    public static ActionId New() => new() { Value = Guid.NewGuid() };

    public static ActionId Empty => new() { Value = Guid.Empty };

    public static ActionId FromGuid(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new ArgumentException("An action ID cannot be empty.", nameof(guid));
        }

        return new() { Value = guid };
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public Guid Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="ConnectionId"/> class from being created.
    /// </summary>
    private ActionId()
    {
        // Hide default constructor
    }
}
