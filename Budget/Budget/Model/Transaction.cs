using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model
{
    public class Transaction
    {
        private decimal _amount;
        private DateTime _date;
        private Bin _toBin;
        private Bin _fromBin;
        private string _description;
        private string _name;

        public Transaction(decimal _amount, DateTime _date, Bin _toBin, Bin _fromBin, string _description, string _name)
        {
            this._amount = _amount;
            this._date = _date;
            this._toBin = _toBin;
            this._fromBin = _fromBin;
            this._description = _description;
            this._name = _name;
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

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        internal Bin ToBin
        {
            get
            {
                return _toBin;
            }

            set
            {
                _toBin = value;
            }
        }

        internal Bin FromBin
        {
            get
            {
                return _fromBin;
            }

            set
            {
                _fromBin = value;
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

        public override string ToString()
        {
            return "On " + _date.ToShortDateString() + ", " + _amount + " was taken from " +
                (_fromBin.ToString() ?? "nowhere") + " and put into " + (_toBin.ToString() ?? "nowhere");
        }
    }
}
