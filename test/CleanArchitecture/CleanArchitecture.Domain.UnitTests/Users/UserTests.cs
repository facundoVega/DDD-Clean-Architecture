using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        //Arrange 

        //Act
        var user  = User.Create(
            UserMock.Name, 
            UserMock.LastName, 
            UserMock.Email, 
            UserMock.Password
        );

        //Assert
        user.Name.Should().Be(UserMock.Name);
        user.LastName.Should().Be(UserMock.LastName);
        user.Email.Should().Be(UserMock.Email);
        user.PasswordHash.Should().Be(UserMock.Password);
    }

    [Fact]
    public void Create_Should_RaiseUserCreateDomainEvent()
    {
        var user  = User.Create(
            UserMock.Name, 
            UserMock.LastName, 
            UserMock.Email, 
            UserMock.Password
        );

        var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        domainEvent!.UserID.Should().Be(user.Id);

    }

    [Fact]
    public void Create_Should_AddRegisterRoleToUser()
    {
        var user  = User.Create(
            UserMock.Name, 
            UserMock.LastName, 
            UserMock.Email, 
            UserMock.Password
        );

        user.Roles.Should().Contain(Role.Client);
    }
}