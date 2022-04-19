using System.Text.Json.Serialization;

namespace TravelApi.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
//The JsonIgnore attribute prevents the password property from being serialized and return in api responses
    [JsonIgnore]
    public string Password { get; set; }
  }
}