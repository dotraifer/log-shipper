using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper.pipeline
{
    public class Input : IPipeline
    {
        public async void Run()
        {
            await Console.Out.WriteLineAsync("or");
        }
    }
}
