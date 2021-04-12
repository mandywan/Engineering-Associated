using System;
using System.IO;
using System.Linq;
using AeDirectory.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace IntegrationTest
{
    /**
     * This class sets up the custom web host factory to be used in tests
     * Resource: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1#test-app-organization
     */
    public class CustomSQLiteWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        private SqliteConnection Connection;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            // Read data into the database
            SqliteCommand command = Connection.CreateCommand();
            Console.WriteLine(Directory.GetCurrentDirectory());
            //string dumpFile = Path.Combine("path", "/create_sqlite_testdb.sql");
            // current directory is /server_tests/IntegrationTest/bin/Debug/netcoreapp3.1
            string importedSql = File.ReadAllText("../../../create_sqlite_testdb.sql");
            command.CommandText = importedSql;
            command.ExecuteNonQuery();
            
            builder.ConfigureServices(services =>
            {
                // remove application database context
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AEV2Context>));

                services.Remove(descriptor);

                // set up 
                services.AddDbContext<AEV2Context>(options =>
                {
                    options.UseSqlite(Connection);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AEV2Context>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomSQLiteWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Connection.Close();
        }
    }
}