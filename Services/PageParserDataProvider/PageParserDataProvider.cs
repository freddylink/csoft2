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
            string webData = WebSiteLoader.DownloadData( urlSite );

            if ( webData == null )
            {
                Console.WriteLine( "Данная страница пустая" );
                throw new ArgumentNullException( "Данная страница пустая" );
            }

            FileService.SaveData( urlSite.Host, webData );
            SortedDictionary<string, int> uniqueWordsDict = _pageParserHandler.GetUniqueWords( webData );

            List<WordsStatistic> wordsStatistic = new List<WordsStatistic>();
            foreach ( KeyValuePair<string, int> wordItem in uniqueWordsDict )
            {
                wordsStatistic.Add( new WordsStatistic
                {
                    SiteUrl = urlSite.Host,
                    UniqueWord = wordItem.Key,
                    Count = wordItem.Value,
                    Timestamp = DateTime.Now.Date
                } );
            }

            return wordsStatistic;
        }
    }
}
