using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CleanArchitecture.Api.FunctionalTests.Infrastructure;
using CleanArchitecture.Application.Users.GetUserSessions;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Users;

public class GetUserSessionTests : BaseFunctionalTest
{
    public GetUserSessionTests(FunctionalTestsWebAppFactory factory) : base(factory)
    {
    }


    [Fact]
    public async Task Get_ShouldReturnUnauthorized_WhenTokenIsMissing()
    {
        var response = await HttpClient.GetAsync("api/v1/users/me");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Get_ShouldReturnUser_WhenTokenExists()
    {
        var token =  await GetToken(); 
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            token
        );

        var user = await HttpClient.GetFromJsonAsync<UserResponse>("api/v1/users/me");

        user.Should().NotBeNull();

    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenUserExists()
    {
        var request = new LoginUserRequest(
                UserData.RegisterUserRequestTest.Email,
                UserData.RegisterUserRequestTest.Password
            );

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Register_ShouldReturnOk_WhenRequestIsValid()
    {
        var request = new RegisterUserRequest(
            "test@test.com",
            "test",
            "test",
            "Test123#"
        );

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}