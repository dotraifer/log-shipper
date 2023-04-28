using log_shipper.pipeline.pipelines;
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
        /// <summary>
        /// create pipeline object method
        /// </summary>
        /// <param name="pipelineType">the pipeline type(Input, Parser, Filter, Output)</param>
        /// <param name="pipelineConfiguration">the pipeline object configuration</param>
        /// <returns>pipeline object</returns>
        /// <exception cref="ArgumentException">if the pipeline type specified is unfamillier</exception>
        public static Pipeline CreatePipeline(string pipelineType, object pipelineConfiguration)
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
