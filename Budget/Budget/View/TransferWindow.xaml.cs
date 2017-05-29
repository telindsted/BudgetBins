using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using Budget.Model;

namespace Budget.View
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        Bin _balanceBin;

        public TransferWindow(List<Bin> binList, decimal balance)
        {
            InitializeComponent();
            _balanceBin = new Bin("Balance", "Temp balance bin", 0, 0.0M);
            _balanceBin.CurrentAmount = balance;
            binList.Add(_balanceBin);
            ToBox.ItemsSource = binList;
            FromBox.ItemsSource = binList;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public decimal Amount
        {
            get
            {
                try
                { return Convert.ToDecimal(AmountBox.Text); }
                catch (FormatException)
                { return 0.0M; }
            }
        }
        public Bin ToBin { get { if (ToBox.SelectedItem==_balanceBin) { return null; }
                return ToBox.SelectedItem as Bin; } }
        public Bin FromBin { get { if (FromBox.SelectedItem == _balanceBin) { return null; }
                return FromBox.SelectedItem as Bin; } }
    }
}
