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

using System.IO;

namespace Budget.View
{
    /// <summary>
    /// Interaction logic for OpenUserWindow.xaml
    /// </summary>
    public partial class OpenUserWindow : Window
    {
        private List<string> _userFiles = new List<string>();

        public OpenUserWindow(string directory)
        {
            InitializeComponent();
            foreach (string file in Directory.GetFiles(directory))
            {
                _userFiles.Add(file);
            }
            UserFiles.ItemsSource = _userFiles;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserFiles.SelectedItem == null)
            {
                FilePath = @"Users\Default.xml";
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public string FilePath {
            get
            {
                if (UserFiles.SelectedItem == null)
                    return @"Users\Default.xml";
                else
                    return UserFiles.SelectedItem.ToString();
            }
            private set { }
        }
    }
}
