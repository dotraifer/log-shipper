using log_shipper;
using log_shipper.pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;
using YamlDotNet.Core;

namespace log_shipper
{
    public class PipelineExecuter
    {
        private readonly List<Pipeline> inputs = new List<Pipeline>();
        private readonly List<Pipeline> parsers = new List<Pipeline>();
        private readonly List<Pipeline> filters = new List<Pipeline>();
        private readonly List<Pipeline> outputs = new List<Pipeline>();
        public PipelineExecuter() {
            ExpandoObject
                keyValuePairs = Utils.YamlParser("C:\\Users\\dotan\\source\\repos\\log-shipper\\Conf.yaml");
            foreach (var property in keyValuePairs)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;

                Pipeline pipeline = PipelineFactory.CreatePipeline(propertyName, propertyValue);
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
            this.DeterminateChainOfPipelines();
        }
        /// <summary>
        /// 
        /// </summary>
        public void DeterminateChainOfPipelines()
        {
            DeterminaiteInputsChain();
            DeterminiteParsersChain();
            DeterminiteFiltersChain();
        }

        public void DeterminaiteInputsChain()
        {
            foreach (var input in this.inputs)
            {
                // all the inputs will always send to first parser
                input.AddNextLogger(this.parsers[0]);
            }
        }

        public void DeterminiteParsersChain()
        {
            for (int i = 0; i < this.parsers.Count - 1; i++)
            {
                parsers[i].AddNextLogger(parsers[i + 1]);
            }
            parsers[parsers.Count - 1].AddNextLogger(this.filters[0]);
        }

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
