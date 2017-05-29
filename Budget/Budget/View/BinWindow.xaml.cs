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

namespace Budget.View
{
    /// <summary>
    /// Interaction logic for BinWindow.xaml
    /// </summary>
    public partial class BinWindow : Window
    {
        public BinWindow(string name = "", string description = "", decimal upkeep = 0.0M,
            decimal amount = 0.0M)
        {
            InitializeComponent();
            BinName.Text = name;
            BinDescription.Text = description;
            BinUpkeep.Text = Convert.ToString(upkeep);
            BinAmount.Text = Convert.ToString(amount);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string NameResult
        {
            get { return BinName.Text; }
        }
        public string DescriptionResult
        {
            get { return BinDescription.Text; }
        }
        public decimal UpkeepResult
        {
            get { return Convert.ToDecimal(BinUpkeep.Text); }
        }
        public decimal AmountResult
        {
            get { return Convert.ToDecimal(BinAmount.Text); }
        }
    }
}
