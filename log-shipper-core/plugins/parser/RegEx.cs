using log_shipper.pipeline.pipelines;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace log_shipper.log_shipper_core.plugins.parser
{
    public class RegEx : Parser
    {
        public RegEx(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            try
            {
                eventLog = RegexParser(eventLog);
            }
            catch (Exception ex)
            {
                Log.Error("RegexParserFailed");
            }
            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }

        private Event RegexParser(Event eventLog)
        {
            string reg;
            try
            {
                reg = ((string)this.PipelineConfiguration["regex"]);
            }
            catch (Exception ex)
            {
                Log.Error("regex label was not found in regex parser");
                throw new Exception("to implement");
            }
            try
            {
                Regex rx = new Regex(reg);
                string log = (string)eventLog.LogData["Log"];
                MatchCollection matches = rx.Matches(log);
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    foreach (Group group in groups)
                    {
                        if (group.Name != "0")
                            eventLog.LogData.Add(group.Name, group.Value);
                    }
                }
                eventLog.LogData.Remove("Log");

            } 
            catch (Exception ex) 
            {
                Log.Error("Regex failed");
                throw new Exception();
            }
            return eventLog;
        }
    }
}
