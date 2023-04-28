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
            Utils.YamlParser("C:\\Users\\dotan\\source\\repos\\log-shipper\\Conf.yaml");

            IPipeline pipeline = PipelineFactory.CreatePipeline("InPut");
        }
    }
}
