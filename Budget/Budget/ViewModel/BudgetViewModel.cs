using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using Budget.Model;
using Budget.View;

namespace Budget.ViewModel
{
    public class BudgetViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        private string _userDirectory = "";

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyPath)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyPath));
        }

        public ObservableCollection<Bin> BinList { get; set; }
        public ObservableCollection<Bill> BillList { get; set; }

        public string UserName { get { return _currentUser.Name; } }
        public decimal Balance { get { return _currentUser.Balance; } }
        public string Log { get { return _currentUser.GetLog(); } }
        public string UserDirectory { get { return _userDirectory; } }
        public DateTime? LastUpkeep { get { return _currentUser.LastUpkeep; } }

        public BudgetViewModel()
        {
            BinList = new ObservableCollection<Bin>();
            BillList = new ObservableCollection<Bill>();

            LoadDefaultUser();

            Update();
        }

        private void LoadDefaultUser()
        {
            if (Directory.Exists("Users"))
                _userDirectory = @"Users\";
            else
                Directory.CreateDirectory("Users");

            if (File.Exists(@"Users\Default.xml"))
                ReadUser(@"Users\Default.xml");
            else
            {
                _currentUser = new User("Default");
                ReadUser(@"Users\Default.xml");
            }
        }

        public void UpdateBinList()
        {
            BinList.Clear();
            foreach (Bin bin in _currentUser.BinList)
            {
                BinList.Add(bin);
            }
        }
        public void AddBin(string name, string description, decimal upkeep, decimal currentAmount)
        {
            _currentUser.AddBin(name, description, upkeep, currentAmount);
            Update();
        }
        public void FillBin(Bin binToFill, decimal amountToAdd)
        {
            _currentUser.FillBin(binToFill, amountToAdd);
            Update();
        }
        public void ModBin(Bin binToMod, string name, string description, decimal upkeep, decimal currentAmount)
        {
            _currentUser.ModBin(binToMod, name, description, upkeep, currentAmount);
            Update();
        }
        public void RemoveBin(Bin binToRemove, bool transferFunds)
        {
            _currentUser.RemoveBin(binToRemove, transferFunds);
            Update();

        }
        public void Upkeep()
        {
            _currentUser.Upkeep();
            Update();
        }
        public void AddIncome(decimal amount)
        {
            _currentUser.AddIncome(amount);
            Update();
        }
        public void Transfer(Bin fromBin, Bin toBin, decimal amount)
        {
            _currentUser.Transfer(fromBin, toBin, amount);
            Update();
        }

        public void UpdateBillList()
        {
            BillList.Clear();
            foreach (Bill bill in _currentUser.BillList)
            {
                BillList.Add(bill);
            }
        }
        public void AddBill(string name, DateTime dueDate, string description, decimal amount,
            Bin binToRemoveFrom)
        {
            _currentUser.AddBill(name, dueDate, description, amount, binToRemoveFrom);
            Update();
        }
        public void PayBill(Bill billToPay)
        {
            _currentUser.PayBill(billToPay);
            Update();
        }
        public void RemoveBill(Bill billToRemove)
        {
            _currentUser.RemoveBill(billToRemove, false);
            Update();
        }
        public void ModBill(Bill billToMod, string name, DateTime dueDate, string description, decimal amount)
        {
            _currentUser.ModBill(billToMod, name, dueDate, description, amount);
            Update();
        }

        public void Update()
        {
            OnPropertyChanged("Balance");
            OnPropertyChanged("UserName");
            OnPropertyChanged("Log");
            OnPropertyChanged("LastUpkeep");
            UpdateBinList();
            UpdateBillList();
            WriteUser(_currentUser);
        }
        public void NewUser(string userName)
        {
            _currentUser = new User(userName);
            Update();
        }
        public void LoadUser(string filePath)
        {
            ReadUser(filePath);
            Update();
        }
        public void ReadUser(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;
            if (!File.Exists(filePath))
                return;

            using (Stream inputStream = File.OpenRead(filePath))
            {
                DataContractSerializer serializer =
                    new DataContractSerializer(typeof(User));
                //try
                //{
                    _currentUser = serializer.ReadObject(inputStream) as User;
                //}
                //catch
                //{

                //}
            }
            OnPropertyChanged("Balance");
            UpdateBinList();
            UpdateBillList();
            OnPropertyChanged("UserName");
        }
        public void WriteUser(User userToWrite)
        {
            string filePath = Path.GetFullPath(_userDirectory + userToWrite.Name + ".xml");

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (Stream outputStream = File.OpenWrite(filePath))
            {
                DataContractSerializer serializer =
                    new DataContractSerializer(typeof(User));
                serializer.WriteObject(outputStream, userToWrite);
            }
        }
        public void ResetUser()
        {

            WriteUser(_currentUser);
            if (_currentUser.Name == "Default")
                return;
            else
                File.Replace(Path.GetFullPath(_userDirectory + _currentUser.Name + ".xml"),
                    Path.GetFullPath(_userDirectory + "Default.xml"),
                    Path.GetFullPath(_userDirectory + "Backup.xml"));
            if (File.Exists(Path.GetFullPath(_userDirectory + "Backup.xml")))
                File.Delete(Path.GetFullPath(_userDirectory + "Backup.xml"));
        }


    }
}
