using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_shipper
{
    public class Event
    {
        private DateTime Date { get; set; }
        private string Tag { get; set; }
        public Event(string tag)
        {
            this.Tag = tag;
            this.Date = System.DateTime.Now;
        }
    }
}
