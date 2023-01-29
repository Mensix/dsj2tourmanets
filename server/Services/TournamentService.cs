using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Dsj2TournamentsServer.Repositories;

namespace Dsj2TournamentsServer.Services;

public interface ITournamentService
{
    void Delete(string tournamentCode);
    Tournament Get(string tournamentCode);
    List<Tournament> GetCurrent();
    Tournament GetResults(string tournamentCode);
    void Post(Tournament tournament);
}

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public List<Tournament> GetCurrent()
    {
        return _tournamentRepository.GetCurrent();
    }

    public Tournament Get(string tournamentCode)
    {
        return _tournamentRepository.Get(tournamentCode);
    }

    public Tournament GetResults(string tournamentCode)
    {
        var results = _tournamentRepository.GetResults(tournamentCode);
        var currentPlace = 1;

        if (results.Jumps.Count > 0)
        {
            results.Jumps[0].Place = currentPlace;
            for (int i = 1; i < results.Jumps.Count; i++)
            {
                if (results.Jumps[i].Points < results.Jumps[i - 1].Points)
                {
                    currentPlace++;
                }

                results.Jumps[i].Place = currentPlace;
            }
        }

        return results;
    }

    public void Post(Tournament tournament)
    {
        _tournamentRepository.Post(tournament);
    }

    public void Delete(string tournamentCode)
    {
        _tournamentRepository.Delete(tournamentCode);
    }
}