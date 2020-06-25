using System;

namespace DbRepositories.Data.Object
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
