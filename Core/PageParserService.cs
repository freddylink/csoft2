using DbRepositories.Data;
using DbRepositories.Data.Object;
using Services.PageParserDataProvider;
using Services.UrlValidator;
using System;
using System.Collections.Generic;

namespace Core
{
    public class PageParserService : IPageParserService
    {
        private readonly WordsStatisticRepository _wordsStatisticRepository;
        private readonly PageParserDataProvider _pageParserDataProvider;

        public PageParserService( WordsStatisticRepository wordsStatisticRepository, PageParserDataProvider pageParserDataProvider )
        {
            _wordsStatisticRepository = wordsStatisticRepository;
            _pageParserDataProvider = pageParserDataProvider;

        }

        public void GetPageStatistics( string urlSite )
        {
            Uri validUrlSite = UrlValidator.TryGetValidUrl( urlSite );

            if ( validUrlSite == null )
            {
                Console.WriteLine( "Url должен передаваться в формате http://example.com" );
                throw new Exception( "Переданная строка не является допустимым url" );
            }

            List<WordsStatistic> wordsStatistic = _wordsStatisticRepository.GetWordItems( validUrlSite.Host, DateTime.Now );

            if ( wordsStatistic.Count == 0 )
            {
                IEnumerable<WordsStatistic> statistic = _pageParserDataProvider.GetWordsStatistics( validUrlSite );
                wordsStatistic.AddRange( statistic );
                _wordsStatisticRepository.Add( statistic );
            }

            foreach ( var item in wordsStatistic )
            {
                Console.WriteLine( item.UniqueWord + "-" + item.Count );
            }
        }
    }
}
