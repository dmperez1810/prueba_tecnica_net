using OpenData.Model.OpenData.Responses;
using OpenData.Model.Operation;

namespace OpenData.Sync.Port;

public interface IOpenDataSyncService
{
    Task<OperationResult<IEnumerable<PartMarketDto>>> SyncDataAsync();
    OperationResult<PartMarketDto> GetById(string key);
}

