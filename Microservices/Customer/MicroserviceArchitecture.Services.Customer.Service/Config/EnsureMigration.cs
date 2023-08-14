using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceArchitecture.Services.Customer.Service.Config
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : CustomerDbContext
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<T>();
                
                dbContext.Database.Migrate();
            }
        }
    }
}