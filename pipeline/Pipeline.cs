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
        protected List<Pipeline> nextPipelines { get; set; }
        protected Dictionary<object, object> PipelineConfiguration { get; set; }
        protected string type;
        public void AddNextLogger(Pipeline nextPipeline)
        {
            this.nextPipelines.Add(nextPipeline);
        }
        protected Pipeline(object pipelineConfiguration)
        {
            this.PipelineConfiguration = (Dictionary<object, object>)pipelineConfiguration;
            this.type = (string)PipelineConfiguration["type"];
            this.nextPipelines = new List<Pipeline>();
        }



        public abstract Task Run(Event eventLog);
    }
}
