using log_shipper.pipeline;
using log_shipper.pipeline.pipelines;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.plugins.input.plugins
{
    public class Tail : Input
    {
        protected string path;
        public Tail(Dictionary<object,object> pipelineConfiguration) : base(pipelineConfiguration)
        {
            this.path = (string)pipelineConfiguration["path"];
        }

        public override async Task Run(Event logEvent)
        {
            await MonitorLogFileAsync();

        }
        public async Task MonitorLogFileAsync()
        {
            long lastPosition = 0;

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                Log.Information("started reading from {0} ", path);
                while (true)
                {
                    fileStream.Seek(lastPosition, SeekOrigin.Begin);

                    while (!streamReader.EndOfStream)
                    {
                        var line = await streamReader.ReadLineAsync();
                        if (line != null)
                        {
                            Event logEvent = new Event((string)this.PipelineConfiguration["tag"]);
                            logEvent.LogData.Add("Log", (string)line);
                            Log.Debug("log sent to next pipeline");
                            foreach (var pipeline in this.NextPipelines)
                            {
                                try { 
                                    await pipeline.Run(logEvent); 
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("failed to send to parser");
                                }
  
                            }
                            Log.Debug(line + " parsed success");
                            
                        }
                    }

                    lastPosition = fileStream.Position;
                    await Task.Delay(1000); // Wait for new lines to be added
                }
            }
        }
    }
}
