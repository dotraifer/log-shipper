using log_shipper.plugins.input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline.pipelines
{
    /// <summary>
    /// Input pipeline object
    /// </summary>
    public class Input : Pipeline
    {
        public Input(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }
        /// <summary>
        /// Run async the Input plugin
        /// </summary>
        /// <param name="eventLog">The log object</param>
        public override async Task Run(Event eventLog)
        {
            IInputable plugin = InputFactory.CreateInput(type, PipelineConfiguration);
            Log.Information("input plugin created - start running...");
            await plugin.Run();
        }
    }
}
