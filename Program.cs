using Serilog;
using System;

namespace LogShipper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console()
            .CreateLogger();
            Log.Information("log shipper started");
        }
    }
}
