using System;
using System.IO;

namespace Services.FileServiceModule
{
    public class FileService
    {
        public static void SaveData( string domainName, string htmlData )
        {
            string writePath = PathBuilder( domainName );

            Console.WriteLine( writePath );
            try
            {
                using ( StreamWriter sw = new StreamWriter( writePath, false, System.Text.Encoding.Default ) )
                {
                    sw.WriteLine( htmlData );
                }
                Console.WriteLine( "Запись выполнена" );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
            }
        }

        private static string PathBuilder( string domainName )
        {
            string path = Directory.GetCurrentDirectory();
            string dateString = DateTime.Now.ToShortDateString();
            //добавить дату в строку сохранения
            return path + "\\" + domainName.Replace( ".", "-" ) + "-" + dateString.Replace( ".", "-" ) + ".html";
        }
    }
}
