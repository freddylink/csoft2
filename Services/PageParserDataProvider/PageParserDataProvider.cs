using DbRepositories.Data.Object;
using Services.FileServiceModule;
using Services.PageParserHandler;
using Services.WebClientModule;
using System;
using System.Collections.Generic;

namespace Services.PageParserDataProvider
{
    public class PageParserDataProvider
    {
        private readonly IPageParserHandler _pageParserHandler;
        public PageParserDataProvider( IPageParserHandler pageParserHandler )
        {
            _pageParserHandler = pageParserHandler;
        }

        public List<WordsStatistic> GetWordsStatistics( Uri urlSite )
        {
            string webData = WebModule.DownloadData( urlSite );

            if ( webData == null )
            {
                Console.WriteLine( "Данная страница пустая" );
                throw new ArgumentNullException( "Данная страница пустая" );
            }
            FileService.SaveData( urlSite.Host, webData );
            var dict = _pageParserHandler.GetUniqueWords( webData );

            List<WordsStatistic> wordsStatistic = new List<WordsStatistic>();
            foreach ( var item in dict )
            {
                var el = new WordsStatistic
                {
                    SiteUrl = urlSite.Host,
                    UniqueWord = item.Key,
                    Count = item.Value,
                    Timestamp = DateTime.Now.Date
                };
                wordsStatistic.Add( el );
            }

            return wordsStatistic;
        }
    }
}
