using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Repositories;

namespace Dsj2TournamentsServer.Services;

public interface IUserService
{
    User Get(ulong userId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Get(ulong userId)
    {
        return _userRepository.Get(userId);
    }
}