using log_shipper.pipeline.pipelines;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.log_shipper_core.plugins.filter
{
    public class Modify : Filter
    {
        public Modify(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            Log.Information("start modify running");
            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }
    }
}
