using log_shipper.pipeline.pipelines;
using log_shipper.plugins.input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class PipelineFactory
    {
        /// <summary>
        /// create pipeline object method
        /// </summary>
        /// <param name="pipelineType">the pipeline type(Input, Parser, Filter, Output)</param>
        /// <param name="pipelineConfiguration">the pipeline object configuration</param>
        /// <returns>pipeline object</returns>
        /// <exception cref="ArgumentException">if the pipeline type specified is unfamillier</exception>
        public Pipeline Create(string pipelineType, object pipelineConfiguration)
        {
            string pluginType;
            List<Object> pipelineList = (List<Object>)pipelineConfiguration;
            foreach (var pipeline in pipelineList)
            {
                Dictionary<object, object> properties = (Dictionary<object, object>)pipeline;
                try
                {
                    pluginType = (string)properties["type"];
                }
                catch
                {
                    Logger.Error("Plugin wasn't noted");
                    throw new Exception("Plugin wasn't noted");
                }
                switch (pipelineType.ToLower())
                {
                    case "input":
                        InputFactory inputFactory = new InputFactory();
                        return inputFactory.Create(pluginType, properties);
                    case "parser":
                        ParserFactory parserFactory = new ParserFactory();
                        return parserFactory.Create(pluginType, properties);
                    case "filter":
                        FilterFactory filterFactory = new FilterFactory();
                        return filterFactory.Create(pluginType, properties);
                    case "output":
                        OutputFactory outputFactory = new OutputFactory();
                        return outputFactory.Create(pluginType, properties);
                    default:
                        Logger.Error("Invalid pipeline type specified : {0}.", pipelineType.ToLower());
                        throw new ArgumentException("Invalid pipeline type specified.");
                }
            }
            throw new Exception(String.Format("{0} pipeline didn't contain a list object", pipelineType));
        }
    }
}
