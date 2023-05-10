using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Collections;

namespace log_shipper.pipeline
{
    public abstract class Pipeline
    {
        protected List<Pipeline> NextPipelines { get; set; }
        protected Dictionary<object, object> PipelineConfiguration { get; set; }
        protected string Type;
        protected string Match;

        /// <summary>
        /// pipe constuctor
        /// </summary>
        /// <param name="pipelineConfiguration">Configuration for the pipeline</param>
        protected Pipeline(Dictionary<object, object> pipelineConfiguration)
        {
            this.PipelineConfiguration = pipelineConfiguration;
            try
            {
                this.Type = (string)PipelineConfiguration["type"];
            }
            catch (Exception ex) {
                Logger.Error("Type was not defined");
                throw;
            }
            try
            {
                this.Match = (string)PipelineConfiguration["match"];
            }
            catch (Exception ex)
            {
                this.Match = "*";
            }
            this.NextPipelines = new List<Pipeline>();
        }

        /// <summary>
        /// Add a pipeline object to the nextPipeline List
        /// </summary>
        /// <param name="nextPipeline">object of the next pipeline stops</param>
        public void AddNextPipelines(Pipeline nextPipeline)
        {
            this.NextPipelines.Add(nextPipeline);
        }

        /// <summary>
        /// Run async the plugin
        /// </summary>
        /// <param name="eventLog">The log object</param>
        public abstract Task Run(Event eventLog);

        public bool ChackMatch(Event eventLog)
        {
            string tag = eventLog.Tag;
            return eventLog.Tag.Equals(this.Match) ? true : false;
        }
    }
}
