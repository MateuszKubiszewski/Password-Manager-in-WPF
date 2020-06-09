using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for PasswordEditorPage.xaml
    /// </summary>
    public partial class PasswordEditorPage : Page
    {
        public PasswordEditorPage()
        {
            InitializeComponent();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            PasswordItem pass = new PasswordItem(System.DateTime.Now);
            (this.DataContext as Passwords).Add(pass);
            PasswordList.SelectedItem = pass;
            PasswordForm.Visibility = Visibility.Visible;
            SavedForm.Visibility = Visibility.Hidden;
            TurnOnEditMode();
        }

        //https://www.c-sharpcorner.com/UploadFile/mahesh/using-xaml-image-in-wpf/
        private void AddImageClick(object sender, RoutedEventArgs e)
        {
            PasswordItem item = PasswordList.SelectedItem as PasswordItem;

            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "C:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                item.Icon = dlg.FileName;
            }
            long toAdd = System.DateTime.Now.Ticks - item.EditDate.Ticks;
            item.EditDate = item.EditDate.Add(new TimeSpan(toAdd));
            (this.DataContext as Passwords).NotifyPropertyChanged("PasswordList");
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            PasswordItem item = PasswordList.SelectedItem as PasswordItem;

            if (item.Name == Name.Text && item.Login == Login.Text && item.Email == Email.Text && 
                item.Website == Website.Text && item.Notes == Notes.Text && item.Password == Password.Text)
            {
                PasswordForm.Visibility = Visibility.Hidden;
                SavedForm.Visibility = Visibility.Visible;
                TurnOffEditMode();
                return;
            }
            item.Name = Name.Text;
            item.Login = Login.Text;
            item.Email = Email.Text;
            item.Password = Password.Text;
            item.Website = Website.Text;
            item.Notes = Notes.Text;
            long toAdd = System.DateTime.Now.Ticks - item.EditDate.Ticks;
            item.EditDate = item.EditDate.Add(new TimeSpan(toAdd));
            //http://drwpf.com/blog/2008/10/20/itemscontrol-e-is-for-editable-collection/
            var itemToAdd = new PasswordItem(item);
            (this.DataContext as Passwords).Remove(PasswordList.SelectedItem as PasswordItem);
            (this.DataContext as Passwords).Add(itemToAdd);
            PasswordList.SelectedItem = itemToAdd;
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;
            TurnOffEditMode();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;
            TurnOffEditMode();
        }

        private void NewSelection(object sender, SelectionChangedEventArgs e)
        {
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Passwords).Remove(PasswordList.SelectedItem as PasswordItem);
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Hidden;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            PasswordForm.Visibility = Visibility.Visible;
            SavedForm.Visibility = Visibility.Hidden;
            TurnOnEditMode();
        }

        private void CopyEmail(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SavedEmail.Text);
        }

        private void CopyPass(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText((PasswordList.SelectedItem as PasswordItem).Password);
        }

        private void CopyLogin(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SavedLogin.Text);
        }

        private void NavigateWebsite(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.AbsoluteUri);
            } catch 
            {
                MessageBox.Show("Operation not supported for relative URI.", "Invalid website link", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }

        void TurnOffEditMode()
        {
            PasswordList.IsEnabled = true;
            if (SearchTextBox.Text == null || SearchTextBox.Text == "")
                AddPassword.IsEnabled = true;
            else
                AddPassword.IsEnabled = false;
            SearchTextBox.IsEnabled = true;
        }

        void TurnOnEditMode()
        {
            PasswordList.IsEnabled = false;
            AddPassword.IsEnabled = false;
            SearchTextBox.IsEnabled = false;
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordList.Items.Filter = MyFilter;
        }

        private bool MyFilter(object item)
        {
            if (SearchTextBox.Text == null || SearchTextBox.Text == "")
            {
                AddPassword.IsEnabled = true;
                return true;
            }
            else
            {
                AddPassword.IsEnabled = false;
                return (item as PasswordItem).Name.ToLower().Contains(SearchTextBox.Text.ToLower());
            }
        }
    }
}
