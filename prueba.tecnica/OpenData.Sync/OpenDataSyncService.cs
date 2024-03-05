namespace OpenData.Sync;

using OpenData.Model.Messages;
using OpenData.Model.OpenData.Responses;
using OpenData.Model.Operation;
using OpenData.Persistence.Context;
using OpenData.Persistence.Entities;
using OpenData.Sync.Port;
using System.Linq;

public class OpenDataSyncService: IOpenDataSyncService
{
    private readonly IApiRest _apiRest;
    private readonly OpenDataContext _context;

    private const string _path = "/EXP01/BalanceResponsibleParties";

	public OpenDataSyncService(IApiRest apiRest, OpenDataContext context)
	{
        _apiRest = apiRest;
        _context = context;

    }

    public async Task<OperationResult<IEnumerable<PartMarketDto>>> SyncDataAsync()
    {
        var partMarketsDtos = await _apiRest.GetAsync<IEnumerable<PartMarketDto>>(_path);
        return await partMarketsDtos.FlatMap(SyncDataInContext);
    }

    public OperationResult<PartMarketDto> GetById(string key)
    {
        var partMarket = _context.PartMarkets!.FirstOrDefault(pm => pm.BrpCode == key);

        if(partMarket == null)
        {
            var error = new Error(ValidationCodeError.CODE_KEY_NOT_FOUND, ValidationMessages.KEY_NOT_FOUND);
            return OperationResultExtension.Failure<PartMarketDto>(new List<Error> { error });
        }

        var partMarketDto = new PartMarketDto(partMarket.BrpCode);

        partMarketDto.WithBrpName(partMarket.BrpName)
                     .WithBusinesId(partMarket.BusinessId)
                     .WithCodingScheme(partMarket.CodingScheme)
                     .WithCountry(partMarket.Country)
                     .WithValidityEnd(partMarket.ValidityEnd)
                     .WithValidityStart(partMarket.ValidityStart);

        return OperationResultExtension.Success(partMarketDto);
    }

    private async Task<OperationResult<IEnumerable<PartMarketDto>>> SyncDataInContext(IEnumerable<PartMarketDto> partMarketDtos)
    {
        var partMarkets = _context.PartMarkets!.ToList();

        var partMarketToRemove = partMarkets.Where(pm => partMarketDtos.Any(pmdto => pmdto.BrpCode == pm.BrpCode));

        foreach (var partMarketDto in partMarketDtos)
        {
            var partMarketToSave = partMarkets.FirstOrDefault(pm => pm.BrpCode == partMarketDto.BrpCode) ?? new PartMarket(partMarketDto.BrpCode);

            partMarketToSave.WithBrpName(partMarketDto.BrpName)
                            .WithBusinesId(partMarketDto.BusinessId)
                            .WithCodingScheme(partMarketDto.CodingScheme)
                            .WithCountry(partMarketDto.Country)
                            .WithValidityEnd(partMarketDto.ValidityEnd)
                            .WithValidityStart(partMarketDto.ValidityStart);

            if (partMarketToSave.Id == Guid.Empty) _context.Add(partMarketToSave);
            else _context.Update(partMarketToSave);
        }

        await _context.SaveChangesAsync();

        return OperationResultExtension.Success(partMarketDtos);
    }
}