using log_shipper.log_shipper_core.plugins.filter;
using log_shipper.log_shipper_core.plugins.pluginsFactorys;
using Serilog;

namespace log_shipper.pipeline
{
    public class FilterFactory : ICreatable
    {
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "modify":
                    Logger.Information("Modify plugin created");
                    return new Modify(pipelineConfiguration);
                default:
                    Logger.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}