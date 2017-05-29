using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Budget.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string _name;
        [DataMember]
        private List<Bin> _binList;
        [DataMember]
        private List<Transaction> _transactionList;
        [DataMember]
        private List<Bill> _billList;
        [DataMember]
        private LogBook _log;
        [DataMember]
        private decimal _balance;

        public DateTime? LastUpkeep { get; set; }
        public DateTime? LastUpdate { get; set; }
        public List<Bin> BinList { get { return _binList; } }
        public List<Bill> BillList { get { return _billList; } }
        public decimal Balance { get { return _balance; } }
        public string Name { get { return _name; } }

        public User(string name)
        {
            _name = name;

            _binList = new List<Bin>();
            _transactionList = new List<Transaction>();
            _billList = new List<Bill>();

            _log = new LogBook();
            _balance = 0.0M;

            LastUpdate = null;
            LastUpkeep = null;

        }

        // https://msdn.microsoft.com/en-us/library/system.xml.serialization.xmlserializer(v=vs.110).aspx
        // Look at headfirst book for xml serialization help

        // Fill cost bins from total income

        // Handle dynamic bins here for now
        public void Upkeep()
        {
            // Fill all the cost bins
            // Subtract from the balance
            foreach(Bin bin in _binList)
            {
                    FillBin(bin, bin.Upkeep);
                    _balance -= bin.Upkeep;
            }
            LastUpkeep = DateTime.Now;
            SaveUser();

        }
        public void AddIncome(decimal amount)
        {
            _balance += amount;
            UpdateLog("Added " + amount + " to balance");
            SaveUser();
        }
        // Returns the upkeep cost
        public decimal GetCost()
        {
            decimal result = 0.0M;

            foreach (Bin bin in _binList)
            {
                result += bin.Upkeep;
            }

            return result;
        }
        // Returns a string representation of the log
        // length is controlled within the logbook
        public string GetLog()
        {
            return _log.GetLog();
        }
        // Need method to update log
        // and save (serialize)
        public void UpdateLog(string description)
        {
            LastUpdate = DateTime.Now;
            _log.AddLogEntry(description);
        }
        public void SaveUser()
        {
            //throw new NotImplementedException();
        }

        // Does not save user data
        public void FillBin(Bin binToFill, decimal amountToAdd)
        {
            binToFill.FillBin(amountToAdd);
            UpdateLog("Added " + binToFill.Upkeep + " to " + binToFill.Name);
        }
        // Creates and adds a bin to the binList
        public void AddBin(string name, string description, decimal upkeep, decimal currentAmount)
        {
            _binList.Add(new Bin(name, description, upkeep, currentAmount));
            UpdateLog("Created a new bin: " + name + " and added " + currentAmount);
            SaveUser();
        }
        // Remove a bin from the binList
        public void RemoveBin(Bin binToRemove, bool transferFunds)
        {
            UpdateLog("Removed the bin: " + binToRemove);
            if (transferFunds)
            {
                AddIncome(binToRemove.CurrentAmount);
            }
            _binList.Remove(binToRemove);
            SaveUser();
        }
        public void ModBin(Bin binToMod, string name, string description, decimal upkeep, decimal currentAmount)
        {
            string oldName = binToMod.Name;
            string oldDescription = binToMod.Description;
            decimal oldUpkeep = binToMod.Upkeep;
            decimal oldAmount = binToMod.CurrentAmount;

            binToMod.Name = name;
            binToMod.Description = description;
            binToMod.Upkeep = upkeep;
            binToMod.CurrentAmount = currentAmount;

            UpdateLog("Modifed " + binToMod.Name + Environment.NewLine +
                "Name changed from " + oldName + " to " + name + Environment.NewLine +
                "Description changed from " + oldDescription + " to " + description + Environment.NewLine +
                "Upkeep changed from " + oldUpkeep + " to " + upkeep + Environment.NewLine +
                "Current Amount changed from " + oldAmount + " to " + currentAmount + Environment.NewLine);
        }

        public void AddBill(string name, DateTime dueDate, string description, decimal amount,
            Bin binToRemoveFrom)
        {
            _billList.Add(new Bill(name, dueDate, description, amount, binToRemoveFrom));
            UpdateLog("Added the bill " + name + " due on " + dueDate.ToShortDateString() + 
                " for the amount of " + amount);
            SaveUser();
        }
        public void AddBill(string name, DateTime dueDate, string description, decimal amount,
            bool autoPay, Bin binToRemoveFrom, bool isPeriodic, TimeSpan? frequency)
        {
            _billList.Add(new Bill(name, dueDate, description, amount, binToRemoveFrom,
                isPeriodic, frequency, autoPay));
            UpdateLog("Added the bill " + name + " due on " + dueDate.ToShortDateString() +
                " for the amount of " + amount);
            SaveUser();
        }
        public void RemoveBill(Bill billToRemove, bool wasPayed = true)
        {
            if (wasPayed)
                UpdateLog("Payed the bill \"" + billToRemove.Name + "\"" + " of " + billToRemove.Amount);
            else
                UpdateLog("Removed the bill \"" + billToRemove.Name + "\"" + " of " + billToRemove.Amount);
            _billList.Remove(billToRemove);
        }
        // Returns a list of bills that are due from the current date
        // Up to the parameter date
        public List<Bill> GetUpcomingBills(DateTime futureDate)
        {
            // If a list is empty, does foreach still work?
            var upcomingBills = from bill in _billList
                                where bill.DueDate <= futureDate
                                orderby bill.DueDate ascending
                                select bill;

            return upcomingBills.ToList();
                                
         }
        public void PayBill(Bill billToPay)
        {
            billToPay.PayBill();
            RemoveBill(billToPay);

        }
        public void ModBill(Bill billToMod, string name, DateTime dueDate, string description, decimal amount)
        {
            string oldName = billToMod.Name;
            DateTime oldDate = billToMod.DueDate;
            string oldDescription = billToMod.Description;
            decimal oldAmount = billToMod.Amount;

            billToMod.Name = name;
            billToMod.DueDate = dueDate;
            billToMod.Description = description;
            billToMod.Amount = amount;

            UpdateLog("Modifed " + billToMod.Name + Environment.NewLine +
                "Name changed from " + oldName + " to " + name + Environment.NewLine +
                "Date due changed from " + oldDate + " to " + dueDate + Environment.NewLine +
                "Description changed from " + oldDescription + " to " + description + Environment.NewLine +
                "Amount changed from " + oldAmount + " to " + amount + Environment.NewLine);
        }


    }
}
