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
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        public BillWindow(List<Bin> binList, Bin binToRemoveFrom, string name = "", DateTime? dueDate = null, 
            string description = "", decimal amount = 0.0M)
        {
            InitializeComponent();

            BillNameBox.Text = name;
            BillDateBox.SelectedDate = dueDate;
            BillDescriptionBox.Text = description;
            BillAmountBox.Text = amount.ToString();
            BillBinBox.ItemsSource = binList;
            BillBinBox.SelectedItem = binToRemoveFrom;

        }

        public string BillName { get { return BillNameBox.Text; } }
        public DateTime? SelectedDate { get { return BillDateBox.SelectedDate; } }
        public string BillDescription { get { return BillDescriptionBox.Text; } }
        public decimal BillAmount { get { return Convert.ToDecimal(BillAmountBox.Text); } }
        public Bin BinToRemoveFrom { get { return (BillBinBox.SelectedItem as Bin); } }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate == null || BinToRemoveFrom==null)
            {
                return;
            }
            else
            {
                DialogResult = true;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
