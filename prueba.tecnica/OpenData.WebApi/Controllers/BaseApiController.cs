namespace OpenData.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using OpenData.Model.Operation;

public abstract class BaseApiController : ControllerBase
{
    public ActionResult<T> GetResponse<T>(OperationResult<T> operationResult)
    {
        if (operationResult.HasExceptions)
            return StatusCode(500, operationResult.Errors.First().Message);

        if (operationResult.HasErrors)
            return BadRequest(operationResult.Errors);

        if (operationResult is null)
            return NoContent();

        return Ok(operationResult.Data);
    }
}