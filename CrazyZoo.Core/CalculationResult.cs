namespace CrazyZoo.Core;

public class CalculationResult
{
    public string Name { get; }
    public string Status { get; }
    public string Message { get; }
    public long DurationMs { get; }

    public CalculationResult(string name, string status, string message, long durationMs)
    {
        Name = name;
        Status = status;
        Message = message;
        DurationMs = durationMs;
    }

    public override string ToString()
    {
        return $"{Name} | {Status} | {Message} | {DurationMs} ms";
    }
}

