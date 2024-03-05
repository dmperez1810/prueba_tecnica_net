namespace OpenData.Link;

using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using OpenData.Model.Messages;
using OpenData.Model.Operation;
using OpenData.Sync.Port;

public class ApiRest: IApiRest
{
    private readonly HttpClient _client;
    private readonly ILogger<ApiRest> _logger;

    public ApiRest(IHttpClientFactory httpClientFactory, ILogger<ApiRest> logger)
	{
		_client = httpClientFactory.CreateClient("OpenData");
        _logger = logger;

    }

	public async Task<OperationResult<T>> GetAsync<T>(string path) where T:class
	{
		try
		{
            var response = await _client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                return EvaluateError<T>(contentError);
            }

            var content = await response.Content.ReadFromJsonAsync<T>();

            return OperationResultExtension.Success(content);
        }
        catch(Exception ex)
        {
            return EvaluateError<T>(ex.Message, ex);
        }
	}

    private OperationResult<T> EvaluateError<T>(string message, Exception? ex = default)
    {
        _logger.LogError("{message} {ex}", message, ex?.StackTrace);

        var error = new Error(ValidationCodeError.CODE_RETRIEVING, ValidationMessages.RETRIEVING, ex);
        return OperationResultExtension.Failure<T>(new Error[] { error });
    }
}