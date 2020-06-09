using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
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
    /// Interaction logic for LoggedInPage.xaml
    /// </summary>
    public partial class LoggedInPage : Page
    {
        public string Password { get; set; }
        public ObservableCollection<INotifyPropertyChanged> Items { get; set; }
        public LoggedInPage()
        {
            Password = "";
            Items = new ObservableCollection<INotifyPropertyChanged>();
            this.DataContext = Items;
            InitializeComponent();         
            RightPage.Navigate(new DirectoryPage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void AddPass(object sender, RoutedEventArgs e)
        {
            Passwords toAdd = new Passwords();
            Items.Add(toAdd);
        }

        private void AddPass_Dir(object sender, RoutedEventArgs e)
        {
            Passwords toAdd = new Passwords();
            toAdd.Parent = (sender as MenuItem).DataContext as Directory;
            ((sender as MenuItem).DataContext as Directory).Add(toAdd);
        }

        private void AddDir(object sender, RoutedEventArgs e)
        {
            Directory toAdd = new Directory();
            Items.Add(toAdd);
        }

        private void AddDir_Dir(object sender, RoutedEventArgs e)
        {
            Directory toAdd = new Directory();
            toAdd.Parent = (sender as MenuItem).DataContext as Directory;
            ((sender as MenuItem).DataContext as Directory).Add(toAdd);
        }

        private void AddImg(object sender, RoutedEventArgs e)
        {
            Image toAdd = new Image();
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                toAdd.Path = dlg.FileName;
            }
            if (toAdd.Path == null)
                return;
            Items.Add(toAdd);
        }

        private void AddImg_Dir(object sender, RoutedEventArgs e)
        {
            Image toAdd = new Image();
            toAdd.Parent = (sender as MenuItem).DataContext as Directory;
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                toAdd.Path = dlg.FileName;
            }
            if (toAdd.Path == null)
                return;
            ((sender as MenuItem).DataContext as Directory).Add(toAdd);
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (FilesList.SelectedItem is Directory)
            {
                DirectoryPage dp = new DirectoryPage();
                dp.DataContext = (Directory)FilesList.SelectedItem;
                RightPage.NavigationService.Navigate(dp);
            }
            if (FilesList.SelectedItem is Image)
            {
                ImagePage dp = new ImagePage();
                dp.DataContext = (Image)FilesList.SelectedItem;
                RightPage.NavigationService.Navigate(dp);
            }
            if (FilesList.SelectedItem is Passwords)
            {
                PasswordEditorPage dp = new PasswordEditorPage();
                dp.DataContext = (Passwords)FilesList.SelectedItem;
                RightPage.NavigationService.Navigate(dp);
            }
        }

        private void Rename_Dir(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).DataContext as Directory).IsBeingEdited = true;
        }

        private void Delete_Dir(object sender, RoutedEventArgs e)
        {
            var toDel = (sender as MenuItem).DataContext as Directory;
            if (toDel.Parent != null)
                toDel.Parent.Remove(toDel);
            else
                Items.Remove(toDel);
        }

        private void EndEditing_Dir(object sender, KeyEventArgs e)
        {
            if (System.Windows.Input.Key.Enter == e.Key)
                ((sender as TextBox).DataContext as Directory).IsBeingEdited = false;
        }

        private void Rename_Img(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).DataContext as Image).IsBeingEdited = true;
        }

        private void Delete_Img(object sender, RoutedEventArgs e)
        {
            var toDel = (sender as MenuItem).DataContext as Image;
            if (toDel.Parent != null)
                toDel.Parent.Remove(toDel);
            else
                Items.Remove(toDel);
        }

        private void EndEditing_Img(object sender, KeyEventArgs e)
        {
            if (System.Windows.Input.Key.Enter == e.Key)
                ((sender as TextBox).DataContext as Image).IsBeingEdited = false;
        }

        private void Rename_Pass(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).DataContext as Passwords).IsBeingEdited = true;
        }

        private void Delete_Pass(object sender, RoutedEventArgs e)
        {
            var toDel = (sender as MenuItem).DataContext as Passwords;
            if (toDel.Parent != null)
                toDel.Parent.Remove(toDel);
            else
                Items.Remove(toDel);
        }

        private void EndEditing_Pass(object sender, KeyEventArgs e)
        {
            if (System.Windows.Input.Key.Enter == e.Key)
                ((sender as TextBox).DataContext as Passwords).IsBeingEdited = false;
        }

        private void ItemLostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TreeViewItem).DataContext.GetType() == typeof(Passwords) && FilesList.SelectedItem != (sender as TreeViewItem).DataContext)
                ((sender as TreeViewItem).DataContext as Passwords).IsBeingEdited = false;
            if ((sender as TreeViewItem).DataContext.GetType() == typeof(Image) && FilesList.SelectedItem != (sender as TreeViewItem).DataContext)
                ((sender as TreeViewItem).DataContext as Image).IsBeingEdited = false;
            if ((sender as TreeViewItem).DataContext.GetType() == typeof(Directory) && FilesList.SelectedItem != (sender as TreeViewItem).DataContext)
                ((sender as TreeViewItem).DataContext as Directory).IsBeingEdited = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var Formatter = new BinaryFormatter();
            var Stream = new MemoryStream();
            Formatter.Serialize(Stream, this.Items);
            Byte[] data = Stream.ToArray();
            Byte[] encryptedData = WPF_Project.DataEncryption.Encrypt(this.Password, data);
            File.WriteAllBytes("Passwords.bin", encryptedData);
        }
    }

    class IsBlockVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
                return "Visible";
            else
                return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IsTextVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Visible";
            else
                return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class Test : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return (value as List<INotifyPropertyChanged>).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
