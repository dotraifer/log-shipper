using log_shipper.pipeline;
using log_shipper.plugins.input.plugins;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.plugins.input
{
    public class InputFactory
    {
        /// <summary>
        /// Factory method for crating an input plugin
        /// </summary>
        /// <param name="pluginType">the plugin type</param>
        /// <example>tail</example>
        /// <param name="pipelineConfiguration">Dict of the input pipeline configuration</param>
        /// <returns>IInputable object</returns>
        /// <exception cref="ArgumentException">If the type is not familier</exception>
        public static IInputable CreateInput(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "tail":
                    Log.Information("Tail plugin created");
                    return new Tail((string)pipelineConfiguration["path"]);
                default:
                    Log.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}
