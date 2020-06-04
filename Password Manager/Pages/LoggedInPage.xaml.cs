using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            DirectoryPage p = new DirectoryPage();
            
            RightPage.Navigate(new DirectoryPage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void AddDir(object sender, RoutedEventArgs e)
        {
            var item = new TreeViewItem();
            Directory dir = new Directory();
            item.Header = dir.Name;
            item.Tag = dir;
            if (dir.isbold == true)
                item.FontWeight = FontWeights.Bold;

            var parent = FilesList.SelectedItem as TreeViewItem;
            if (parent == null)
            { //add to main directory
                FilesList.Items.Add(item);
                return;
            }
            if (!(parent.Tag is Directory)) return; // can't add to file that is not directory
            ((Directory)parent.Tag).add(dir); //add to this directory object
            parent.Items.Add(item); //add to treeview
        }

        private void AddPass(object sender, RoutedEventArgs e)
        {
            TreeViewItem Item = new TreeViewItem();
            Passwords pass = new Passwords();
            Item.Header = pass.Name;
            Item.Tag = pass;
            if (pass.isbold == true)
                Item.FontWeight = FontWeights.Bold;
            else
                Item.FontWeight = FontWeights.Light;
            var parent = FilesList.SelectedItem as TreeViewItem;
            if (parent == null)
            { //add to main directory
                FilesList.Items.Add(Item);
                return;
            }
            if (!(parent.Tag is Directory)) return;
            ((Directory)parent.Tag).add(pass); //add to this directory object
            parent.Items.Add(Item); //add to treeview
        }

        private void AddImg(object sender, RoutedEventArgs e)
        {
            TreeViewItem Item = new TreeViewItem();
            Image img = new Image();
            Item.Header = img.Name;
            Item.Tag = img;
            if (img.isbold == true)
                Item.FontWeight = FontWeights.Bold;
            else
                Item.FontWeight = FontWeights.Light;
            var parent = FilesList.SelectedItem as TreeViewItem;
            if (parent == null)
            { //add to main directory
                FilesList.Items.Add(Item);
                FilesList.SetResourceReference(TreeView.ContextMenuProperty, "baseContextMenu");
                return;
            }
            if (!(parent.Tag is Directory)) return;
            ((Directory)parent.Tag).add(img); //add to this directory object
            parent.Items.Add(Item); //add to treeview
            FilesList.SetResourceReference(TreeView.ContextMenuProperty, "baseContextMenu");
        }

        private void TreeViewItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((TreeViewItem)sender).Tag.GetType() == typeof(Directory))
                FilesList.SetResourceReference(TreeView.ContextMenuProperty, "directoryContextMenu");
            else
            {
                if (((TreeViewItem)sender).Tag.GetType() == typeof(Passwords))
                    FilesList.SetResourceReference(TreeView.ContextMenuProperty, "passwordsContextMenu");
                if (((TreeViewItem)sender).Tag.GetType() == typeof(Image))
                    FilesList.SetResourceReference(TreeView.ContextMenuProperty, "imageContextMenu");

            }
            e.Handled = true;
        }

        private void TreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilesList.SetResourceReference(TreeView.ContextMenuProperty, "baseContextMenu");
            e.Handled = true;
        }

        private void TreeViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            item.IsSelected = true;
            item.IsExpanded = true;
            if (item.Tag is Directory)
            {
                DirectoryPage dp = new DirectoryPage();
                Data data = new Data();
                data.File = (File)item.Tag;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
            if (item.Tag is Image)
            {
                ImagePage dp = new ImagePage();
                Data data = new Data();
                data.File = (File)item.Tag;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
            if (item.Tag is Passwords)
            {
                PasswordEditorPage dp = new PasswordEditorPage();
                Data data = new Data();
                data.File = (File)item.Tag;
                dp.DataContext = data;
                RightPage.NavigationService.Navigate(dp);
            }
        }

        private void AddRename(object sender, RoutedEventArgs e)
        {

        }

        private void AddDelete(object sender, RoutedEventArgs e)
        {

        }
    }
}
