using System.ComponentModel.DataAnnotations;
using Dsj2TournamentsServer.Models;

namespace Dsj2TournamentsApi.Models;

public class SentJump
{
    public User User { get; set; }

    [RegularExpression("[A-Za-z0-9]{12}", ErrorMessage = "Invalid replay code.")]
    public string ReplayCode { get; set; }
}