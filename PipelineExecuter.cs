using log_shipper;
using log_shipper.pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;

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
        }   
        public  void Execute() 
        {
            foreach(var input in this.inputs)
            {
                input.Run();
            }
        }
    }
}
