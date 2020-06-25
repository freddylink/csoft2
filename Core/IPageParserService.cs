using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IPageParserService
    {
        void GetPageStatistics( Uri url );
    }
}
