using log_shipper.log_shipper_core.plugins.output;
using log_shipper.log_shipper_core.plugins.pluginsFactorys;
using Serilog;

namespace log_shipper.pipeline
{
    public class OutputFactory : ICreatable
    {
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration)
        {
            switch (pluginType.ToLower())
            {
                case "stdout":
                    Log.Information("Stdout plugin created");
                    return new Stdout(pipelineConfiguration);
                default:
                    Log.Error("Invalid plugin type specified : {0}.", pluginType.ToLower());
                    throw new ArgumentException("Invalid plugin type specified.");
            }
        }
    }
}