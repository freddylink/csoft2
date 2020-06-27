using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.UrlValidator
{
    public class UrlValidator
    {
        public static Uri  TryGetValidUrl( string urlSite )
        {
            Uri uriResult;

            //заменяем www на пустоту, чтобы запись в базе не отличалась www.simbirsoft.com и simbirsoft.com
            if ( urlSite.Contains("www.") )
            {
                urlSite.Replace("www.", "");
            }

            if ( !Regex.IsMatch( urlSite, @"^http(s)?(://)?", RegexOptions.IgnoreCase ) )
                urlSite = "http://" + urlSite;

            Uri.TryCreate( urlSite, UriKind.Absolute, out uriResult );

            return uriResult;
        }
        /*
        private static bool ValidateRegexUrl( string URL )
        {
            string Pattern = @"(http(s)?://)?([\w-]+\.)+[\w-]+[\w-]+[\.]+[\][a-z.]{2,3}$+([./?%&=]*)?";
            ///string Pattern = @"/^((?:https?\:)?(?:\/{2})?)?((?:[\w\d-_]{1,64})\.(?:[\w\d-_\.]{2,64}))(\:\d{2,6})?((?:\/|\?|#|&){1}(?:[\w\d\S]+)?)?$/u";
            Regex Rgx = new Regex( Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
            return Rgx.IsMatch( URL );
        }
        */
    }
}
