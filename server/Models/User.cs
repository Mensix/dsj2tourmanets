using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dsj2TournamentsServer.Models;

public class User
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public ulong UserId { get; set; }

    public string Username { get; set; }

    public override bool Equals(object o)
    {
        User user = (User)o;
        return user.UserId == UserId;
    }
}