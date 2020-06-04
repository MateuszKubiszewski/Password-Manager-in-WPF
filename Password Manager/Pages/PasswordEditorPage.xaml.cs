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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager.Pages
{
    /// <summary>
    /// Interaction logic for PasswordEditorPage.xaml
    /// </summary>
    public partial class PasswordEditorPage : Page
    {
        PasswordItem tempPass;

        public PasswordEditorPage()
        {
            InitializeComponent();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            PasswordItem pass = new PasswordItem(System.DateTime.Now);
            //var item = new ListViewItem();
            //item.Name = "Account Name";
            //PasswordList.Items.Add(item)
            ((this.DataContext as Data).File as Passwords).items.Add(pass);
            (this.DataContext as Data).NotifyPropertyChanged("File");
            PasswordList.SelectedItem = pass;
            PasswordForm.Visibility = Visibility.Visible;
            SavedForm.Visibility = Visibility.Hidden;
        }

        //private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    PasswordItem item = PasswordList.SelectedItem as PasswordItem;
        //    item.UpdateProperty(textBox.Name, textBox.Text);
        //    (this.DataContext as Data).NotifyPropertyChanged("File");
        //}

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
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(dlg.FileName);
                bitmap.EndInit();
                item.Icon = bitmap;
            }

            (this.DataContext as Data).NotifyPropertyChanged("File");
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            PasswordItem item = PasswordList.SelectedItem as PasswordItem;

            item.Name = Name.Text;
            item.Login = Login.Text;
            item.Email = Email.Text;
            item.Password = Password.Text;
            item.Website = Website.Text;
            item.Notes = Notes.Text;
            //long toAdd = System.DateTime.Now.Ticks - item.EditDate.Ticks;
            //item.EditDate.Add(new TimeSpan(toAdd));

            tempPass = new PasswordItem(item);

            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;

            (this.DataContext as Data).NotifyPropertyChanged("File");
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {

            (PasswordList.SelectedItem as PasswordItem).Reset(tempPass);

            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;
            
            (this.DataContext as Data).NotifyPropertyChanged("File");
        }

        private void NewSelection(object sender, SelectionChangedEventArgs e)
        {
            this.tempPass = new PasswordItem((PasswordItem)(sender as ListView).SelectedItem);
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Visible;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            ((this.DataContext as Data).File as Passwords).items.Remove((PasswordItem)PasswordList.SelectedItem);
            (this.DataContext as Data).NotifyPropertyChanged("File");
            PasswordForm.Visibility = Visibility.Hidden;
            SavedForm.Visibility = Visibility.Hidden;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            PasswordForm.Visibility = Visibility.Visible;
            SavedForm.Visibility = Visibility.Hidden;
        }

        private void CopyEmail(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SavedEmail.Text);
        }

        private void CopyPass(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SavedPassword.Text);
        }

        private void CopyLogin(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SavedLogin.Text);
        }
    }
}
