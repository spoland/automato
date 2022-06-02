namespace automato.Framework;

public interface IEntity<out TId>
{
    public TId Id { get; }
}
