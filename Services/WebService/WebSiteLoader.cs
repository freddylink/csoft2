using System;
using System.Net;

namespace Services.WebClientModule
{
    public class WebSiteLoader
    {
        public static string DownloadData( Uri url )
        {
            using ( WebClient client = new WebClient() )
            {
                return client.DownloadString( url );
            }
        }
    }
}
