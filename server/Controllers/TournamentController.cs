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
    [Route("current")]
    public IActionResult GetCurrent()
    {
        var tournament = _tournamentService.GetCurrent();
        if (tournament == null)
        {
            _logger.LogError("Getting current tournament was requested, wasn't found");
            return NotFound(new ApiError() { Message = "No tournament is currently held." });
        }

        _logger.LogInformation("Getting current tournament was requested: {tournament}", JsonSerializer.Serialize(tournament));
        return Ok(tournament);
    }

    [HttpGet]
    [Route("{code}/results")]
    public IActionResult GetResults(string code)
    {
        var tournament = _tournamentService.Get(code);
        if (tournament == null)
        {
            _logger.LogError("Getting tournament results with {code} was requested, weren't found", code);
            return NotFound(new ApiError() { Message = "No tournament with given code was found." });
        }

        if (tournament.EndDate.ToUniversalTime() > DateTime.UtcNow && !tournament.Settings.LiveBoard)
        {
            _logger.LogError("Getting tournament results with {code} was requested, can't be fetched yet", code);
            return NotFound(new ApiError() { Message = "Tournament results aren't available at the moment." });
        }

        _logger.LogInformation("Getting tournament results with {code} was requested, were found", code);
        return Ok(_tournamentService.GetResults(code));
    }

    [HttpPost]
    [Route("")]
    public IActionResult Post(Tournament tournament)
    {
        var foundTournament = _tournamentService.Get(tournament.Code);
        if (foundTournament != null)
        {
            _logger.LogError("Unable to schedule tournament with code {code}: already exists", tournament.Code);
            return BadRequest(new ApiError() { Message = "Tournament with given code already exists." });
        }

        if (tournament.StartDate is DateTime dt1 && dt1.ToUniversalTime() > tournament.EndDate.ToUniversalTime())
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament end date can't be earlier than start date." });
        }

        if (tournament.StartDate is DateTime dt2 && dt2.ToUniversalTime() < DateTime.UtcNow)
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament start date can't be earlier than current date." });
        }

        if (tournament.EndDate.ToUniversalTime() < DateTime.UtcNow)
        {
            _logger.LogError("Unable to schedule tournament with code {code} - invalid date: {tournament}", tournament.Code, JsonSerializer.Serialize(tournament));
            return BadRequest(new ApiError() { Message = "Tournament end date can't be earlier than current date." });
        }

        if (tournament.StartDate == null)
        {
            tournament.StartDate = DateTime.UtcNow;
        }

        _tournamentService.Post(tournament);
        _logger.LogInformation("Tournament with code {code} was scheduled on hill {hill} from {startDate} to {endDate}.", tournament.Code, tournament.Hill.ToString(), tournament.StartDate.ToString(), tournament.EndDate.ToString());
        return Ok(tournament);
    }

    [HttpDelete]
    [Route("{code}")]
    public IActionResult Delete(string code, [FromBody] User user)
    {
        Tournament foundTournament = _tournamentService.Get(code);
        if (user.Equals(foundTournament.CreatedBy))
        {
            if (foundTournament != null)
            {
                _tournamentService.Delete(code);
                _logger.LogInformation("Tournament with code {code} was deleted.", code);
                return NoContent();
            }
            else
            {
                _logger.LogError("Unable to delete tournament with {code} code, doesn't exist.", code);
                return NotFound(new ApiError() { Message = "No such tournament exists." });
            }
        }
        else
        {
            _logger.LogError("Unable to delete tournament with {code} code, authorization error.", code);
            return NotFound(new ApiError() { Message = "Authorization failed." });
        }
    }
}