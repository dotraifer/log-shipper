using log_shipper.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.log_shipper_core.plugins.pluginsFactorys
{
    public interface ICreatable
    {
        public Pipeline Create(string pluginType, Dictionary<object, object> pipelineConfiguration);
    }
}
