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
        protected string type;
        /// <summary>
        /// Add a pipeline object to the nextPipeline List
        /// </summary>
        /// <param name="nextPipeline">object of the next pipeline stops</param>
        public void AddNextLogger(Pipeline nextPipeline)
        {
            this.NextPipelines.Add(nextPipeline);
        }
        /// <summary>
        /// pipe constuctor
        /// </summary>
        /// <param name="pipelineConfiguration">Configuration for the pipeline</param>
        protected Pipeline(Dictionary<object, object> pipelineConfiguration)
        {
            this.PipelineConfiguration = pipelineConfiguration;
            this.type = (string)PipelineConfiguration["type"];
            this.NextPipelines = new List<Pipeline>();
        }

        /// <summary>
        /// Run async the plugin
        /// </summary>
        /// <param name="eventLog">The log object</param>
        public abstract Task Run(Event eventLog);
    }
}
