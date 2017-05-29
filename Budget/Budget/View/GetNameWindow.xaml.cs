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
    /// Interaction logic for GetNameWindow.xaml
    /// </summary>
    public partial class GetNameWindow : Window
    {
        public GetNameWindow()
        {
            InitializeComponent();
        }

        public string UserInput { get { return UserNameBox.Text; } }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
