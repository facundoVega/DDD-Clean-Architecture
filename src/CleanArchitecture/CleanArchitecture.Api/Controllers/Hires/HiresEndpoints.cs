using Asp.Versioning;
using Asp.Versioning.Builder;
using CleanArchitecture.Api.Utils;
using CleanArchitecture.Application.Hires.ReserveHire;
using CleanArchitecture.Applications.Hires.GetHire;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Permissions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Hires;

public static class HiresEndpoints
{
    public static IEndpointRouteBuilder MapHireEndpoints(
        this IEndpointRouteBuilder builder
    )
    {

        builder
            .MapGet("hires/{id}", GetHire)
            .RequireAuthorization(PermissionEnum.ReadUser.ToString())
            .WithName(nameof(GetHire));

        builder
            .MapPost("hires", ReserveHire)
            .RequireAuthorization(PermissionEnum.WriteUser.ToString());

        return builder;
    }


    public static async Task<IResult> GetHire(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken
    )
    {
        var query = new GetHireQuery(id);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
    }

    public static async Task<IResult> ReserveHire(
        ISender sender,
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

        var result = await sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.CreatedAtRoute(nameof(GetHire), new { id = result.Value});
    }

}