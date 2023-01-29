using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dsj2TournamentsServer.Models;

public class Hill
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public override bool Equals(object o)
    {
        Hill hill = (Hill)o;
        return hill.Name == Name;
    }

    public override string ToString()
    {
        return Name;
    }
}