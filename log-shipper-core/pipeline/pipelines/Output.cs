﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline.pipelines
{
    public abstract class Output : Pipeline
    {
        public Output(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
    }
}
