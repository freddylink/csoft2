using System;
using System.Net;

namespace Services.WebClientModule
{
    public class WebModule
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
