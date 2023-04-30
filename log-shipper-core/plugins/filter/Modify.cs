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
            Log.Debug("start modify running");
            try
            {
                eventLog = await AddFilter(eventLog);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }

            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }

        public async Task<Event> AddFilter(Event eventLog)
        {
            foreach (var field in this.PipelineConfiguration)
            {
                if (((string)field.Key).ToLower().Equals("add"))
                {
                    Dictionary<object, object> keyValuesToAdd = (Dictionary<object, object>)field.Value;
                    foreach (var add in keyValuesToAdd)
                    {
                        eventLog.LogData.Add((string)add.Key, (string)add.Value);
                    }
                }
            }
            return eventLog;
        }


    }
}
