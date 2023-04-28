using log_shipper.pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.plugins.input.plugins
{
    public class Tail : IInputable
    {
        protected string path;
        public Tail(string path)
        {
            this.path = path;
        }

        public async Task Run()
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
