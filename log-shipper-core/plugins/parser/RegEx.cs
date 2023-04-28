using log_shipper.pipeline.pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.log_shipper_core.plugins.parser
{
    public class RegEx : Parser
    {
        public RegEx(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override Task Run(Event eventLog)
        {
            throw new NotImplementedException();
        }
    }
}
