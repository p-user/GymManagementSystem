
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using Moq;
using Shared.DDD;

namespace IntegrationTesting.Testing_Utilities
{
    public static class DbSetMockHelper
    {

        public static Mock<DbSet<T>> CreateDbSetMock<T, TKey>(this IList<T> data)
            where T : Entity<TKey>
        {


            //**************************
            //
            //
            //Todo : why customized mockes are not working ????
            //
            //
            //
            //**************************




            var mockSet = data.AsQueryable().BuildMockDbSet(); // Create a new mock DbSet



            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
               .ReturnsAsync((object[] keyValues) =>
               {
                   var id = (Guid)keyValues[0];  // Id is a Guid
                   return data.FirstOrDefault(e => e.Id.Equals(id));  // Find the exercise by Id
               });


            //Mock AddAsync
            mockSet.Setup(m => m.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
              .Callback<T, CancellationToken>((entity, _) => data.Add(entity))
              .Returns<T, CancellationToken>((entity, _) => ValueTask.FromResult((EntityEntry<T>)null!));


            //Mock AnyAsync
            //mockSet.Setup(m => m.AnyAsync(It.IsAny<CancellationToken>()))
            //   .Returns<CancellationToken>(ct => Task.FromResult(data.Any()));





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
