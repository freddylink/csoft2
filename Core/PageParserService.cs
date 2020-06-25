using DbRepositories.Data;
using DbRepositories.Data.Object;
using Services.PageParserDataProvider;
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

        public void GetPageStatistics( Uri urlSite )
        {
            List<WordsStatistic> wordsStatistic = _wordsStatisticRepository.GetWordItems( urlSite.Host, DateTime.Now );
            if ( wordsStatistic.Count == 0 )
            {
                IEnumerable<WordsStatistic> statistic = _pageParserDataProvider.GetWordsStatistics( urlSite );
                wordsStatistic.AddRange( statistic );
                _wordsStatisticRepository.Add( statistic );
            }

            foreach ( var item in wordsStatistic )
            {
                Console.WriteLine( item.UniqueWord + ":" + item.Count );
            }
        }
    }
}
