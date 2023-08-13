using automato.Domain.Jobs;

namespace automato.Domain.Unit.Tests.Jobs;

public class ScheduledJobTests
{
    [Fact]
    public void Create_WhenValid_ShouldCreateInstance()
    {
        // Arrange
        var task = new TestTask(id: "1", name: "Test Task");
        var name = "Test Job";
        var cronExpression = CronExpression.Create(
            minute: "*",
            hour: "*",
            dayOfMonth: "*",
            month: "*",
            dayOfWeek: "*").Value!;

        // Act
        var result = ScheduledJob.Create(
            name: name,
            task: task,
            cronExpression: cronExpression);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value?.Name.Should().Be(name);
        result.Value?.Id.Should().NotBeNullOrEmpty();
        result.Value?.ScheduledTaskId.Should().Be(task.Id);
        result.Value?.CronExpression.Should().Be(cronExpression);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_WhenInvalid_ShouldCreateInstance(string name)
    {
        // Arrange
        var task = new TestTask(id: "1", name: "Test Task");
        var cronExpression = CronExpression.Create(
            minute: "*",
            hour: "*",
            dayOfMonth: "*",
            month: "*",
            dayOfWeek: "*").Value!;

        // Act
        var result = ScheduledJob.Create(
            name: name,
            task: task,
            cronExpression: cronExpression);

        // Assert
        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Exceptions.Should().HaveCount(1);
    }

    private class TestTask : Framework.AutomatoTask
    {
        public TestTask(string id, string name) : base(id, name)
        {
        }
    }
}
