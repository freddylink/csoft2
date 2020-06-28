using Microsoft.Extensions.Configuration;
using Services.PageParserHandler;
using System;
using System.IO;
using Xunit;

namespace Services.Tests
{
    public class HtmlTextParseHandlerTest : IClassFixture<IConfigurationFixture>
    {
        private IPageParserHandler _pageParseHandler;
        private IConfiguration _configuration;
        public HtmlTextParseHandlerTest( IConfigurationFixture fixture )
        {
            _configuration = fixture.Configuration;
            _pageParseHandler = new HtmlTextParseHandler( _configuration );
        }

        [Fact]
        public void HtmlTextParseHandler_GetUniqueWords_EqualsTestData()
        {
            string absolutFilePath = Path.GetFullPath( "..\\..\\..\\TestData\\HtmlData\\simbirsoft.html", Directory.GetCurrentDirectory() );
            var contentHtml = File.ReadAllText( absolutFilePath );
            var result = _pageParseHandler.GetUniqueWords( contentHtml );
            Assert.Equal( 677, result.Count );
        }


        [Fact]
        public void HtmlTextParseHandler_GetUniqueWords_Test_EqualsTestData()
        {
            string absolutFilePath = Path.GetFullPath( "..\\..\\..\\TestData\\HtmlData\\test.html", Directory.GetCurrentDirectory() );
            var contentHtml = File.ReadAllText( absolutFilePath );
            var result = _pageParseHandler.GetUniqueWords( contentHtml );
            Assert.Equal( 52, result.Count );
        }
    }
}
