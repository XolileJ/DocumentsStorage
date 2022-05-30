using DocumentsStorage.Infra.Data.Extentions;
using DocumentsStorage.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PPECB.UserLinking.Infra.Data.Mappings;
using System.IO;

namespace DocumentsStorage.Infra.Data.Context
{
    public class DocumentContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new DocumentMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            //optionsBuilder.U
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DocumentsStorageConnection"));

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
