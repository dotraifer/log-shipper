﻿
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShipperProject.pipeline.pipelines
{
    public abstract class Parser : Pipeline
    {
        public Parser(Dictionary<object, object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
    }
}
