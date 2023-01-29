using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsServer.Repositories;

public interface IUserRepository
{
    User Get(ulong userId);
}

public class UserRepository : IUserRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;
    public UserRepository(Dsj2TournamentsServerDbContext context)
    {
        _context = context;
    }

    public User Get(ulong userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId);
    }
}