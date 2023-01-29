using System.ComponentModel.DataAnnotations;
using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsApi.Models;

public class SentJump
{
    [Required]
    public User User { get; set; }

    [RegularExpression("[A-Za-z0-9]{12}", ErrorMessage = "Invalid replay code.")]
    [Required]
    public string ReplayCode { get; set; }
}