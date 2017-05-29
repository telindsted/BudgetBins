using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class Bin
    {
        [DataMember]
        private string _name;
        [DataMember]
        private string _description;
        [DataMember]
        private decimal _upkeep;
        [DataMember]
        private decimal _currentAmount;

        public Bin(string _name, string _description, decimal _upkeep, decimal _currentAmount)
        {
            this._name = _name;
            this._description = _description;
            this._upkeep = _upkeep;
            this._currentAmount = _currentAmount;
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

        public decimal Upkeep
        {
            get
            {
                return _upkeep;
            }

            set
            {
                _upkeep = value;
            }
        }

        public decimal CurrentAmount
        {
            get
            {
                return _currentAmount;
            }

            set
            {
                _currentAmount = value;
            }
        }

        public void FillBin(decimal amountToFill)
        {
            _currentAmount += amountToFill;
        }

        public override string ToString()
        {
            return _name + ": " + _currentAmount + " [" + _upkeep + "]";
        }
    }
}
