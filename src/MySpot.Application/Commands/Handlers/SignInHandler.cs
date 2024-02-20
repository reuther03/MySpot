using MySpot.Application.Abstractions;
using MySpot.Application.Security;
using MySpot.Core.Repositories;

namespace MySpot.Application.Commands.Handlers;

public class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IAuthenticator _authenticator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;


    public SignInHandler(IAuthenticator authenticator, IUserRepository userRepository, IPasswordManager passwordManager, ITokenStorage tokenStorage)
    {
        _authenticator = authenticator;
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }

    public async Task HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        if(!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new Exception("Invalid password");
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role);
        _tokenStorage.Set(jwt);
    }
}