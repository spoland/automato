namespace automato.Domain.Framework;

public abstract class AutomatoTask : ITask
{
    protected AutomatoTask(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"{nameof(id)} must not be null or whitespace.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} must not be null or whitespace.", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public string Id { get; }

    public string Name { get; }
}
