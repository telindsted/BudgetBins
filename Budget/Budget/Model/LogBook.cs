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

        [DataMember]
        private int _MaxEntries = 50;

        public LogBook()
        {
            _logEntries = new List<LogEntry>();
        }

        internal string GetLog()
        {
            _logEntries.Sort();
            StringBuilder logBuilder = new StringBuilder();
            foreach (LogEntry entry in _logEntries)
            {
                logBuilder.Append(entry.ToString());
            }
            return logBuilder.ToString();
        }


        // Add a new log entry with the current date
        public void AddLogEntry(string description)
        {
            _logEntries.Sort();
            while (_logEntries.Count >= _MaxEntries)
            {
                _logEntries.RemoveAt(_logEntries.Count - 1);
            }
            description = description + Environment.NewLine;
            _logEntries.Add(new LogEntry(DateTime.Now, description));
        }
    }
}
