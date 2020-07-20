using System;

namespace DbRepositories.Data.Object
{
    public class WordsStatistic
    {
        public int WordsStatisticId { get; set; }
        public string SiteUrl { get; set; }
        public string UniqueWord { get; set; }
        public int Count { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
