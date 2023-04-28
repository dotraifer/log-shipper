using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class Filter : Pipeline
    {
        public Filter(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            await Console.Out.WriteLineAsync("filter");
            foreach (var next in nextPipelines)
            {
                await next.Run(eventLog);
            }
            await Task.WhenAll();
        }
    }
}
