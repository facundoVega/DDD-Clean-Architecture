using System.Net;
using Asp.Versioning;
using CleanArchitecture.Api.Utils;
using CleanArchitecture.Application.Vehicles.GetVehiclesByPagination;
using CleanArchitecture.Applications.Vehicles.SearchVehicles;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Vehicles;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly  ISender _sender;

    public VehicleController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(PermissionEnum.ReadUser)]
    [HttpGet("search")]
    public async Task<IActionResult> SearchVehicles(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiclesQuery(startDate, endDate);
        var results = await _sender.Send(query, cancellationToken);
        return Ok(results.Value);
    }

    [AllowAnonymous]
    [HttpGet("getPagination", Name = "PaginationVehicles")]
    [ProducesResponseType(typeof(PaginationResult<Vehicle, VehicleId>), 
        (int)HttpStatusCode.OK)]

    public async Task<ActionResult<PaginationResult<Vehicle, VehicleId>>> GetPaginationVehicle(
        [FromQuery] GetVehiclesByPaginationQuery request
    )
    {
        var results = await _sender.Send(request);
        return Ok(results);
    }

}