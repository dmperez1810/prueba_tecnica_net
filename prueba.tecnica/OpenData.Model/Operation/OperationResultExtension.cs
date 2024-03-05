namespace OpenData.Model.Operation;

public static class OperationResultExtension
{
    public static OperationResult<T> Success<T>(T? value)
    {
        return new OperationResult<T>().AddResult(value);
    }

    public static OperationResult<T> Failure<T>(IEnumerable<Error> errors)
    {
        return new OperationResult<T>().AddErrors(errors);
    }

    public static async Task<OperationResult<W>> FlatMap<T, W>(this OperationResult<T> source, Func<T, Task<OperationResult<W>>> next)
    {
        if (source.HasErrors)
            return Failure<W>(source.Errors);

        return await next(source.Data!);
    }
}