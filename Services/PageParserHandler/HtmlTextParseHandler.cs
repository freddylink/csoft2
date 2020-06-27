using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.PageParserHandler
{
    public class HtmlTextParseHandler : IPageParserHandler
    {
        public SortedDictionary<string, int> GetUniqueWords( string webData )
        {
            /*
            string validFileAbsolutePath = Path.GetFullPath( Directory.GetCurrentDirectory() + "/test.html", Directory.GetCurrentDirectory() );
            string validContent = File.ReadAllText( validFileAbsolutePath );
            webData = validContent;
            Console.WriteLine( ASCIIEncoding.Unicode.GetByteCount( webData ) );
            Console.WriteLine("-------");
            Process proc = Process.GetCurrentProcess();
            var mem = proc.PrivateMemorySize64;
            Console.WriteLine( proc.PrivateMemorySize64 );
            //Console.WriteLine( proc.MaxWorkingSet );
            long memorySystem = (mem / (1024 * 1024));
            long memoryFile = (ASCIIEncoding.Unicode.GetByteCount( webData ) / (1024 * 1024));
            GC.GetTotalMemory( true );
            Console.WriteLine( memorySystem.ToString() );
            Console.WriteLine( memoryFile.ToString() );
            //Console.WriteLine( GC.GetTotalMemory( true ).ToString( "0,0" ) );

            if ( memoryFile > memorySystem )
            {

            }
            else
            {

            }
            */
            var textResult = SortText( webData );
            var items = textResult.Split( new[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '«', '»', '-', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries );

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
    }
}
