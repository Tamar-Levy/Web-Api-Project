using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        MyShop215736745Context _context;

        public RatingRepository(MyShop215736745Context context)
        {
            _context = context;
        }

        //Post
        public async Task<Rating> AddRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }
    }
}
