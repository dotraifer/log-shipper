using LogShipperProject.plugins.input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShipperProject.pipeline.pipelines
{
    /// <summary>
    /// Input pipeline object
    /// </summary>
    public abstract class Input : Pipeline
    {
        public Input(Dictionary<object,object> pipelineConfiguration) : base(pipelineConfiguration)
        {
        }

    }
}
