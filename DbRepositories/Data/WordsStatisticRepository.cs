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
            var a = date.Date.AddDays( -7 );
            //берем за основу, что если в БД есть запись по сайту за последний 7 дней, то показываем базы
            return Entities
                .Where( item => item.Timestamp.Date >= date.Date.AddDays( -7 )
                     && item.SiteUrl == urlSite )
                .ToList();
        }
    }
}
