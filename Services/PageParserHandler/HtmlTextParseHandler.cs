using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Services.PageParserHandler
{
    public class HtmlTextParseHandler : IPageParserHandler
    {
        private readonly string _splitSymbols;
        private readonly string _replaceSymbols;
        public HtmlTextParseHandler( IConfiguration configuration )
        {
            _splitSymbols = configuration[ "FilterSettings:SplitSymbols" ];
            _replaceSymbols = configuration[ "FilterSettings:ReplaceSymbols" ];
        }

        public SortedDictionary<string, int> GetUniqueWords( string webData )
        {
            string textData = GetTextDataFromWebData( webData );
            string[] allWords = textData.Split( _splitSymbols.ToCharArray(), StringSplitOptions.RemoveEmptyEntries );

            SortedDictionary<string, int> uniqueWords = new SortedDictionary<string, int>();
            Char[] replaceCharArr = _replaceSymbols.ToCharArray();

            foreach ( string word in allWords )
            {
                if ( Regex.IsMatch( word, @"\d" ) )
                {
                    continue;
                }

                string wordReplaced = ReplaceSymbols( word, replaceCharArr );

                //не успел хорошо реализовать случай, когда в строке просто дефис
                if ( (wordReplaced.Length == 1 && wordReplaced == "-") || wordReplaced.Length == 0 )
                {
                    continue;
                }

                if ( uniqueWords.ContainsKey( wordReplaced ) )
                {
                    uniqueWords[ wordReplaced ]++;
                }
                else
                {
                    uniqueWords.Add( wordReplaced, 1 );
                }
            }

            return uniqueWords;
        }

        private string GetTextDataFromWebData( string webData )
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml( webData );

            List<string> textData = new List<string>();

            htmlDoc.DocumentNode.Descendants()
                .Where( n => n.Name == "script" || n.Name == "style" )
                .ToList()
                .ForEach( n => n.Remove() );

            foreach ( HtmlNode node in htmlDoc.DocumentNode.DescendantsAndSelf() )
            {
                if ( node.NodeType == HtmlNodeType.Text && node.InnerText.Trim() != "" )
                {
                    string decodeText = WebUtility.HtmlDecode( node.InnerText.Trim() );
                    textData.Add( decodeText.ToUpper() );
                }
            }

            return String.Join( " ", textData );
        }

        private string ReplaceSymbols( string word, Char[] replaceCharArr )
        {
            foreach ( var item in replaceCharArr )
            {
                word = word.Replace( item, ' ' );
            }

            return word.Trim();
        }
    }
}
