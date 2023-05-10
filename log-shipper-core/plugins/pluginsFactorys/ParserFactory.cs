using log_shipper.log_shipper_core.plugins.parser;
using log_shipper.log_shipper_core.plugins.pluginsFactorys;
using log_shipper.plugins.input.plugins;
using Serilog;

namespace log_shipper.pipeline
{
    public class ParserFactory : ICreatable
    {
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "regex":
                    Logger.Information("Regex plugin created");
                    return new RegEx(pipelineConfiguration);
                default:
                    Logger.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}