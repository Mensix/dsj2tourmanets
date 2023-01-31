using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Microsoft.EntityFrameworkCore;

namespace Dsj2TournamentsServer.Repositories;

public interface ITournamentRepository
{
    Tournament Get(string tornamentCode);
    List<Tournament> GetCurrent();
    List<Jump> GetJumps(string tournamentCode);
    void Post(Tournament tournament);
    void Post(Jump jump);
    void Delete(string tournamentCode);
    bool AnyCurrent();
}

public class TournamentRepository : ITournamentRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;

    public TournamentRepository(Dsj2TournamentsServerDbContext context)
    {
        _context = context;
    }

    public Tournament Get(string tournamentCode)
    {
        var foundTournament = _context.Tournaments
            .Include(x => x.Hill)
            .Include(x => x.Settings)
            .Include(x => x.CreatedBy)
            .FirstOrDefault(x => x.Code == tournamentCode);

        if (foundTournament != null)
        {
            if (foundTournament.Settings.LiveBoard || foundTournament.IsFinished)
            {
                foundTournament.Jumps = GetJumps(tournamentCode);
            }
        }

        return foundTournament;
    }

    public List<Tournament> GetCurrent()
    {
        var foundTournaments = _context.Tournaments
            .Include(x => x.Hill)
            .Include(x => x.Settings)
            .Include(x => x.CreatedBy)
            .OrderByDescending(x => x.StartDate)
            .Where(x => ((DateTime)x.StartDate).ToUniversalTime() <= DateTime.UtcNow && x.EndDate.ToUniversalTime() >= DateTime.UtcNow)
            .ToList();

        foreach (var tournament in foundTournaments)
        {
            if (tournament.Settings.LiveBoard || tournament.IsFinished)
            {
                tournament.Jumps = GetJumps(tournament.Code);
            }
        }

        return foundTournaments;
    }

    public List<Jump> GetJumps(string tournamentCode)
    {
        return _context.Jumps
            .Include(x => x.User)
            .OrderByDescending(x => x.Points)
            .Where(x => x.TournamentCode.ToLower() == tournamentCode.ToLower())
            .GroupBy(x => x.User.UserId)
            .Select(x => x.OrderByDescending(y => y.Points).First())
            .ToList();
    }

    public void Post(Tournament tournament)
    {
        _context.Tournaments.Add(tournament);
        _context.SaveChanges();
    }

    public void Post(Jump jump)
    {
        var foundTournament = Get(jump.TournamentCode);
        (foundTournament.Jumps ??= new()).Add(jump);
        _context.SaveChanges();
    }

    public void Delete(string tournamentCode)
    {
        _context.Jumps.RemoveRange(_context.Jumps.Where(x => x.TournamentCode.ToLower() == tournamentCode.ToLower()));
        _context.Tournaments.Remove(_context.Tournaments.FirstOrDefault(x => x.Code.ToLower() == tournamentCode.ToLower()));
        _context.SaveChanges();
    }

    public bool AnyCurrent()
    {
        return _context.Tournaments
            .Any(x => ((DateTime)x.StartDate).ToUniversalTime() <= DateTime.UtcNow && x.EndDate.ToUniversalTime() >= DateTime.UtcNow);
    }
}