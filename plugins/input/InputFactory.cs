using log_shipper.pipeline;
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
