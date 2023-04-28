using log_shipper;
using log_shipper.pipeline;
using Serilog;
using System;

namespace LogShipper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().
                WriteTo.
                Console()
            .CreateLogger();
            Log.Information("log shipper started");

            Event eve = new Event("fgf");

            log_shipper.LogShipper pe = new log_shipper.LogShipper();
            pe.Start();


        }
    }
}
