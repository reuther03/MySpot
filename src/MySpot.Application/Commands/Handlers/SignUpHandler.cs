using MySpot.Application.Abstractions;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(SignUp command)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullname = new Fullname(command.Fullname);
        var role = new Role(command.Role);

        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new NotImplementedException();
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new NotImplementedException();
        }

        var securePassword = _passwordManager.Secure(password);
        var user = new User(userId, email, username, securePassword, fullname, role, _clock.Current());
        await _userRepository.AddAsync(user);
    }
}