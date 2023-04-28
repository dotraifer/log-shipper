using log_shipper.pipeline.pipelines;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.log_shipper_core.plugins.parser
{
    public class RegEx : Parser
    {
        public RegEx(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            Log.Information("start parser running");
            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }
    }
}
