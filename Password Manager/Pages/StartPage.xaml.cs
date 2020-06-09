using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("Passwords.bin"))
            {
                LoggedInPage lp = new LoggedInPage();
                if (passwordBox == null)
                    lp.Password = "";
                else
                    lp.Password = passwordBox.Password;
                this.NavigationService.Navigate(lp);
            }
            else
            {
                Byte[] encryptedData = File.ReadAllBytes("Passwords.bin");
                Byte[] data = WPF_Project.DataEncryption.Decrypt(passwordBox.Password, encryptedData);
                if (data == null)
                {
                    MessageBox.Show("Invalid Password", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var Formatter = new BinaryFormatter();
                var Stream = new MemoryStream(data);
                var lp = new LoggedInPage();
                lp.Items = (ObservableCollection<INotifyPropertyChanged>)Formatter.Deserialize(Stream);
                lp.DataContext = lp.Items;
                this.NavigationService.Navigate(lp);
            }
        }
    }
}
