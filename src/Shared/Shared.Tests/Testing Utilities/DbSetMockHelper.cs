
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Shared.DDD;
using System.Linq.Expressions;

namespace IntegrationTesting.Testing_Utilities
{
    public static  class DbSetMockHelper
    {

        public static Mock<DbSet<T>> CreateDbSetMock<T, TKey>(this IEnumerable<T> data) 
            where T : Entity<TKey>
        {
           
            var mockSet = data.AsQueryable().BuildMockDbSet(); // Create a new mock DbSet


            // Mock FindAsync to return the entity 
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] keyValues) =>
                {
                    // Simulate finding the entity by the key
                    var key = (TKey)keyValues[0]; 

                    return data.AsQueryable().FirstOrDefault(e => e.Id.Equals(key));
                });



            return mockSet;
        }

    }


}
