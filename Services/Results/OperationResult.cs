namespace TimeFlow.Services.Results;

public sealed record OperationResult(bool Succeeded, string? Error)
{
    public static OperationResult Success() => new(true, null);
    public static OperationResult Failure(string error) => new(false, error);
}