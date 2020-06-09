using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Password_Manager
{
    [Serializable]
    public class Image : INotifyPropertyChanged
    {
        public Directory Parent { get; set; }
        public string Path { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        bool isbeingedited;
        public bool IsBeingEdited
        {
            get { return isbeingedited; }
            set
            {
                isbeingedited = value;
                NotifyPropertyChanged("IsBeingEdited");
            }
        }
        public Image()
        {
            Path = null;
            Name = "New Image";
            IsBeingEdited = false;
            Parent = null;
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Serializable]
    public class Directory : INotifyPropertyChanged
    {
        public Directory Parent { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        bool isbeingedited;
        public bool IsBeingEdited
        {
            get { return isbeingedited; }
            set
            {
                isbeingedited = value;
                NotifyPropertyChanged("IsBeingEdited");
            }
        }
        List<INotifyPropertyChanged> files;
        public List<INotifyPropertyChanged> Files
        {
            get { return files; }
            set
            {
                files = value;
                NotifyPropertyChanged("Files");
            }
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Directory()
        {
            Name = "New Directory";
            IsBeingEdited = false;
            Files = new List<INotifyPropertyChanged>();
            Parent = null;
        }

        public void Add(INotifyPropertyChanged file)
        {
            Files.Add(file);
            NotifyPropertyChanged("Files");
        }

        public void Remove(INotifyPropertyChanged file)
        {
            Files.Remove(file);
            NotifyPropertyChanged("Files");
        }
    }

    [Serializable]
    public class Passwords : INotifyPropertyChanged
    {
        public Directory Parent { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        bool isbeingedited;
        public bool IsBeingEdited
        {
            get { return isbeingedited; }
            set
            {
                isbeingedited = value;
                NotifyPropertyChanged("IsBeingEdited");
            }
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        ObservableCollection<PasswordItem> passwordslist;
        public ObservableCollection<PasswordItem> PasswordsList
        {
            get { return passwordslist; }
            set
            {
                passwordslist = value;
                NotifyPropertyChanged("PasswordsList");
            }
        }
        string searchtext;
        public string SearchText
        {
            get { return searchtext; }
            set
            {
                searchtext = value;
                NotifyPropertyChanged("SearchText");
            }
        }

        public Passwords()
        {
            Name = "New Passwords";
            IsBeingEdited = false;
            PasswordsList = new ObservableCollection<PasswordItem>();
            Parent = null;
        }

        public void Add(PasswordItem pass)
        {
            PasswordsList.Add(pass);
            NotifyPropertyChanged("PasswordsList");
        }

        public void Remove(PasswordItem pass)
        {
            PasswordsList.Remove(pass);
            NotifyPropertyChanged("PasswordsList");
        }
    }
}
