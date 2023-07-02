using automato.Domain.Jobs;

namespace automato.Domain.Unit.Tests.Jobs;

public class CronExpressionTests
{
    [Theory]
    [InlineData("*", "*", "*", "*", "*")]
    [InlineData("0", "22", "2", "12", "6")]
    public void Create_WhenValid_ShouldCreateSuccessfully(
        string minute,
        string hour,
        string dayOfMonth,
        string month,
        string dayOfWeek)
    {
        // Act
        var result = CronExpression.Create(
            minute: minute,
            hour: hour,
            dayOfMonth: dayOfMonth,
            month: month,
            dayOfWeek: dayOfWeek);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value?.Value.Should().Be($"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}");
    }

    [Theory]
    [InlineData("-1", "-1", "0", "0", "-1")]
    [InlineData("60", "24", "32", "13", "7")]
    public void Create_WhenInvalid_ShouldFailToCreate(
    string minute,
    string hour,
    string dayOfMonth,
    string month,
    string dayOfWeek)
    {
        // Act
        var result = CronExpression.Create(
            minute: minute,
            hour: hour,
            dayOfMonth: dayOfMonth,
            month: month,
            dayOfWeek: dayOfWeek);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exceptions.Should().HaveCount(5);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("1 1 1 1")]
    [InlineData("-1 -1 0 0 -1")]
    [InlineData("60 24 32 13 7")]
    public void TryParse_WhenInvalid_ShouldReturnFalse(string stringExpression)
    {
        // Act
        var success = CronExpression.TryParse(stringExpression, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_WhenValid_ShouldReturnTrue()
    {
        // Act
        var success = CronExpression.TryParse("* * * 1 *", out var result);

        // Assert
        success.Should().BeTrue();
        result?.Value.Should().Be("* * * 1 *");
    }

}
