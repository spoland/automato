using automato.Domain.Framework;

namespace automato.Domain.Jobs;

public class ScheduledJob : IEntity
{
    public static Result<ScheduledJob> Create(string name, Framework.Task task, CronExpression cronExpression)
    {
        var validationExceptions = new List<ValidationException>();

        if (string.IsNullOrWhiteSpace(name))
        {
            validationExceptions.Add(new ValidationException(
                propertyName: nameof(name),
                message: $"{nameof(name)} must not be null or whitespace."));
        }

        if (validationExceptions.Any())
        {
            return validationExceptions;
        }

        return new ScheduledJob(
            name: name,
            scheduledTaskId: task.Id,
            id: new Guid().ToString(),
            cronExpression: cronExpression);
    }

    public string Id { get; }

    public string Name { get; }

    public string CronExpression { get; }

    public string ScheduledTaskId { get; }

    private ScheduledJob(string id, string name, string scheduledTaskId, string cronExpression)
    {
        Id = id;
        Name = name;
        CronExpression = cronExpression;
        ScheduledTaskId = scheduledTaskId;
    }
}
