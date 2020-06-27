using System;
using System.Linq;
using Xunit;


namespace Services.Tests
{
    public class UrlValidatorTest
    {
        [Fact]
        public void UrlValidator_TryGetValidUrl_AbsolutePathValid_Success()
        {
            string[] urlSiteArr = new string[] {
                @"http://www.examplehhtp.com",
                @"https://www.examplehttps.com",
                @"http://www.example.com/pages/book.jpg",
                @"example.com",
                @"www.example.com/pages",
                @"www.example.com/pages/",
                @"www.example.com/pages/book.jpg",
                @"www.example.com/pages/book.jpg?id=51"
            };
            Uri[] validUrls = new Uri[ urlSiteArr.Count() ];

            var i = 0;
            foreach ( string url in urlSiteArr )
            {
                validUrls[ i ] = UrlValidator.UrlValidator.TryGetValidUrl( url );
                i++;
            }
            
            Assert.DoesNotContain( null, validUrls );
        }

        [Fact]
        public void UrlValidator_TryGetValidUrl_AbsolutePathNotValid_Equal()
        {
            string[] urlSiteArr = new string[] {
                @"http//www.examplehhtp.com",
                @"https:/www.examplehttps.com",
                @"http://",
                @"//site.com",
                @"http://<>example.com",
                @"<>www.example.com/pages",
                @"w!!!ww.example.com/pages/",
                @"wwws7\\xxx.example.com/pages/book.jpg",
            };
            Uri[] validUrls = new Uri[ urlSiteArr.Count() ];

            var i = 0;
            foreach ( string url in urlSiteArr )
            {
                validUrls[ i ] = UrlValidator.UrlValidator.TryGetValidUrl( url );
                i++;
            }
            var countNull = validUrls.Count( el => el == null );
            Assert.Equal( urlSiteArr.Count(), countNull );
        }
    }
}
