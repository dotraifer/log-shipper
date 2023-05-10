using LogShipperProject.pipeline.pipelines;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShipperProject.log_shipper_core.plugins.filter
{
    public class Modify : Filter
    {
        public Modify(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override async Task Run(Event eventLog)
        {
            Logger.Debug("start modify running");
            try
            {
                eventLog = ModifyLog(eventLog);
            }
            catch (Exception ex)
            {
                // TODO: Catch actual exceptions
                Logger.Error("Log modification failed");
                throw;
            }
            // send to all next pipliens
            foreach (var pipeline in this.NextPipelines)
            {
                await pipeline.Run(eventLog);
            }
        }

        /// <summary>
        /// Will call the modify functions according to the configuration
        /// </summary>
        /// <param name="eventLog">the log to modify</param>
        /// <returns>the log after modification</returns>
        /// <exception cref="Exception">if an unknown modify labal configured</exception>
        public Event ModifyLog(Event eventLog)
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

        /// <summary>
        /// Implements the add label, will add a new key: value if he isn't already exist 
        /// </summary>
        /// <param name="eventLog">the log to add to</param>
        /// <param name="valuesToAdd">dict of values to add if the keyToAdd: valueToAdd format</param>
        /// <returns>the modified log</returns>
        public static Event Add(Event eventLog, object valuesToAdd)
        {
            // TODO : ignore if value already exists
            Dictionary<object, object> keyValuesToAdd = (Dictionary<object, object>)valuesToAdd;
            foreach (var add in keyValuesToAdd)
            {
                eventLog.LogData.Add((string)add.Key, (string)add.Value);
            }
            return eventLog;
        }

        /// <summary>
        /// Implements the set label, will set a new key: value if he isn't already exist. 
        /// if exist he'll override it
        /// </summary>
        /// <param name="eventLog">the log to add to</param>
        /// <param name="valuesToAdd">dict of values to add if the keyToAdd: valueToAdd format</param>
        /// <returns>the modified log</returns>
        public static Event Set(Event eventLog, object valuesToSet)
        {
            Dictionary<object, object> keyValuesToSet = (Dictionary<object, object>)valuesToSet;
            foreach (var set in keyValuesToSet)
            {
                if (eventLog.LogData.ContainsKey((string)set.Key))
                    eventLog.LogData[(string)set.Key] = (string)set.Value;
                else
                    eventLog.LogData.Add((string)set.Key, (string)set.Value);

            }
            return eventLog;
        }

        /// <summary>
        /// Implements the remove label, will remove a key
        /// </summary>
        /// <param name="eventLog">the log to remove key from</param>
        /// <param name="valuesToAdd">dict of keys to remove</param>
        /// <returns>the modified log</returns>
        public static Event Remove(Event eventLog, object valuesToRemove)
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
