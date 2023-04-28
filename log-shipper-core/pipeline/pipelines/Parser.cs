
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline.pipelines
{
    public class Parser : Pipeline
    {
        public Parser(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }

        public override async Task Run(Event eventLog)
        {
            await Console.Out.WriteLineAsync("parse");
            foreach (var next in nextPipelines)
            {
                await next.Run(eventLog);
            }
            await Task.WhenAll();
        }
    }
}
