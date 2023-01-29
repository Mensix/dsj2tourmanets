using AngleSharp;
using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsServer.Services;

public interface IReplayService
{
    Task<Jump> GetJump(string replayCode, User user);
}

public class ReplayService : IReplayService
{
    private readonly IUserService _userService;
    public ReplayService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Jump> GetJump(string replayCode, User user)
    {
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync($"https://replay.dsj2.com/{replayCode}");
        var jumpExists = document.QuerySelector("table") is not null;

        if (!jumpExists)
        {
            return null;
        }
        else
        {
            var jumpData = document.QuerySelectorAll("tr :nth-child(2)").Select(x => x.TextContent).ToArray();
            var isCrash = jumpData[2].Last() == '*';
            return new Jump()
            {
                User = user != null ? (_userService.Get(user.UserId) ?? user) : null,
                ReplayCode = replayCode,
                Hill = new()
                {
                    Name = jumpData[0]
                },
                Player = jumpData[1],
                Length = decimal.Parse(jumpData[2].Replace(isCrash ? " m*" : " m", string.Empty)),
                Crash = isCrash,
                Points = decimal.Parse(jumpData[3]),
                Date = DateOnly.ParseExact(jumpData[4], "yyyy-MM-dd")
            };
        }
    }
}