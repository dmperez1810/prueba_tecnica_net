namespace OpenData.Sync.Port;

using OpenData.Model.Operation;

public interface IApiRest
{
    Task<OperationResult<T>> GetAsync<T>(string path) where T : class;
}

