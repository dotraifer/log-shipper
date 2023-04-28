
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
        public async Task MonitorLogFileAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                while (true)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (line != null)
                    {
                        // Process the log line asynchronously
                    }
                    else
                    {
                        await Task.Delay(1000); // Wait for new lines to be added
                    }
                }
            }
        }
    }
}
