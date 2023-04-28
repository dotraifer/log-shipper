using log_shipper.plugins.input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class Input : Pipeline
    {
        public Input(object pipelineConfiguration) : base(pipelineConfiguration)
        {
        }

        public override async Task Run(Event eventLog)
        {
            //await Console.Out.WriteLineAsync(this.PipelineConfiguration);
            IInputable plugin = InputFactory.CreateInput(this.type, this.PipelineConfiguration);
            Log.Information("input plugin created - start running...");
            await plugin.Run();
        }
    }
}
