using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public static class PipelineFactory
    {
        public static IPipeline CreatePipeline(string pipelineType)
        {
            switch (pipelineType.ToLower())
            {
                case "input":
                    return new Input();
                case "parser":
                    return new Parser();
                case "filter":
                    return new Filter();
                case "output":
                    return new Output();
                default:
                    Log.Error("Invalid pipeline type specified : {0}.", pipelineType.ToLower());
                    throw new ArgumentException("Invalid pipeline type specified.");
            }
        }
    }
}
