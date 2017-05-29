using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class Bill
    {
        [DataMember]
        private DateTime _dueDate;
        [DataMember]
        private string _name;
        [DataMember]
        private string _description;
        [DataMember]
        private decimal _amount;
        [DataMember]
        private Bin _binToRemoveFrom;
        [DataMember]
        private bool _isPeriodic;
        [DataMember]
        private TimeSpan? _frequency;
        [DataMember]
        private bool _autoPay;

        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }

            set
            {
                _dueDate = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }
        public Bin BinToRemoveFrom
        {
            get
            {
                return _binToRemoveFrom;
            }

            set
            {
                _binToRemoveFrom = value;
            }
        }
        public bool IsPeriodic
        {
            get
            {
                return _isPeriodic;
            }

            set
            {
                _isPeriodic = value;
            }
        }
        public TimeSpan? Frequency
        {
            get
            {
                return _frequency;
            }

            set
            {
                _frequency = value;
            }
        }
        public bool AutoPay
        {
            get
            {
                return _autoPay;
            }

            set
            {
                _autoPay = value;
            }
        }

        public Bill(string name, DateTime dueDate, string description, decimal amount,
            Bin binToRemoveFrom)
        {
            _name = name;
            _dueDate = dueDate;
            _description = description;
            _amount = amount;
            _binToRemoveFrom = binToRemoveFrom;

            _isPeriodic = false;
            _frequency = null;
            _autoPay = false;
        }

        public Bill(string name, DateTime dueDate, string description, decimal amount,
    Bin binToRemoveFrom, bool isPeriodic, TimeSpan? frequency, bool autoPay)
        {
            _name = name;
            _dueDate = dueDate;
            _description = description;
            _amount = amount;
            _binToRemoveFrom = binToRemoveFrom;

            _isPeriodic = isPeriodic;
            _frequency = frequency;
            _autoPay = autoPay;
        }

        public void PayBill()
        {
            _binToRemoveFrom.CurrentAmount -= _amount;
        }

        public override string ToString()
        {
            return DueDate.ToShortDateString() + ": " + Name + " for $" + Amount.ToString();
        }



    }
}
