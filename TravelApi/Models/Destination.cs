using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TravelApi.Models
{
  public class Destination
  {
    public Destination()
    {
      this.JoinEntities = new HashSet<Review>();
    }

    public int DestinationId { get; set; }
    public string City { get; set; }
    public virtual ICollection<Review> JoinEntities { get; set; }
  }
}