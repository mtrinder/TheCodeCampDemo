using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TheCodeCamp.Data
{
    public class CampContext : DbContext
    {
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public static readonly LoggerFactory MyConsoleLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true) });

        public CampContext(DbContextOptions<CampContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyConsoleLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camp>().HasKey(c => c.CampId);
            modelBuilder.Entity<Camp>()
                .HasOne(a => a.Location)
                .WithOne(a => a.Camp)
                .HasForeignKey<Location>(c => c.CampId);

            modelBuilder.Entity<Talk>().HasKey(c => c.TalkId);

            modelBuilder.Entity<TalkSpeakers>().HasKey(c => new { c.TalkId, c.SpeakerId });

            modelBuilder.Entity<Speaker>().HasKey(c => c.SpeakerId);
            modelBuilder.Entity<Location>().HasKey(c => c.LocationId);

            modelBuilder.Entity<Camp>().HasData(new Camp
            {
                CampId = 1,
                Moniker = "ATL2018",
                Name = "Atlanta Code Camp",
                EventDate = new DateTime(2018, 10, 18),
                Length = 1,
                LocationId = 1
            });

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = 1,
                CampId = 1,
                VenueName = "Atlanta Convention Center",
                Address1 = "123 Main Street",
                CityTown = "Atlanta",
                StateProvince = "GA",
                PostalCode = "12345",
                Country = "USA"
            });

            modelBuilder.Entity<Talk>().HasData(new[]
            {
                new
                {
                  TalkId = 1,
                  CampId = 1,
                  SpeakerId = 1,
                  Title = "Entity Framework From Scratch",
                  Abstract = "Entity Framework from scratch in an hour. Probably cover it all",
                  Level = 100,
                },
                new
                {
                  TalkId = 2,
                  CampId = 1,
                  SpeakerId = 2,
                  Title = "Writing Sample Data Made Easy",
                  Abstract = "Thinking of good sample data examples is tiring.",
                  Level = 200,
                }
            });

            modelBuilder.Entity<Speaker>().HasData(new[]
            {
                new
                {
                    TalkId = 1,
                    SpeakerId = 1,
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    BlogUrl = "http://wildermuth.com",
                    Company = "Wilder Minds LLC",
                    CompanyUrl = "http://wilderminds.com",
                    GitHub = "shawnwildermuth",
                    Twitter = "shawnwildermuth"
                },
                new
                {
                    TalkId = 2,
                    SpeakerId = 2,
                    FirstName = "Resa",
                    LastName = "Wildermuth",
                    BlogUrl = "http://shawnandresa.com",
                    Company = "Wilder Minds LLC",
                    CompanyUrl = "http://wilderminds.com",
                    GitHub = "resawildermuth",
                    Twitter = "resawildermuth"
                }
            });

            modelBuilder.Entity<TalkSpeakers>().HasData(new[]
            {
                new
                {
                    TalkId = 1,
                    SpeakerId = 1,
                },
                new
                {
                    TalkId = 2,
                    SpeakerId = 2,
                },
                new
                {
                    TalkId = 1,
                    SpeakerId = 2,
                },
                new
                {
                    TalkId = 2,
                    SpeakerId = 1,
                }
            });
        }
    }
}
