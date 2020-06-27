using Services.PageParserHandler;
using System;
using System.IO;
using Xunit;

namespace Services.Tests
{
    public class HtmlTextParseHandlerTest
    {
        private IPageParserHandler _pageParseHandler;
        public HtmlTextParseHandlerTest( IPageParserHandler pageParseHandler )
        {
            _pageParseHandler = pageParseHandler;
        }

        [Fact]
        public void HtmlTextParseHandler_GetUniqueWords_EqualsTestData()
        {
            string absolutFilePath = Path.GetFullPath( "..\\..\\..\\TestData\\NationalBankKz\\invalid_bank_response.xml", Directory.GetCurrentDirectory() );
            var contentHtml = File.ReadAllText( absolutFilePath );
            var result = _pageParseHandler.GetUniqueWords( contentHtml );
            Assert.Equal( 39, result.Count );
        }
    }
}
