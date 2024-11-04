using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Hires.ReserveHire;
using CleanArchitecture.Application.UnitTests.Users;
using CleanArchitecture.Application.UnitTests.Vehicles;
using CleanArchitecture.Applications.Abstractions.Clock;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Hires;

public  class ReserveHireTests 
{
    private readonly ReserveHireCommandHandler _handlerMock;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IVehicleRepository _vehicleRepositoryMock;
    private readonly IHireRepository _hireRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly DateTime UtcNow = DateTime.UtcNow;
    private readonly ReserveHireCommand command = new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        new DateOnly(2024, 1, 1),
        new DateOnly(2025, 1, 1)
    );

    public ReserveHireTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _vehicleRepositoryMock = Substitute.For<IVehicleRepository>();
        _hireRepositoryMock = Substitute.For<IHireRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.currentTime.Returns(UtcNow);

        _handlerMock = new ReserveHireCommandHandler(
            _userRepositoryMock,
            _vehicleRepositoryMock,
            _hireRepositoryMock,
            new PriceService(),
            _unitOfWorkMock,
            dateTimeProviderMock
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
    {
        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId), 
            Arg.Any<CancellationToken>()
        )
        .Returns((Domain.Users.User?)null);
    
    
        var result = await  _handlerMock.Handle(command, default );
    
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_Returnfailure_WhenVehicleIsNull()
    {
        //Arrange
        var user = UserMock.Create();

        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
             Arg.Any<CancellationToken>()
        ).Returns(user);

        _vehicleRepositoryMock.GetByIdAsync(
            new VehicleId(command.VehicleId),
            Arg.Any<CancellationToken>()
        ).Returns((Vehicle?)null);
        

        //Act
        var results = await _handlerMock.Handle(command, default);

        //Assert
        results.Error.Should().Be(VehicleErrors.NotFound);

    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenVehicleIsHired()
    {
        //Arrange
        var user = UserMock.Create();
        var vehicle = VehicleMock.Create();
        var duration = DateRange.Create(command.StartDate, command.EndDate);

        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
            Arg.Any<CancellationToken>()
        ).Returns(user);

        _vehicleRepositoryMock.GetByIdAsync(
            new VehicleId(command.VehicleId),
            Arg.Any<CancellationToken>()
        ).Returns(vehicle);

        _hireRepositoryMock.IsOverlappingAsync(
            vehicle,
            duration,
            Arg.Any<CancellationToken>()
        ).Returns(true);


        //Act
        var results = await _handlerMock.Handle(command, default);

        //Asserts
        results.Error.Should().Be(HireErrors.Overlap);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOFWorkThrows()
    {
        //Arrange
        var user = UserMock.Create();
        var vehicle = VehicleMock.Create(); 
        var duration = DateRange.Create(command.StartDate, command.EndDate);

        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
            Arg.Any<CancellationToken>()
        ).Returns(user);

        _vehicleRepositoryMock.GetByIdAsync(
            new VehicleId(command.VehicleId),
            Arg.Any<CancellationToken>()
        ).Returns(vehicle);

        _hireRepositoryMock.IsOverlappingAsync(
            vehicle,
            duration,
            Arg.Any<CancellationToken>()
        ).Returns(false);

        _unitOfWorkMock.SaveChangesAsync().ThrowsAsync(
            new ConcurrencyException("Concurrency", new Exception())
        );

        //Act
        var result = await _handlerMock.Handle(command, default);

        //Assert
        result.Error.Should().Be(HireErrors.Overlap);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenHireIsReserved()
    {
         //Arrange
        var user = UserMock.Create();
        var vehicle = VehicleMock.Create(); 
        var duration = DateRange.Create(command.StartDate, command.EndDate);

                _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
            Arg.Any<CancellationToken>()
        ).Returns(user);

        _vehicleRepositoryMock.GetByIdAsync(
            new VehicleId(command.VehicleId),
            Arg.Any<CancellationToken>()
        ).Returns(vehicle);

        _hireRepositoryMock.IsOverlappingAsync(
            vehicle,
            duration,
            Arg.Any<CancellationToken>()
        ).Returns(false);

        //Act
        var result = await _handlerMock.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}