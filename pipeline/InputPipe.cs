using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    internal class InputPipe : IRunnable
    {
        public async void Run()
        {
            await Console.Out.WriteLineAsync("or");
        }
    }
}
