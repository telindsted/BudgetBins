using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class LogEntry
    {
        [DataMember]
        private DateTime _date;
        [DataMember]
        private string _description;

        public LogEntry(DateTime date, string description)
        {
            _date = date;
            _description = description;
        }

    }
}
