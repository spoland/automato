using System.Diagnostics.CodeAnalysis;
using automato.Domain.Framework;

namespace automato.Domain.Jobs;

public record CronExpression : IParsable<CronExpression>
{
    public static Result<CronExpression> Create(
        string minute,
        string hour,
        string dayOfMonth,
        string month,
        string dayOfWeek)
    {
        List<ValidationException> exceptions = new();

        if (minute != "*" && (!int.TryParse(minute, out var minuteInt) || minuteInt < 0 || minuteInt > 59))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(minute),
                message: $"{nameof(minute)} must be either '*' or an integer value between 0 and 59."));
        }

        if (hour != "*" && (!int.TryParse(hour, out var h) || h < 0 || h > 23))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(hour),
                message: $"{nameof(hour)} must be either '*' or an integer between 0 and 23."));
        }

        if (dayOfMonth != "*" && (!int.TryParse(dayOfMonth, out var dom) || dom < 1 || dom > 31))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(dayOfMonth),
                message: $"{nameof(dayOfMonth)} must be either '*' or an integer between 1 and 31."));
        }

        if (month != "*" && (!int.TryParse(month, out var m) || m < 1 || m > 12))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(month),
                message: $"{nameof(month)} must be either '*' or an integer between 1 and 12."));
        }

        if (dayOfWeek != "*" && (!int.TryParse(dayOfWeek, out var dow) || dow < 0 || dow > 6))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(dayOfWeek),
                message: $"{nameof(dayOfWeek)} must be either '*' or an integer between 0 and 6."));
        }

        if (exceptions.Any())
        {
            return exceptions;
        }

        var stringValue = $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}";

        return new CronExpression(minute, hour, dayOfMonth, month, dayOfWeek, stringValue);
    }

    public string Minute { get; }

    public string Hour { get; }

    public string DayOfMonth { get; }

    public string Month { get; }

    public string DayOfWeek { get; }

    public string Value { get; }

    public override string ToString() => Value;

    public static bool TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CronExpression result)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        var parts = s.Split(' ');

        if (parts.Length != 5)
        {
            return false;
        }

        var attempt = Create(
            minute: parts[0],
            hour: parts[1],
            dayOfMonth: parts[2],
            month: parts[3],
            dayOfWeek: parts[4]);

        if (attempt.IsSuccess && attempt.Value != null)
        {
            result = attempt.Value;
            return true;
        }

        return false;
    }

    public static CronExpression Parse(string s, IFormatProvider? provider)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException($"{nameof(s)} must not be null or whitespace.", nameof(s));
        }

        var parts = s.Split(' ');

        if (parts.Length != 5)
        {
            throw new FormatException($"{nameof(s)} must be a string with 5 parts separated by spaces.");
        }

        var attempt = Create(
            minute: parts[0],
            hour: parts[1],
            dayOfMonth: parts[2],
            month: parts[3],
            dayOfWeek: parts[4]);

        if (!attempt.IsSuccess)
        {
            throw new FormatException($"{nameof(s)} is not a supported cron expression.");
        }

        return attempt.Value!;
    }

    public static implicit operator string(CronExpression cronExpression) => cronExpression.Value;

    private CronExpression(string minute, string hour, string dayOfMonth, string month, string dayOfWeek, string value)
    {
        Minute = minute;
        Hour = hour;
        DayOfMonth = dayOfMonth;
        Month = month;
        DayOfWeek = dayOfWeek;
        Value = value;
    }
}
