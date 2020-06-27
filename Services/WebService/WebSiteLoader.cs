using System;
using System.Net;
using System.Threading.Tasks;

namespace Services.WebClientModule
{
    public class WebSiteLoader
    {
        public static async Task<string> DownloadData( Uri url )
        {
            using ( WebClient client = new WebClient() )
            {
                return await client.DownloadStringTaskAsync( url );
            }
        }
    }
}
