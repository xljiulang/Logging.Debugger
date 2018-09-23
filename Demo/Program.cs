using Microsoft.Extensions.Logging;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logging = new LoggerFactory().AddDebugger();
            var logger = logging.CreateLogger(nameof(Demo));

            logger.LogInformation("log info ...");
            logger.LogError(0, new Exception("boom!"), "sorry!");

            Console.ReadLine();
        }
    }
}
