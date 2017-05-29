using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class LogEntry : IComparable<LogEntry>
    {
        [DataMember]
        private DateTime _date;
        [DataMember]
        private string _description;

        public DateTime Date { get { return _date; } }

        public LogEntry(DateTime date, string description)
        {
            _date = date;
            _description = description;
        }

        public int CompareTo(LogEntry other)
        {
                if (_date < other._date)
                    return 1;
                if (_date > other._date)
                    return -1;
                return 0;
        }

        public override string ToString()
        {
            return _date.ToShortDateString() + ": " + _description;
        }

    }
}
