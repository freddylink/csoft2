using Core;
using DbRepositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.PageParserDataProvider;
using Services.PageParserHandler;
using Services.UrlValidator;
using Services.WebClientModule;
using System;
using System.IO;

namespace PageParser
{
    public class Program
    {
        static void Main( string[] args )
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath( Path.Combine( AppContext.BaseDirectory ) )
                .AddJsonFile( "appsettings.json", optional: true )
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton( Configuration )
                .AddLogging()
                .AddScoped<WordsStatisticRepository>()
                .AddScoped<LogRepository>()
                .AddScoped<UrlValidator>()
                .AddDbContext<PageParserDbContext>( options => options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) )
                .AddScoped<IPageParserHandler, HtmlTextParseHandler>()
                .AddScoped<IPageParserService, PageParserService>()
                .AddScoped<PageParserDataProvider>()
                .AddScoped<WebSiteLoader>()
                .BuildServiceProvider();
            LogRepository logRepository = serviceProvider.GetService<LogRepository>();
            var context = serviceProvider.GetRequiredService<PageParserDbContext>();

            if ( args.Length > 0 )
            {
                string urlSite = args[ 0 ];
                using ( var db = context )
                {
                    try
                    {
                        IPageParserService pageParser = serviceProvider.GetService<IPageParserService>();
                        pageParser.GetPageStatistics( urlSite );

                    }
                    catch ( Exception ex )
                    {
                        logRepository.AddException( ex.Message );
                        Console.WriteLine( ex.Message );
                    }
                }
            }
            else
            {
                string logMessage = "Были переданы пустые данные.";
                logRepository.AddException( logMessage );
                Console.WriteLine( logMessage + " Необходимо передать url в формате http://example.com или example.com" );
            }
        }
    }
}
