using log_shipper;
using log_shipper.pipeline;
using Serilog;
using System;

namespace log_shipper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().
                WriteTo.
                Console()
            .CreateLogger();
            Logger.Information("log shipper started");

            LogShipper logShipper = new LogShipper();
            logShipper.Start();


        }
    }
}
