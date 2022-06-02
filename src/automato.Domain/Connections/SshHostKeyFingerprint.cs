namespace automato.Domain.Connections;

public class SshHostKeyFingerprint : ValueObject
{
    public static SshHostKeyFingerprint FromString(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return new SshHostKeyFingerprint { Value = value };
        }

        throw new ArgumentException($"An SSH Host Key Fingerprint value cannot be empty.", nameof(value));
    }

    public string Value { get; private set; } = string.Empty;

    public static SshHostKeyFingerprint Empty => new();

    public static implicit operator SshHostKeyFingerprint(string value) => FromString(value);
    public static implicit operator string(SshHostKeyFingerprint value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private SshHostKeyFingerprint()
    {
        // Hide default constructor
    }
}
