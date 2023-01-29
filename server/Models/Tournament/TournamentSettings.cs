using System.Text.Json.Serialization;

namespace Dsj2TournamentsServer.Models.Tournament;

public class TournamentSettings
{
    [JsonIgnore]
    public int Id { get; set; }
    public bool LiveBoard { get; set; }
}