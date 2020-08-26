using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        //Restaurant Entity (The class that gets stroed in the database)
        //Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Rating
        {
            get
            {
                //return FoodRating + EnvironmentRating + CleanlinessRating / 3;

                //Calculate total Average score based on ratings
                double totalAverageRating = 0;

                // Add all Ratings together to get total Average Rating
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //Return Avg of total if count > 0
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            }
        }

        // Avg food rating
        public double FoodRating
        {
            get
            {                   
                double avg = 0;
                foreach (var rating in Ratings)
                    avg += rating.FoodScore;
                return (Ratings.Count > 0) ? avg / Ratings.Count : 0;
            }
        }

        // Avg Environment rating
        public double EnvironmentRating
        {
            get
            {
                var scores = Ratings.Select(rating => rating.EnvironmentScore);
                double totalEnvironmentScore = scores.Sum();
                return Ratings.Count > 0 ? totalEnvironmentScore / Ratings.Count : 0;
            }
        }

        //avg Cleanliness rating
        public double CleanlinessRating
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return Ratings.Count > 0 ? totalScore / Ratings.Count : 0;
                //return Ratings.Count > 0 ? Ratings.Select(r => r.CleanlinessScore).Average() : 0;
            }
        }

        public bool IsRecommended => Rating >= 8;

        //All of the associated Rating objects from the database
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}