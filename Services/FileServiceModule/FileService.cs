using System;
using System.IO;

namespace Services.FileServiceModule
{
    public class FileService
    {
        public static void SaveData( string domainName, string webData )
        {
            string writePath = PathBuilder( domainName );
            try
            {
                using ( StreamWriter sw = new StreamWriter( writePath, false, System.Text.Encoding.Default ) )
                {
                    sw.WriteLine( webData );
                }
                Console.WriteLine( "Запись в файл с сайта " + domainName + " выполнена" );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
            }
        }

        private static string PathBuilder( string domainName )
        {
            string pathToDirectory = Directory.GetCurrentDirectory();
            string dateString = DateTime.Now.ToShortDateString();
            string fileName = domainName.Replace( ".", "-" ) + "-" + dateString.Replace( ".", "-" ) + ".html";

            return Path.Combine( pathToDirectory, fileName );
        }
    }
}
