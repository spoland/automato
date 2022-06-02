namespace automato.Domain.Actions;

public abstract class AutomaterAction : IEntity<ActionId>
{
    protected AutomaterAction(ActionId id, ActionName name)
    {
        Id = id;
        Name = name;
    }

    protected AutomaterAction()
    {
    }

    public ActionId Id { get; private set; } = ActionId.Empty;

    public ActionName Name { get; private set; } = ActionName.Empty;
}
