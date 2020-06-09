using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
        public LoggedInPage()
        {
            InitializeComponent();         
            RightPage.Navigate(new DirectoryPage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void AddDir(object sender, RoutedEventArgs e)
        {
            Directory toAdd = new Directory();
            TreeViewItem selected = (TreeViewItem)FilesList.SelectedItem;
            if (selected == null)
            {
                (FilesList as TreeView).Items.Add(toAdd);
                return;
            }
            else if (selected.GetType() == typeof(Directory))
            {
                selected.Items.Add(toAdd);
                return;
            }
            else
            {
                (FilesList as TreeView).Items.Add(toAdd);
            }
        }

        private void AddPass(object sender, RoutedEventArgs e)
        {
            Passwords toAdd = new Passwords();
            toAdd.ItemTemplate = FilesList.FindResource("PassTemplate") as DataTemplate;
            TreeViewItem selected = (TreeViewItem)FilesList.SelectedItem;
            if (selected == null)
            {
                (FilesList as TreeView).Items.Add(toAdd);
                return;
            }
            else if (selected.GetType() == typeof(Directory))
            {
                selected.Items.Add(toAdd);
                return;
            }
            else
            {
                (FilesList as TreeView).Items.Add(toAdd);
            }
        }

        private void AddImg(object sender, RoutedEventArgs e)
        {
            Image toAdd = new Image();
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(dlg.FileName);
                bitmap.EndInit();
                toAdd.Img = bitmap;
            }
            if (toAdd.Img == null)
                return;
            TreeViewItem selected = (TreeViewItem)FilesList.SelectedItem;
            if (selected == null)
            {
                (FilesList as TreeView).Items.Add(toAdd);
                return;
            }
            else if (selected.GetType() == typeof(Directory))
            {
                selected.Items.Add(toAdd);
                return;
            }
            else
            {
                (FilesList as TreeView).Items.Add(toAdd);
            }
        }

        private void TreeViewItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(Directory))
                FilesList.SetResourceReference(TreeView.ContextMenuProperty, "directoryContextMenu");
            else
            {
                if (sender.GetType() == typeof(Passwords))
                    FilesList.SetResourceReference(TreeView.ContextMenuProperty, "passwordsContextMenu");
                if (sender.GetType() == typeof(Image))
                    FilesList.SetResourceReference(TreeView.ContextMenuProperty, "imageContextMenu");
            }
            e.Handled = true;
        }

        private void TreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilesList.SetResourceReference(TreeView.ContextMenuProperty, "baseContextMenu");
            e.Handled = true;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            //var item = sender as TreeViewItem;
            //item.IsSelected = true;
            //item.IsExpanded = true;
            //MessageBox.Show($"{FilesList.SelectedItem.ToString()}");
            if (FilesList.SelectedItem is Directory)
            {
                DirectoryPage dp = new DirectoryPage();
                Data data = new Data();
                data.File = (TreeViewItem)FilesList.SelectedItem;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
            if (FilesList.SelectedItem is Image)
            {
                ImagePage dp = new ImagePage();
                Data data = new Data();
                data.File = (TreeViewItem)FilesList.SelectedItem;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
            if (FilesList.SelectedItem is Passwords)
            {
                PasswordEditorPage dp = new PasswordEditorPage();
                Data data = new Data();
                data.File = (TreeViewItem)FilesList.SelectedItem;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
        }


        private void Rename(object sender, RoutedEventArgs e)
        {
            //(sender as TreeViewItem).Template = "EditTemplate";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }

    class AmIVisible : IValueConverter
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
}
