using LogShipperProject.log_shipper_core.plugins.pluginsFactorys;
using LogShipperProject.pipeline;
using LogShipperProject.plugins.input.plugins;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShipperProject.plugins.input
{
    public class InputFactory : ICreatable
    {
        /// <summary>
        /// Factory method for crating an input plugin
        /// </summary>
        /// <param name="pluginType">the plugin type</param>
        /// <example>tail</example>
        /// <param name="pipelineConfiguration">Dict of the input pipeline configuration</param>
        /// <returns>IInputable object</returns>
        /// <exception cref="ArgumentException">If the type is not familier</exception>
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "tail":
                    Logger.Information("Tail plugin created");
                    return new Tail(pipelineConfiguration);
                default:
                    Logger.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}
