﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace log_shipper.pipeline
{
    public abstract class Pipeline
    {
        protected List<Pipeline> nextPipelines { get; set; }
        protected Object PipelineConfiguration { get; set; }
        protected Pipeline(Object pipelineConfiguration) 
        {
            this.PipelineConfiguration = pipelineConfiguration;
        }

        public abstract void Run();
    }
}
