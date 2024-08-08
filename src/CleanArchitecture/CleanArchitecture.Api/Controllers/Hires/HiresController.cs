using CleanArchitecture.Application.Hires.ReserveHire;
using CleanArchitecture.Applications.Hires.GetHire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Hires;

[ApiController]
[Route("api/hires")]
public class HiresController : ControllerBase
{
     private readonly ISender _sender;

    public HiresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHire(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetHireQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReserveHire(
        Guid id,
        ReserveHireRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new ReserveHireCommand(
            request.VehicleId,
            request.UserId,
            request.StartDate,
            request.EndDate
        );

        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetHire), new { id = result.Value});
    }

}