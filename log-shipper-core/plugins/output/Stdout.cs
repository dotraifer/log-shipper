using LogShipperProject.pipeline.pipelines;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShipperProject.log_shipper_core.plugins.output
{
    public sealed class Stdout : Output
    {
        public Stdout(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            Logger.Debug("start stdout running");
            if (this.ChackMatch(eventLog))
                await Console.Out.WriteLineAsync(eventLog.ToString());
        }
    }
}
