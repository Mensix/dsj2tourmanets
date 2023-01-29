using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dsj2TournamentsServer.Models;

public class Jump
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    [NotMapped]
    public int Place { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User User { get; set; }

    [Required]
    public string ReplayCode { get; set; }

    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Hill Hill { get; set; }

    [Required]
    public string Player { get; set; }

    [Required]
    public decimal Length { get; set; }

    public bool Crash { get; set; }

    [Required]
    public decimal Points { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TournamentCode { get; set; }

    public override bool Equals(object o)
    {
        Jump jump = (Jump)o;
        return jump.Hill.Equals(Hill) && jump.Player == Player && jump.Length == Length && jump.Points == Points && jump.Date.CompareTo(Date) == 0;
    }
}