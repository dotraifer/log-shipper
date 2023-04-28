using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper
{
    /// <summary>
    /// Object reprasanting an event message
    /// </summary>
    public class Event
    {
        private DateTime Date { get; set; }
        private string Tag { get; set; }
        public readonly Dictionary<string, object> LogData = new Dictionary<string, object>();
        public Event(string tag)
        {
            this.Tag = tag;
            this.Date = System.DateTime.Now;
        }

        
    }
}
