using LogShipperProject;
using LogShipperProject.pipeline;
using Serilog;
using System;

namespace LogShipperProject
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
