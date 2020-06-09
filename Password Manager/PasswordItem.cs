using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Password_Manager
{
    [Serializable]
    public class PasswordItem : INotifyPropertyChanged
    {
        string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyPropertyChanged();
            }
        }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged();
            }
        }
        string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                NotifyPropertyChanged();
            }
        }
        string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged();
            }
        }
        string website;
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
                NotifyPropertyChanged();
            }
        }
        string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                NotifyPropertyChanged();
            }
        }
        DateTime creationdate;
        public DateTime CreationDate
        {
            get { return creationdate; }
            set
            {
                creationdate = value;
                NotifyPropertyChanged();
            }
        }
        DateTime editdate;
        public DateTime EditDate
        {
            get { return editdate; }
            set
            {
                editdate = value;
                NotifyPropertyChanged();
            }
        }

        public PasswordItem(DateTime date)
        {
            Icon = null;
            Name = "Account Name";
            CreationDate = date;
            EditDate = date;
        }

        public PasswordItem(PasswordItem pass)
        {
            if (pass == null)
            {
                return;
            }
            Name = pass.Name;
            Login = pass.Login;
            Email = pass.Email;
            Icon = pass.Icon;
            Password = pass.Password;
            Website = pass.Website;
            Notes = pass.Notes;
            CreationDate = pass.CreationDate;
            EditDate = pass.EditDate;
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateProperty(string property, string value)
        {
            switch (property)
            {
                case "Name": Name = value; break;
                case "Email": Email = value; break;
                case "Login": Login = value; break;
                case "Password": Password = value; break;
                case "Website": Website = value; break;
                case "Notes": Notes = value; break;
            }
        }

        public void Reset(PasswordItem pass)
        {
            Name = pass.Name;
            Login = pass.Login;
            Email = pass.Email;
            Icon = pass.Icon;
            Password = pass.Password;
            Website = pass.Website;
            Notes = pass.Notes;
            CreationDate = pass.CreationDate;
            EditDate = pass.EditDate;
        }

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
