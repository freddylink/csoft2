using System;
using System.Text.RegularExpressions;

namespace Services.UrlValidator
{
    public class UrlValidator
    {
        public static Uri TryGetValidUrl( string urlSite )
        {
            Uri uriResult;

            //заменяем www на пустоту, чтобы запись в базе не отличалась www.simbirsoft.com и simbirsoft.com
            if ( urlSite.Contains( "www." ) )
            {
                urlSite.Replace( "www.", "" );
            }

            if ( !Regex.IsMatch( urlSite, @"^http(s)?(://)?", RegexOptions.IgnoreCase ) )
                urlSite = "http://" + urlSite;

            Uri.TryCreate( urlSite, UriKind.Absolute, out uriResult );

            return uriResult;
        }
    }
}
