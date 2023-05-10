using LogShipperProject;
using LogShipperProject.pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Xml.Linq;
using YamlDotNet.Core;

namespace LogShipperProject
{
    /// <summary>
    /// The log shipper object
    /// </summary>
    public class LogShipper
    {
        private readonly List<Pipeline> Inputs = new List<Pipeline>();
        private readonly List<Pipeline> Parsers = new List<Pipeline>();
        private readonly List<Pipeline> Filters = new List<Pipeline>();
        private readonly List<Pipeline> Outputs = new List<Pipeline>();
        public LogShipper() {
            ExpandoObject
                keyValuePairs = Utils.YamlParser("C:\\Users\\dotan\\source\\repos\\log-shipper\\configuration\\Conf.yaml");
            foreach (var property in keyValuePairs)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;

                PipelineFactory pipelineFactory = new PipelineFactory();

                Pipeline pipeline = pipelineFactory.Create(propertyName, propertyValue);
                this.InitPipelines(propertyName, pipeline);
            }
            this.DeterminateChainOfPipelines();
        }

        /// <summary>
        /// Start the log shipper pipeline
        /// </summary>
        public void Start()
        {
            foreach(var input in this.Inputs)
            {
                Task.Run(() => input.Run(eventLog: null));
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
                    this.Inputs.Add(pipeline);
                    break;
                case "parser":
                    this.Parsers.Add(pipeline);
                    break;
                case "filter":
                    this.Filters.Add(pipeline);
                    break;
                case "output":
                    this.Outputs.Add(pipeline);
                    break;
            }
        }
        /// <summary>
        /// Determinate the logical order of the pipeline with Pipeline object property
        /// </summary>
        public void DeterminateChainOfPipelines()
        {
            Logger.Debug("Pipeline chain determinate stated");
            DeterminaiteInputsChain();
            DeterminiteParsersChain();
            DeterminiteFiltersChain();
            Logger.Debug("Pipeline chain determinate ended succussefully");
        }

        /// <summary>
        /// determinaite the inputs chain
        /// </summary>
        public void DeterminaiteInputsChain()
        {
            foreach (var input in this.Inputs)
            {
                // all the inputs will always send to first parser
                input.AddNextPipelines(this.Parsers[0]);
            }
        }
        /// <summary>
        /// determinaite the parsers chain
        /// </summary>
        public void DeterminiteParsersChain()
        {
            for (int i = 0; i < this.Parsers.Count - 1; i++)
            {
                Parsers[i].AddNextPipelines(Parsers[i + 1]);
            }
            Parsers[Parsers.Count - 1].AddNextPipelines(this.Filters[0]);
        }
        /// <summary>
        /// determinaite the filters chain
        /// </summary>
        public void DeterminiteFiltersChain()
        {
            for (int i = 0; i < this.Filters.Count - 1; i++)
            {
                Filters[i].AddNextPipelines(Filters[i + 1]);
            }

            foreach (var output in this.Outputs)
            {
                Filters[Filters.Count - 1].AddNextPipelines(output);
            }
        }
    }
}
