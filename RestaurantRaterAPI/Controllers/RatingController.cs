using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static System.Net.WebRequestMethods;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Create New Ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            if (model == null)
                return BadRequest("Your request cannot be empty");

            //Make sure model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Find Target Restaurant
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist");

            //The Restaurant is not null
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated {restaurant.Name} successfully!");
            return InternalServerError();
        }

        //Get a rating by its ID?

        //Get ALL Ratings for a specific restaurant by the restaurant ID

        //Update Rating

        //Delete Rating


    }
}
