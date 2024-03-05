namespace OpenData.Model.Operation;

public class OperationResult<TData>
{
    public TData? Data { get; private set; }
    public IEnumerable<Error> Errors { get; private set; }
    public bool HasErrors => Errors.Any();
    public bool HasExceptions => Errors.Any(x => x.Exception != null);

    public OperationResult(IEnumerable<Error>? error = default)
    {
        Errors = error ?? new HashSet<Error>();
        Data = default;
    }

    public OperationResult<TData> AddResult(TData? data)
    {
        Data = data;
        return this;
    }

    public OperationResult<TData> AddErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Errors = Errors.Append(error);
        }

        return this;
    }
}
