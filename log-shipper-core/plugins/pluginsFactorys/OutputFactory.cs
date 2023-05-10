using LogShipperProject.log_shipper_core.plugins.output;
using LogShipperProject.log_shipper_core.plugins.pluginsFactorys;
using Serilog;

namespace LogShipperProject.pipeline
{
    public class OutputFactory : ICreatable
    {
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "stdout":
                    Logger.Debug("Stdout plugin created");
                    return new Stdout(pipelineConfiguration);
                default:
                    Logger.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}