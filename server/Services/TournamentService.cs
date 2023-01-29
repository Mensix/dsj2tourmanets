using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Dsj2TournamentsServer.Repositories;

namespace Dsj2TournamentsServer.Services;

public interface ITournamentService
{
    void Delete(string tournamentCode);
    Tournament Get(string tournamentCode);
    List<Tournament> GetCurrent();
    void Post(Jump jump);
    void Post(Tournament tournament);
    List<Jump> GetPlaces(List<Jump> jumps);
}

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public Tournament Get(string tournamentCode)
    {
        var tournament = _tournamentRepository.Get(tournamentCode);
        tournament.Jumps = GetPlaces(tournament.Jumps);

        return tournament;
    }

    public List<Tournament> GetCurrent()
    {
        var tournaments = _tournamentRepository.GetCurrent();

        foreach (var tournament in tournaments)
        {
            if (tournament.IsFinished || tournament.Settings.LiveBoard)
            {
                tournament.Jumps = GetPlaces(tournament.Jumps);
            }
        }

        return tournaments;
    }

    public void Post(Jump jump)
    {
        _tournamentRepository.Post(jump);
    }


    public void Post(Tournament tournament)
    {
        _tournamentRepository.Post(tournament);
    }

    public void Delete(string tournamentCode)
    {
        _tournamentRepository.Delete(tournamentCode);
    }

    public List<Jump> GetPlaces(List<Jump> jumps)
    {
        var currentPlace = 1;

        if (jumps.Count > 0)
        {
            jumps[0].Place = currentPlace;
            for (int i = 1; i < jumps.Count; i++)
            {
                if (jumps[i].Points < jumps[i - 1].Points)
                {
                    currentPlace++;
                }

                jumps[i].Place = currentPlace;
            }
        }

        return jumps;
    }
}