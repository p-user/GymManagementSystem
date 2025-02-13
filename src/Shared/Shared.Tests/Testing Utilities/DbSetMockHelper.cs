
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using Moq;
using Shared.DDD;
using System.Linq.Expressions;

namespace IntegrationTesting.Testing_Utilities
{
    public static  class DbSetMockHelper
    {

        public static Mock<DbSet<T>> CreateDbSetMock<T, TKey>(this ICollection<T> data) 
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



            //Mock AddAsync
             mockSet.Setup(x => x.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((T entity, CancellationToken _) =>
            {
                // Return a mocked EntityEntry

                var entityEntryMock = new Mock<EntityEntry<T>>();

                entityEntryMock.Setup(e => e.Entity).Returns(entity);
                data.Add(entity); // Add the entity to the data collection
                return null;
            });

            //   mockSet.Setup(m => m.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
            //.Callback<T, CancellationToken>((entity, _) => data.Add(entity))
            //.ReturnsAsync((T entity, CancellationToken _) =>
            //{
            //    var entryMock = new Mock<EntityEntry<T>>();
            //    entryMock.Setup(e => e.Entity).Returns(entity);
            //    return entryMock.Object;
            //});


            //mockSet.Setup(m => m.Include(It.IsAny<Expression<Func<T, object>>>()))
            //.Returns((Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>)mockSet.Object);




            // Mock async FirstOrDefault
            //mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync((Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
            //    {
            //        // Simulate the async behavior for FirstOrDefault
            //        return data.AsQueryable().FirstOrDefault(predicate);
            //    });

           

            return mockSet;
        }

    }


}
