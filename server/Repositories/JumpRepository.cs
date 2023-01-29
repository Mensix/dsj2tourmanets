using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsServer.Repositories;

public interface IJumpRepository
{
    Jump Get(string replayCode);
    void Post(Jump jump);
    void Delete(string replayCode);
    bool AnyBetterThan(Jump jump);
}

public class JumpRepository : IJumpRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;
    private readonly ITournamentRepository _tournamentRepository;
    public JumpRepository(Dsj2TournamentsServerDbContext context, ITournamentRepository tournamentRepository)
    {
        _context = context;
        _tournamentRepository = tournamentRepository;
    }

    public Jump Get(string replayCode)
    {
        return _context.Jumps.FirstOrDefault(x => x.ReplayCode == replayCode);
    }

    public void Post(Jump jump)
    {
        var foundTournament = _tournamentRepository.Get(jump.TournamentCode);
        (foundTournament.Jumps ??= new()).Add(jump);
        _context.SaveChanges();
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
