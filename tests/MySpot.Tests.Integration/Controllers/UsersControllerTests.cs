using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.Security;
using Shouldly;

namespace MySpot.Tests.Integration.Controllers;

public class UsersControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task post_users_should_return_201_created_status_code()
    {
        var testDatabase = new TestDatabase();
        await testDatabase.Context.Database.MigrateAsync();
        var command = new SignUp(Guid.Empty, "2test@test.com", "2test_user", "password", "2test test", "user");
        var response = await Client.PostAsJsonAsync("SignUp", command);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task post_sign_in_should_return_200_ok_status_code()
    {
        // Arrange
        const string password = "password";
        var passwordManager = new PasswordManager(new PasswordHasher<User>());

        var user = new User(Guid.NewGuid(), "2test@test.com", "2test_user", passwordManager.Secure(password), "2test test", Role.User(), DateTime.Now);
        await _userRepository.AddAsync(user);
        // await _testDatabase.Context.Database.MigrateAsync();
        // await _testDatabase.Context.Users.AddAsync(user);
        // await _testDatabase.Context.SaveChangesAsync();

        // Act
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("SignUp/sign-in", command);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

        // Assert
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task get_user_me_should_return_200_ok_status_code()
    {
        // Arrange
        const string password = "password";
        var passwordManager = new PasswordManager(new PasswordHasher<User>());

        var user = new User(Guid.NewGuid(), "test.email1@test.com", "test-username#1", passwordManager.Secure(password), "Test Username #1", Role.User(), DateTime.Now);
        await _testDatabase.Context.Database.MigrateAsync();
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();

        // Act
        Authorize(user.Id, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("SignUp/me");

        // Assert
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }

    private IUserRepository _userRepository;
    private readonly TestDatabase _testDatabase;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        _userRepository = new TestUserRepository();
        services.AddSingleton(_userRepository);
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}