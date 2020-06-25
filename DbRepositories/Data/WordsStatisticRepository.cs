using DbRepositories.Data.Object;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbRepositories.Data
{
    public class WordsStatisticRepository : Repository<WordsStatistic>
    {
        public WordsStatisticRepository( PageParserDbContext context )
            : base( context )
        { }

        public List<WordsStatistic> GetWordItems( string urlSite, DateTime date )
        {
            return Entities
                .Where( item => item.Timestamp.Date == date.Date
                     && ( item.SiteUrl == urlSite ))
                .ToList();
        }
    }
}
