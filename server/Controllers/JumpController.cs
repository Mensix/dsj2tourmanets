using System.Text.Json;
using Dsj2TournamentsApi.Models;
using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsj2TournamentsServer.Controllers;

[ApiController]
[Route("[controller]")]
public class JumpController : ControllerBase
{
    private readonly ILogger _logger;

    private readonly IJumpService _jumpService;
    private readonly IReplayService _replayService;
    private readonly ITournamentService _tournamentService;

    public JumpController(ILogger<JumpController> logger, IJumpService jumpService, IReplayService replayService, ITournamentService tournamentService)
    {
        _logger = logger;
        _jumpService = jumpService;
        _replayService = replayService;
        _tournamentService = tournamentService;
    }

    [HttpGet]
    [Route("{replayCode}")]
    public async Task<IActionResult> Get(string replayCode)
    {
        if (replayCode.Length != 12)
        {
            _logger.LogError("Unable to find jump with {replayCode} replay code.", replayCode);
            return NotFound(new ApiError() { Message = "Jump with given replay code doesn't exists." });
        }

        var jump = await _replayService.GetJump(replayCode, null);
        if (jump == null)
        {
            _logger.LogError("Unable to find jump with {code} replay code.", replayCode);
            return NotFound(new ApiError() { Message = "Jump with given replay code doesn't exists." });
        }

        _logger.LogInformation("Jump with replay code {code} was found: {jump}", replayCode, JsonSerializer.Serialize(jump));
        return Ok(jump);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SentJump sentJump)
    {
        var currentTournaments = _tournamentService.GetCurrent();
        if (currentTournaments == null)
        {
            _logger.LogError("Unable to post jump, no tournament is currently held.");
            return NotFound(new ApiError() { Message = "No tournament is currently held." });
        }

        var jumpAlreadyExists = _jumpService.AlreadyExists(sentJump.ReplayCode);
        if (jumpAlreadyExists)
        {
            _logger.LogError("Unable to post jump, already sent: {sentJump}", JsonSerializer.Serialize(sentJump));
            return BadRequest(new ApiError() { Message = "The jump was already sent." });
        }

        var jump = await _replayService.GetJump(sentJump.ReplayCode, sentJump.User);
        if (jump == null)
        {
            _logger.LogError("Unable to post jump, doesn't exist: {sentJump}", JsonSerializer.Serialize(jump));
            return NotFound(new ApiError() { Message = "Jump with given replay code doesn't exist." });
        }

        if (jump.Player.StartsWith("CPU"))
        {
            _logger.LogError("Unable to post jump, performed by CPU: {sentJump}", JsonSerializer.Serialize(jump));
            return BadRequest(new ApiError() { Message = "Jump performed by CPU can't be sent." });
        }

        if (currentTournaments.All(x => x.StartDate is DateTime dt && jump.Date < DateOnly.FromDateTime(dt)))
        {
            _logger.LogError("Unable to post jump, it's too old: {sentJump}", JsonSerializer.Serialize(jump));
            return BadRequest(new ApiError() { Message = "Jump performed earlier than tournament day start can't be sent." });
        }

        if (currentTournaments?.All(x => x.Hill.Equals(jump.Hill)) == false)
        {
            _logger.LogError("Unable to post jump, invalid hill: {sentJump}", JsonSerializer.Serialize(jump));
            return BadRequest(new ApiError() { Message = "Jump was performed on invalid hill." });
        }

        var alreadySentBetter = _jumpService.AnyBetterThan(jump);
        if (alreadySentBetter)
        {
            _logger.LogError("Unable to post jump, already sent better: {sentJump} and {jump}", JsonSerializer.Serialize(sentJump), JsonSerializer.Serialize(jump));
            return BadRequest(new ApiError() { Message = "Jump with better result has been already sent." });
        }

        var foundTournamentCode = _jumpService.GetTournamentCode(jump);
        if (foundTournamentCode == null)
        {
            _logger.LogError("Unable to post jump, couldn't find tournament code: {sentJump} and {jump}", JsonSerializer.Serialize(sentJump), JsonSerializer.Serialize(jump));
        }

        jump.TournamentCode = foundTournamentCode;
        var foundTournament = _tournamentService.Get(jump.TournamentCode);

        _jumpService.Post(jump);
        _logger.LogInformation("Jump was succesfully post to tournament {code} by {nickname}: {jump}", jump.TournamentCode, jump.User.Username, JsonSerializer.Serialize(jump));
        return Ok(jump);
    }

    [HttpDelete]
    [Route("{replayCode}")]
    public async Task<IActionResult> Delete(string replayCode)
    {
        if (replayCode.Length != 12)
        {
            _logger.LogError("Unable to delete jump with {replayCode} replay code.", replayCode);
            return NotFound(new ApiError() { Message = "Jump with given replay code doesn't exists." });
        }

        _jumpService.Delete(replayCode);
        _logger.LogInformation("Jump with replay code {replayCode} was deleted.", replayCode);
        return Ok(replayCode);
    }
}