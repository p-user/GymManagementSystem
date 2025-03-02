using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data
{
    public class DatabaseSeeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ISeed> _seeders;

        public DatabaseSeeder(IServiceProvider serviceProvider, IEnumerable<ISeed> seeders)
        {
            _serviceProvider = serviceProvider;
            _seeders = seeders;
        }

        public async Task SeedAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                foreach (var seeder in _seeders)
                {
                    await seeder.SeedAsync();
                }
            }
        }
    }
} 