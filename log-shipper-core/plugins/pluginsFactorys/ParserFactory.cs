using LogShipperProject.log_shipper_core.plugins.parser;
using LogShipperProject.log_shipper_core.plugins.pluginsFactorys;
using LogShipperProject.plugins.input.plugins;
using Serilog;

namespace LogShipperProject.pipeline
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