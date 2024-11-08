using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Applications.Abstractions.Clock;
using CleanArchitecture.Application.Exceptions;
using System.Net.Http.Headers;

namespace CleanArchitecture.Application.Hires.ReserveHire;

internal sealed class ReserveHireCommandHandler :
    ICommandHandler<ReserveHireCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IHireRepository _hireRepository;
    private readonly PriceService _priceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveHireCommandHandler(
        IUserRepository userRepository, 
        IVehicleRepository vehicleRepository, 
        IHireRepository hireRepository, 
        PriceService priceService, 
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
        )
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _hireRepository = hireRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        ReserveHireCommand request, 
        CancellationToken cancellationToken
        )
    {
        var userId = new UserId(request.UserId);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if(user is null) 
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehicleId = new VehicleId(request.VehicleId);
        var  vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, cancellationToken);

        if(vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if(await _hireRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
        {
            return Result.Failure<Guid>(HireErrors.Overlap);
        }

        try
        {
            var hire = Hire.Reserve(
                vehicle,
                user.Id!,
                duration,
                _dateTimeProvider.currentTime,
                _priceService   
            );

            _hireRepository.Add(hire);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return hire.Id!.Value;
        }
        catch(ConcurrencyException )
        {
            return Result.Failure<Guid>(HireErrors.Overlap);
        }
    }
}

