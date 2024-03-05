namespace OpenData.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using OpenData.Model.OpenData.Responses;
using OpenData.Sync.Port;

[ApiController]
[Route("[controller]")]
public class OpenDataController : BaseApiController
{
	private readonly IOpenDataSyncService _openDataService;

	public OpenDataController(IOpenDataSyncService openDataService)
	{
		_openDataService = openDataService;

    }

	[HttpPost("Synchronize")]
	public async Task<ActionResult<IEnumerable<PartMarketDto>>> Sync()
	{
		var response = await _openDataService.SyncDataAsync();
		return GetResponse(response);
	}

	[HttpGet("Search/{key}")]
	public ActionResult<PartMarketDto> GetByKey(string key)
	{
		return GetResponse(_openDataService.GetById(key));
	}
}