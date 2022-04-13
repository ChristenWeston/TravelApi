using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;
using System.Linq;
using System;



namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public ReviewsController(TravelApiContext db)
    {
      _db = db;
    }

    // GET api/animals
    //http://localhost:5000/api/animals?species=dinosaur&gender=female
    [HttpGet]
    //added a parameter to the method of type string that we've called species. The naming here is important as .NET
    // will automatically bind parameter values based on the query string. A call to http://localhost:5000/api/animals?species=dinosaur 
    //will now trigger our Get method and automatically bind the value "dinosaur" to the variable species. It does this by utilizing model binding
    public async Task<ActionResult<IEnumerable<Review>>> Get(string description, int rating, string city)
    {
      var query = _db.Reviews.AsQueryable();

      if (description != null)
      {
        query = query.Where(entry => entry.Description == description);
      }

      if (rating > 0)
      {
        query = query.Where(entry => entry.Rating == rating);
      }

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      return await query.ToListAsync();
    }

    // GET: api/Animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);

      if (review == null)
      {
          return NotFound();
      }
      return review;
    }

//https://localhost:5001/api/Reviews/HighestRated
    [HttpGet("HighestRated")]
    public async Task<ActionResult<IEnumerable<Review>>> GetHighestRated()
    {
      var query = _db.Reviews.AsQueryable();
      var highestRating = _db.Reviews.Max(review => review.Rating);
      query = query.Where(review => review.Rating == highestRating);
      return await query.ToListAsync();
    }
// var query2 = _db.Reviews.GroupBy(x => x.City).Select(x => x.OrderByDescending(x => x.Count()).FirstOrDefault());
// .GroupBy _db.Reviews.City into CityCount
// .Select(grp => new {CityCount.Max()}


    //https://localhost:5001/api/Reviews/MostRatings
    // [HttpGet("MostReviews")]
    // public async Task<ActionResult<string>> GetMostRatings()
    // {
    //   var query = _db.Reviews.AsQueryable();
        // var groupByQuery = from reviews in _db.Reviews
        // group reviews by reviews.City into newGroup
        // orderby newGroup.Key
        // select newGroup;
        // var groupByQueryList = groupByQuery.ToListAsync();
    //   var listOfReviews = _db.Reviews.ToList();
    //   var mostReviews = query.GroupBy(review => review.City);
    //   var mostReviews2 = listOfReviews.GroupBy(review => review.City).OrderByDescending(city => city.Count());
    // foreach(var result in groupByQueryList)
    // {
    //   Console.WriteLine("Foreach time");
    //   Console.WriteLine(result.Key);
    //   foreach (var res in result)
    //   {

    //   Console.WriteLine ("ConsoleWriteLine City: " + result);
    //   }
    // }
    //   Console.WriteLine("Console Writeline time");
    //   Console.WriteLine(mostReviews2.Where(city => city.Count() > 0));
    //   // var mostReviews = _db.Reviews.Max(review => review.City);
    // /*   query = query.Where(review => review.City == mostRating); */
    //   // return await query.ToListAsync();
    //   return ("HI");
    //  }
    
    [HttpGet("TestRoute")]
    public async Task<ActionResult<string>> TestRoute()
    {
      var listReview = await _db.Reviews.ToListAsync();
      var groupedReviews = listReview.GroupBy(review => review.City);
      string returnString = "";
      foreach (var cityGroup in groupedReviews)
      {
        returnString += ("City: " + cityGroup.Key);
        returnString += System.Environment.NewLine;
  
        foreach(Review review in cityGroup)
        {
          returnString += ("Review: " + review.Description);
          returnString += System.Environment.NewLine;
        }
      }
      return (returnString);
    }

//Our POST route utilizes the function CreatedAtAction. This is so that it can end up returning the Animal object to the user
//, as well as update the status code to 201, for "Created", rather than the default 200 OK.
    // POST api/animals
    [HttpPost]
    public async Task<ActionResult<Review>> Post(Review review)
    {
      _db.Reviews.Add(review);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }

    // PUT: api/Reviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Review review)
    {
      if (id != review.ReviewId)
      {
        return BadRequest();
      }
      _db.Entry(review).State = EntityState.Modified;
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReviewExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

// forms in HTML5 don't allow for PUT, PATCH or DELETE verbs. For that reason, we had to use HttpPost along
// with an ActionName like this: [HttpPost, ActionName("Delete")]. However, we aren't using HTML anymore and
// there are no such limitations with an API. For that reason, we can use HttpPut and HttpDelete.


    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }
      _db.Reviews.Remove(review);
      await _db.SaveChangesAsync();
      return NoContent();
    }

// Private method for this controller
    private bool ReviewExists(int id)
    {
      return _db.Reviews.Any(e => e.ReviewId == id);
    }

  }
}

//http://localhost:5000/api/reviews