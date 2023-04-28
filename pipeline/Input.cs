using log_shipper.plugins.input;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class Input : Pipeline
    {
        public Input(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }

        public override async Task Run(Event eventLog)
        {
            //await Console.Out.WriteLineAsync(this.PipelineConfiguration);
            Tail tail = new Tail();
            await tail.MonitorLogFileAsync("C:\\Users\\dotan\\log-shipper-test\\log.txt");
        }
    }
}
