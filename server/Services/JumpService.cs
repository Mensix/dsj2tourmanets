using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Repositories;

namespace Dsj2TournamentsServer.Services;

public interface IJumpService
{
    Jump Get(string replayCode);
    void Delete(string replayCode);
    bool AlreadyExists(string replayCode);
    bool AnyBetterThan(Jump jump);
    string GetTournamentCode(Jump jump);
}

public class JumpService : IJumpService
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IJumpRepository _jumpRepository;
    public JumpService(ITournamentRepository tournamentRepository, IJumpRepository jumpRepository)
    {
        _tournamentRepository = tournamentRepository;
        _jumpRepository = jumpRepository;
    }

    public Jump Get(string replayCode)
    {
        return _jumpRepository.Get(replayCode);
    }

    public void Delete(string replayCode)
    {
        _jumpRepository.Delete(replayCode);
    }

    public bool AlreadyExists(string replayCode)
    {
        return _jumpRepository.Get(replayCode) != null;
    }

    public bool AnyBetterThan(Jump jump)
    {
        return _jumpRepository.AnyBetterThan(jump);
    }

    public string GetTournamentCode(Jump jump)
    {
        var currentTournaments = _tournamentRepository.GetCurrent();
        if (currentTournaments.Count == 1)
        {
            return currentTournaments[0].Code;
        }

        var groupedCurrentTournaments = currentTournaments.GroupBy(x => x.Hill);
        if (groupedCurrentTournaments.All(x => x.Count() == 1))
        {
            return currentTournaments.Find(x => x.Hill.Name == jump.Hill.Name).Code;
        }
        else if (groupedCurrentTournaments.Any(x => x.Count() > 1))
        {
            return jump.Player.Split(" ").Last();
        }

        return null;
    }
}