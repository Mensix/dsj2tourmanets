using System.ComponentModel.DataAnnotations;
using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsApi.Models;

public class SentJump
{
    [Required]
    public User User { get; set; }

    [Required]
    public string ReplayCode { get; set; }
}