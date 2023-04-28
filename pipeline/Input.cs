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
            Tail tail = new Tail();
            await tail.MonitorLogFileAsync("C:\\Users\\dotan\\log-shipper-test\\log.txt");

            /*await Console.Out.WriteLineAsync("input");
            eventLog = new Event("TAG");
            await Console.Out.WriteLineAsync("ara");
            await Console.Out.WriteLineAsync(eventLog.ToString());
            foreach (var next in nextPipelines)
            {
                await Task.Run(() => next.Run(eventLog));
            }
            await Task.WhenAll();
            */
        }
    }
}
