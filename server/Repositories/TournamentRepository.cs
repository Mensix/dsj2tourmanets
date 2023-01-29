using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Microsoft.EntityFrameworkCore;

namespace Dsj2TournamentsServer.Repositories;

public interface ITournamentRepository
{
    void Delete(string tournamentCode);
    Tournament Get(string tornamentCode);
    List<Tournament> GetCurrent();
    Tournament GetResults(string tournamentCode);
    void Post(Tournament tournament);
    void PostJump(Jump jump);
}

public class TournamentRepository : ITournamentRepository
{
    private readonly Dsj2TournamentsServerDbContext _context;

    public TournamentRepository(Dsj2TournamentsServerDbContext context)
    {
        _context = context;
    }

    public List<Tournament> GetCurrent()
    {
        return _context.Tournaments
            .Include(x => x.Hill)
            .Include(x => x.Settings)
            .Include(x => x.CreatedBy)
            .OrderByDescending(x => x.StartDate)
            .Where(x => ((DateTime)x.StartDate).ToUniversalTime() <= DateTime.UtcNow && x.EndDate.ToUniversalTime() >= DateTime.UtcNow)
            .ToList();
    }

    public Tournament Get(string tournamentCode)
    {
        return _context.Tournaments
            .Include(x => x.Hill)
            .Include(x => x.Settings)
            .Include(x => x.CreatedBy)
            .FirstOrDefault(x => x.Code == tournamentCode);
    }

    public Tournament GetResults(string tournamentCode)
    {
        var foundTournament = Get(tournamentCode);
        foundTournament.Jumps = _context.Jumps
            .Include(x => x.User)
            .Where(x => x.TournamentCode.ToLower() == tournamentCode.ToLower())
            .GroupBy(x => x.User.UserId)
            .Select(x => x.OrderByDescending(y => y.Points).First())
            .OrderByDescending(x => x.Points)
            .ToList();

        return foundTournament;
    }

    public void Post(Tournament tournament)
    {
        _context.Tournaments.Add(tournament);
        _context.SaveChanges();
    }

    public void PostJump(Jump jump)
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
}