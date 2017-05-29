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

using Budget.ViewModel;
using Budget.Model;

namespace Budget.View
{
    /// <summary>
    /// Interaction logic for BudgetView.xaml
    /// </summary>
    public partial class BudgetView : Window
    {
        BudgetViewModel viewModel;

        public BudgetView()
        {
            InitializeComponent();
            viewModel = FindResource("viewModel") as BudgetViewModel;
        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.AddIncome(Convert.ToDecimal(BalanceBox.Text));
            }
            // For illegal input, just add 0
            catch (FormatException)
            {
                viewModel.AddIncome(0.0M);
                BalanceBox.Clear();
            }
        }

        private void BillPay_Click(object sender, RoutedEventArgs e)
        {
            if (BillDisplay.SelectedItem is Bill)
            {
                Bill selectedBill = BillDisplay.SelectedItem as Bill;
                viewModel.PayBill(selectedBill);
            }
        }
        private void BillAdd_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.BinList.Count <= 0)
            {
                return;
            }
            BillWindow billWindow = new BillWindow(viewModel.BinList.ToList(), null);
            if (billWindow.ShowDialog() == true)
            {
                viewModel.AddBill(billWindow.BillName, billWindow.SelectedDate.Value, billWindow.BillDescription,
                    billWindow.BillAmount, billWindow.BinToRemoveFrom);
            }
        }
        private void BillMod_Click(object sender, RoutedEventArgs e)
        {

            if (BillDisplay.SelectedItem is Bill)
            {
                Bill selectedBill = BillDisplay.SelectedItem as Bill;
                BillWindow billWindow = new BillWindow(viewModel.BinList.ToList(), selectedBill.BinToRemoveFrom,
                    selectedBill.Name, selectedBill.DueDate, selectedBill.Description, selectedBill.Amount);
                if (billWindow.ShowDialog() == true)
                {
                    viewModel.ModBill(selectedBill, billWindow.BillName, billWindow.SelectedDate.Value, billWindow.BillDescription,
                        billWindow.BillAmount);
                }
            }
        }
        private void BillDel_Click(object sender, RoutedEventArgs e)
        {
            if (BillDisplay.SelectedItem is Bill)
            {
                Bill selectedBill = BillDisplay.SelectedItem as Bill;
                YesNoWindow ConfirmDelete = new YesNoWindow("Do you want to permanently delete "
                 + selectedBill.Name + "?");
                if (ConfirmDelete.ShowDialog() == true)
                {
                    viewModel.RemoveBill(selectedBill);
                }
            }

        }

        private void BinMod_Click(object sender, RoutedEventArgs e)
        {
            if (BinDisplay.SelectedItem is Bin)
            {
                Bin selectedBin = BinDisplay.SelectedItem as Bin;
                BinWindow binWindow = new BinWindow(selectedBin.Name, selectedBin.Description,
                    selectedBin.Upkeep, selectedBin.CurrentAmount);
                if (binWindow.ShowDialog() == true)
                {
                    viewModel.ModBin(selectedBin, binWindow.NameResult, binWindow.DescriptionResult,
                        binWindow.UpkeepResult, binWindow.AmountResult);
                }
            }
        }
        private void BinNew_Click(object sender, RoutedEventArgs e)
        {
            BinWindow binWindow = new BinWindow();
            if (binWindow.ShowDialog() == true)
            {
                viewModel.AddBin(binWindow.NameResult, binWindow.DescriptionResult,
                    binWindow.UpkeepResult, binWindow.AmountResult);
            }
        }
        private void BinDel_Click(object sender, RoutedEventArgs e)
        {
            if (BinDisplay.SelectedItem is Bin)
            {
                Bin selectedBin = BinDisplay.SelectedItem as Bin;
                YesNoWindow ConfirmDelete = new YesNoWindow("Do you want to permanently delete "
                    + selectedBin.Name + "?");
                if (ConfirmDelete.ShowDialog() == true)
                {
                    YesNoWindow ConfirmBinDeletionTransfer = new YesNoWindow("Transfer current amount to balance?");
                    if (ConfirmBinDeletionTransfer.ShowDialog() == true)
                        viewModel.RemoveBin(selectedBin, true);
                    else
                        viewModel.RemoveBin(selectedBin, false);
                }
                
            }
        }

        private void UpkeepButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Upkeep();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            GetNameWindow getNameWindow = new GetNameWindow();
            if (getNameWindow.ShowDialog() == true)
            {
               viewModel.NewUser(getNameWindow.UserInput);
            }
        }

        private void LoadUserButton_Click(object sender, RoutedEventArgs e)
        {
            GetNameWindow getNameWindow = new GetNameWindow();
            if (getNameWindow.ShowDialog() == true)
            {
                viewModel.LoadUser(getNameWindow.UserInput);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.ResetUser();
        }
    }
}
