using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{

public class RatingRepositoryIntegrationTests
    {
        private readonly DbContextOptions<MyShop215736745Context> _dbOptions;

        public RatingRepositoryIntegrationTests()
        {
            // יצירת מסד נתונים In-Memory
            _dbOptions = new DbContextOptionsBuilder<MyShop215736745Context>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task AddRating_ShouldAddRatingToDatabase()
        {
            // Arrange
            using (var context = new MyShop215736745Context(_dbOptions))
            {
                var repository = new RatingRepository(context);
                var rating = new Rating
                {
                    RatingId = 1,
                    Host = "100",
                    UserId = 200,
                    Referer = "5",
                    Path = "Great product!"
                };

                // Act
                var result = await repository.AddRating(rating);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.RatingId);
                Assert.Equal(5, result.Referer);
            }

            // וידוא שהנתון באמת נשמר במסד הנתונים
            using (var context = new MyShop215736745Context(_dbOptions))
            {
                var savedRating = await context.Ratings.FindAsync(1);
                Assert.NotNull(savedRating);
                Assert.Equal(5, savedRating.Score);
                Assert.Equal("Great product!", savedRating.Comment);
            }
        }
    }

}

