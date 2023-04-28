
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.plugins.input
{
    public class Tail
    {
        protected string path;
        public Tail(string path)
        {
           this.path = path;
        }
        public async Task MonitorLogFileAsync()
        {
            long lastPosition = 0;

            using (var fileStream = new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                while (true)
                {
                    fileStream.Seek(lastPosition, SeekOrigin.Begin);

                    while (!streamReader.EndOfStream)
                    {
                        var line = await streamReader.ReadLineAsync();
                        if (line != null)
                        {
                            await Console.Out.WriteLineAsync(line);
                        }
                    }

                    lastPosition = fileStream.Position;
                    await Task.Delay(1000); // Wait for new lines to be added
                }
            }
        }
    }
}
