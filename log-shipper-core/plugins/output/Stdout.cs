﻿using log_shipper.pipeline.pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.log_shipper_core.plugins.output
{
    public class Stdout : Output
    {
        public Stdout(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        public override Task Run(Event eventLog)
        {
            throw new NotImplementedException();
        }
    }
}