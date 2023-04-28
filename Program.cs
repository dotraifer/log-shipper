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

            PipelineExecuter pe = new PipelineExecuter();
            pe.Execute();
        }
    }
}
