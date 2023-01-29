using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsServer.Repositories;

public interface IJumpRepository
{
    Jump GetByReplayCode(string replayCode);
    bool AnyBetterThan(Jump jump);
}

public class JumpRepository : IJumpRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;
    public JumpRepository(Dsj2TournamentsServerDbContext context)
    {
        _context = context;
    }

    public Jump GetByReplayCode(string replayCode)
    {
        return _context.Jumps.FirstOrDefault(x => x.ReplayCode == replayCode);
    }

    public bool AnyBetterThan(Jump jump)
    {
        return _context.Jumps.Any(x => jump.User.Equals(x.User) && x.TournamentCode == jump.TournamentCode && jump.Points < x.Points);
    }
}
