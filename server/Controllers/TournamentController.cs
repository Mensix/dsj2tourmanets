using System.Text.Json;
using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Dsj2TournamentsServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsj2TournamentsServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TournamentController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ITournamentService _tournamentService;
    public TournamentController(ILogger<TournamentController> logger, ITournamentService tournamentService)
    {
        _logger = logger;
        _tournamentService = tournamentService;
    }

    [HttpGet]
    [Route("")]
    [Route("{code}")]
    public IActionResult Get(string code, [FromQuery]  bool? anyCurrent)
    {
        Console.WriteLine(anyCurrent);
        if (anyCurrent == true)
        {
            var result = _tournamentService.AnyCurrent();
            _logger.LogInformation("Getting any current tournament was requested, result: {anyCurrent}", anyCurrent);
            return Ok(result);
        }

        if (code == null)
        {
            var tournaments = _tournamentService.GetCurrent();
            if (tournaments == null)
            {
                _logger.LogError("Getting current tournament was requested, wasn't found");
                return NotFound(new ApiError() { Message = "No tournaments are currently held." });
            }

            _logger.LogInformation("Getting current tournaments was requested: {tournaments}", JsonSerializer.Serialize(tournaments));
            return Ok(tournaments);
        }

        var tournament = _tournamentService.Get(code);
        if (tournament == null)
        {
            _logger.LogError("Getting tournament info with {code} was requested, weren't found", code);
            return NotFound(new ApiError() { Message = "No tournament with given code was found.", Input = tournament });
        }

        _logger.LogInformation("Getting tournament info with {code} was requested, were found", code);
        return Ok(_tournamentService.Get(code));
    }

    [HttpGet]
    [Route("current")]
    public IActionResult GetAnyCurrent()
    {
        var anyCurrent = _tournamentService.AnyCurrent();
        _logger.LogInformation("Getting any current tournament was requested, result: {anyCurrent}", anyCurrent);
        return Ok(anyCurrent);
    }

    [HttpPost]
    [Route("")]
    public IActionResult Post(Tournament tournament)
    {
        var foundTournament = _tournamentService.Get(tournament.Code);
        if (foundTournament != null)
        {
            _logger.LogError("Unable to schedule tournament with code {code}: already exists", tournament.Code);
            return BadRequest(new ApiError() { Message = "Tournament with given code already exists.", Input = tournament });
        }

        if (tournament.StartDate is DateTime dt1 && dt1.ToUniversalTime() > tournament.EndDate.ToUniversalTime())
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament end date can't be earlier than start date.", Input = tournament });
        }

        if (tournament.StartDate is DateTime dt2 && dt2.ToUniversalTime() < DateTime.UtcNow)
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament start date can't be earlier than current date.", Input = tournament });
        }

        if (tournament.EndDate.ToUniversalTime() < DateTime.UtcNow)
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament end date can't be earlier than current date.", Input = tournament });
        }

        _tournamentService.Post(tournament);
        _logger.LogInformation("Tournament with code {code} was scheduled on hill {hill} from {startDate} to {endDate}.", tournament.Code, tournament.Hill.ToString(), tournament.StartDate.ToString(), tournament.EndDate.ToString());
        return Ok(tournament);
    }

    [HttpDelete]
    [Route("{tournamentCode}")]
    public IActionResult Delete(string tournamentCode, [FromBody] User user)
    {
        Tournament foundTournament = _tournamentService.Get(tournamentCode);
        if (user.Equals(foundTournament.CreatedBy))
        {
            if (foundTournament != null)
            {
                _tournamentService.Delete(tournamentCode);
                _logger.LogInformation("Tournament with code {tournamentCode} was deleted.", tournamentCode);
                return NoContent();
            }
            else
            {
                _logger.LogError("Unable to delete tournament with {tournamentCode} code, doesn't exist.", tournamentCode);
                return NotFound(new ApiError() { Message = "No such tournament exists.", Input = tournamentCode });
            }
        }
        else
        {
            _logger.LogError("Unable to delete tournament with {tournamentCode} code, authorization error.", tournamentCode);
            return NotFound(new ApiError() { Message = "Authorization failed.", Input = tournamentCode });
        }
    }
}