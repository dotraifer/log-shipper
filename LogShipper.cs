using log_shipper;
using log_shipper.pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Xml.Linq;
using YamlDotNet.Core;

namespace log_shipper
{
    /// <summary>
    /// The log shipper object
    /// </summary>
    public class LogShipper
    {
        private readonly List<Pipeline> inputs = new List<Pipeline>();
        private readonly List<Pipeline> parsers = new List<Pipeline>();
        private readonly List<Pipeline> filters = new List<Pipeline>();
        private readonly List<Pipeline> outputs = new List<Pipeline>();
        public LogShipper() {
            ExpandoObject
                keyValuePairs = Utils.YamlParser("C:\\Users\\dotan\\source\\repos\\log-shipper\\Conf.yaml");
            foreach (var property in keyValuePairs)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;

                Pipeline pipeline = PipelineFactory.CreatePipeline(propertyName, propertyValue);
                this.InitPipelines(propertyName, pipeline);
            }
            this.DeterminateChainOfPipelines();
        }

        /// <summary>
        /// Start the log shipper pipeline
        /// </summary>
        public void Start()
        {
            foreach(var input in this.inputs)
            {
                Task.Run(() => input.Run(null));
                Console.ReadLine();
            }
        }

        /// <summary>
        /// abstraction of dividing the pipelines to their lists
        /// </summary>
        /// <param name="propertyName">the pipeline property name</param>
        /// <param name="pipeline">the pipeline object to add</param>
        public void InitPipelines(string propertyName, Pipeline pipeline)
        {
            switch (propertyName)
            {
                case "input":
                    this.inputs.Add(pipeline);
                    break;
                case "parser":
                    this.parsers.Add(pipeline);
                    break;
                case "filter":
                    this.filters.Add(pipeline);
                    break;
                case "output":
                    this.outputs.Add(pipeline);
                    break;
            }
        }
        /// <summary>
        /// Determinate the logical order of the pipeline with Pipeline object property
        /// </summary>
        public void DeterminateChainOfPipelines()
        {
            Log.Debug("Pipeline chain determinate stated");
            DeterminaiteInputsChain();
            DeterminiteParsersChain();
            DeterminiteFiltersChain();
            Log.Debug("Pipeline chain determinate ended succussefully");
        }

        /// <summary>
        /// determinaite the inputs chain
        /// </summary>
        public void DeterminaiteInputsChain()
        {
            foreach (var input in this.inputs)
            {
                // all the inputs will always send to first parser
                input.AddNextLogger(this.parsers[0]);
            }
        }
        /// <summary>
        /// determinaite the parsers chain
        /// </summary>
        public void DeterminiteParsersChain()
        {
            for (int i = 0; i < this.parsers.Count - 1; i++)
            {
                parsers[i].AddNextLogger(parsers[i + 1]);
            }
            parsers[parsers.Count - 1].AddNextLogger(this.filters[0]);
        }
        /// <summary>
        /// determinaite the filters chain
        /// </summary>
        public void DeterminiteFiltersChain()
        {
            for (int i = 0; i < this.filters.Count - 1; i++)
            {
                filters[i].AddNextLogger(filters[i + 1]);
            }

            foreach (var output in this.outputs)
            {
                filters[filters.Count - 1].AddNextLogger(output);
            }
        }
    }
}
