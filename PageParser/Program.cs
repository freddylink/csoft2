using Core;
using DbRepositories.Data;
using Microsoft.Extensions.DependencyInjection;
using Services.PageParserDataProvider;
using Services.PageParserHandler;
using Services.WebClientModule;
using System;

namespace PageParser
{
    public class Program
    {
        static void Main( string[] args )
        {
            //if ( args.Length > 0 )
            // string url = arg[0];
            //bool IsValidateUrl = IsValidateUrl( str );
            var str = @"https://www.simbirsoft.com/";
            Uri urlSite = GetValidUrl( str );
            
            //if ( isValid )
            //else  
            // Console.WriteLine( Url должен передаваться в формате http://example.com );
            // throw new Exception( "переданная строка не является допустимым url" );
            
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<WordsStatisticRepository>()
                .AddScoped<LogRepository>()
                .AddDbContext<PageParserDbContext>()
                .AddScoped<IPageParserHandler, HtmlTextParseHandler>()
                .AddScoped<IPageParserService, PageParserService>()
                .AddScoped<PageParserDataProvider>()
                .AddScoped<WebModule>()
                .BuildServiceProvider();

            using ( var db = new PageParserDbContext() )
            {
                if ( !string.IsNullOrEmpty(str) )
                {
                    try
                    {
                        //получить html
                        //записать в файл
                        //извлечь текстовые данные
                        //подсчитать количество уникальных
                        //сохранить в бд
                        //вывести на экран
                        var pageParser = serviceProvider.GetService<IPageParserService>();
                        //pageParser.GetPageStatistics( args[ 0 ] );
                        pageParser.GetPageStatistics( urlSite );

                    }
                    catch ( Exception ex )
                    {
                        var logRepository = serviceProvider.GetService<LogRepository>();
                        logRepository.AddException( ex.Message );
                        Console.WriteLine( ex.Message );
                    }
                }
            
            }
        }

        private static Uri GetValidUrl( string urlSite )
        {
            Uri uriResult;
            bool isValid = Uri.TryCreate( urlSite, UriKind.Absolute, out uriResult )
                        && (uriResult.Scheme == Uri.UriSchemeHttp
                        || uriResult.Scheme == Uri.UriSchemeHttps);
            return isValid ? uriResult : null;
        }
        /*
        private static bool ValidateRegexUrl( string URL )
        {
            //string Pattern = @"(http(s)?://)?([\w-]+\.)+[\w-]+[\w-]+[\.]+[\][a-z.]{2,3}$+([./?%&=]*)?";
            string Pattern = @"/^((?:https?\:)?(?:\/{2})?)?((?:[\w\d-_]{1,64})\.(?:[\w\d-_\.]{2,64}))(\:\d{2,6})?((?:\/|\?|#|&){1}(?:[\w\d\S]+)?)?$/u";
            Regex Rgx = new Regex( Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
            return Rgx.IsMatch( URL );
        }
        */
    }
}
