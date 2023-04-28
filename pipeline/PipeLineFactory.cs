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
        public static Pipeline CreatePipeline(string pipelineType, Object pipelineConfiguration)
        {
            switch (pipelineType.ToLower())
            {
                case "input":
                    return new Input(pipelineConfiguration);
                case "parser":
                    return new Parser(pipelineConfiguration);
                case "filter":
                    return new Filter(pipelineConfiguration);
                case "output":
                    return new Output(pipelineConfiguration);
                default:
                    Log.Error("Invalid pipeline type specified : {0}.", pipelineType.ToLower());
                    throw new ArgumentException("Invalid pipeline type specified.");
            }
        }
    }
}
