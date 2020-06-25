using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Services.PageParserHandler
{
    public class HtmlTextParseHandler : IPageParserHandler
    {
        public SortedDictionary<string, int> GetUniqueWords( string webData )
        {
            var textResult = SortText( webData );
            var items = textResult.Split( new[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '«', '»', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries );

            SortedDictionary<string, int> dict = new SortedDictionary<string, int>();
            foreach ( var item in items )
            {

                if ( Regex.IsMatch( item, @"\d" ) )
                {
                    continue;
                }

                if ( dict.ContainsKey( item ) )
                {
                    dict[ item ]++;
                }
                else
                {
                    dict.Add( item, 1 );
                }

            }

            return dict;
        }

        private string SortText( string webData )
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml( webData );

            var textData = new List<string>();

            htmlDoc.DocumentNode.Descendants()
                .Where( n => n.Name == "script" || n.Name == "style" )
                .ToList()
                .ForEach( n => n.Remove() );

            foreach ( var node in htmlDoc.DocumentNode.DescendantsAndSelf() )
            {
                if ( node.NodeType == HtmlNodeType.Text )
                {
                    if ( node.InnerText.Trim() != "" )
                    {
                        var decodeText = WebUtility.HtmlDecode( node.InnerText.Trim() );
                        textData.Add( decodeText.ToUpper() );
                    }
                }
            }

            return String.Join( " ", textData );
        }
    }
}
