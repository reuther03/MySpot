using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;
using MySpot.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SignUpController : ControllerBase
{
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenStorage _tokenStorage;

    public SignUpController(IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler, ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetUser, UserDto> getUserHandler,
        IAuthenticator authenticator, ITokenStorage tokenStorage, ICommandHandler<SignIn> signInHandler)
    {
        _getUsersHandler = getUsersHandler;
        _signUpHandler = signUpHandler;
        _getUserHandler = getUserHandler;
        _authenticator = authenticator;
        _tokenStorage = tokenStorage;
        _signInHandler = signInHandler;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        var userIdValue = User.Claims.FirstOrDefault(c => c.Type == ClaimConsts.UserId)?.Value;
        if (string.IsNullOrEmpty(userIdValue))
        {
            return NotFound();
        }

        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = Guid.Parse(userIdValue) });

        return user;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("all")]
    [SwaggerOperation("Get all users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));

    [HttpPost]
    public async Task<ActionResult<JwtDto>> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { command.UserId }, null);
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }
}