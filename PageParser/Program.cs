using Core;
using DbRepositories.Data;
using Microsoft.Extensions.DependencyInjection;
using Services.PageParserDataProvider;
using Services.PageParserHandler;
using Services.UrlValidator;
using Services.WebClientModule;
using System;

namespace PageParser
{
    public class Program
    {
        static void Main( string[] args )
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<WordsStatisticRepository>()
                .AddScoped<LogRepository>()
                .AddScoped<UrlValidator>()
                .AddDbContext<PageParserDbContext>()
                .AddScoped<IPageParserHandler, HtmlTextParseHandler>()
                .AddScoped<IPageParserService, PageParserService>()
                .AddScoped<PageParserDataProvider>()
                .AddScoped<WebSiteLoader>()
                .BuildServiceProvider();
            LogRepository logRepository = serviceProvider.GetService<LogRepository>();

            if ( args.Length == 0 )
            {
                //string urlSite = args[ 0 ];

                using ( var db = new PageParserDbContext() )
                {
                    try
                    {
                        IPageParserService pageParser = serviceProvider.GetService<IPageParserService>();
                        pageParser.GetPageStatistics( "https://simbirsoft.com" );

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
                Console.WriteLine( logMessage + " Необходимо передать url в формате http://example.com" );
            }
            Console.ReadKey();
        }
    }
}
