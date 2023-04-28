
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class Parser : Pipeline
    {
        public Parser(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
