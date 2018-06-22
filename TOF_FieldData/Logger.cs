using System;
namespace Parsers
{
    public class Logger
    {

        private static readonly Lazy<Logger> lazy =
            new Lazy<Logger>(() => new Logger());

        public static Logger Instance { get { return lazy.Value; } }

        public Logger()
        {
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
