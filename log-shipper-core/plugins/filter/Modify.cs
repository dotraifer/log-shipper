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
                eventLog = AddFilter(eventLog);
            }
            catch (Exception ex)
            {
                // TODO: Catch actual exceptions
                await Console.Out.WriteLineAsync(ex.ToString());
            }
            // send to all next pipliens
            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }

        /// <summary>
        /// Will call 
        /// </summary>
        /// <param name="eventLog"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Event Modify(Event eventLog)
        {
            foreach (var field in this.PipelineConfiguration)
            {
                string modifier = (string)field.Key;
                switch ((modifier.ToLower()))
                {
                    case "type":
                        break;
                    case "add":
                        eventLog = Add(eventLog, field.Value);
                        break;
                    case "set":
                        eventLog = Set(eventLog, field.Value);
                        break;
                    case "remove":
                        eventLog = Remove(eventLog, field.Value);
                        break;
                    default:
                        Log.Error("{0} is not exicted in the modify pipeline", modifier);
                        throw new Exception("To implement exception");

                }
            }
            return eventLog;
        }

        public Event Add(Event eventLog, object valuesToAdd)
        {
            // TODO : ignore if value already exists
            Dictionary<object, object> keyValuesToAdd = (Dictionary<object, object>)valuesToAdd;
            foreach (var add in keyValuesToAdd)
            {
                eventLog.LogData.Add((string)add.Key, (string)add.Value);
            }
            return eventLog;
        }

        public Event Set(Event eventLog, object valuesToSet)
        {
            Dictionary<object, object> keyValuesToSet = (Dictionary<object, object>)valuesToSet;
            foreach (var set in keyValuesToSet)
            {
                eventLog.LogData.Remove(((string)set.Key));
                eventLog.LogData.Add((string)set.Key, (string)set.Value);
            }
            return eventLog;
        }

        public Event Remove(Event eventLog, object valuesToRemove)
        {
            List<object> listValuesToRemove = (List<object>)valuesToRemove;
            foreach (var remove in listValuesToRemove)
            {
                eventLog.LogData.Remove(((string)remove));
            }
            return eventLog;
        }
    }
}
