using System.Collections.Generic;

namespace Services.PageParserHandler
{
    public interface IPageParserHandler
    {
        SortedDictionary<string, int> GetUniqueWords( string webData );
    }
}
