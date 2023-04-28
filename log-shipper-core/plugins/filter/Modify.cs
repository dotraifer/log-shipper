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
            eventLog = await AddFilter(eventLog);
            eventLog.LogData.Add("app", "test");

            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }

        public async Task<Event> AddFilter(Event eventLog)
        {
            string key = string.Empty;
            string value = string.Empty;
            foreach(var field in this.PipelineConfiguration)
            {
                if (field.Key.ToString().ToLower().Equals("add"))
                {
                    string[] words = field.Value.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    // Split the string by space delimiter, and remove any empty substrings

                    if (words.Length >= 2)
                    {
                        key = words[0];
                        value = words[1];
                        eventLog.LogData.Add(key, value);
                    }
                }
            }
            return eventLog;
        }


    }
}
