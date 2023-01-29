using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsServer.Repositories;

public interface IJumpRepository
{
    Jump Get(string replayCode);
    void Delete(string replayCode);
    bool AnyBetterThan(Jump jump);
}

public class JumpRepository : IJumpRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;
    public JumpRepository(Dsj2TournamentsServerDbContext context)
    {
        _context = context;
    }

    public Jump Get(string replayCode)
    {
        return _context.Jumps.FirstOrDefault(x => x.ReplayCode == replayCode);
    }

    public void Delete(string replayCode)
    {
        _context.Jumps.RemoveRange(_context.Jumps.Where(x => x.ReplayCode.ToLower() == replayCode.ToLower()));
        _context.SaveChanges();
    }

    public bool AnyBetterThan(Jump jump)
    {
        return _context.Jumps.Any(x => jump.User.Equals(x.User) && x.TournamentCode == jump.TournamentCode && jump.Points < x.Points);
    }
}
