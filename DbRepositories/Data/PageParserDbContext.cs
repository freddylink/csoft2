using DbRepositories.Data.Configurations;
using DbRepositories.Data.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DbRepositories.Data
{
    public class PageParserDbContext : DbContext
    {
        public PageParserDbContext()
        {
        }
        public PageParserDbContext( DbContextOptions<PageParserDbContext> options )
            : base( options )
        { }

        public DbSet<WordsStatistic> WordsStatistic { get; set; }
        public DbSet<Log> Log { get; set; }

        
        /*
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            //optionsBuilder.UseSqlServer( @"Server=WEBDEV28;Database=words_statistic;Trusted_Connection=True;MultipleActiveResultSets=True;" );
            optionsBuilder.UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) );
            base.OnConfiguring( optionsBuilder );
        }
        */
        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new WordsStatisticEntityConfiguration() );
            modelBuilder.ApplyConfiguration( new LogEntityConfiguration() );
        }
    }
}
