using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class LogBook
    {
        [DataMember]
        private List<LogEntry> _logEntries;

        public LogBook()
        {
            _logEntries = new List<LogEntry>();
        }

        internal string GetLog()
        {
            throw new NotImplementedException();
        }

        // Add a new log entry with the current date
        public void AddLogEntry(string description)
        {
            description = description + Environment.NewLine;
            _logEntries.Add(new LogEntry(DateTime.Now, description));
        }
    }
}
