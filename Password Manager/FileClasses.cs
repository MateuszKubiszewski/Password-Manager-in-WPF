using System;
using System.Collections.Generic;
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
    //public class Image : TreeViewItem
    //{
    //    public BitmapImage Img = null;

    //    public bool IsBeingEdited = false;

    //    public Image()
    //    {
    //        this.Header = "New Image";
    //        this.FontStyle = FontStyles.Italic;
    //        this.FontWeight = FontWeights.Normal;
    //    }
    //}

    public class Image : INotifyPropertyChanged
    {
        BitmapImage img;
        public BitmapImage Img
        {
            get { return img; }
            set
            {
                img = value;
                NotifyPropertyChanged("Img");
            }
        }
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
            Img = null;
            Name = "New Image";
            IsBeingEdited = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Directory : TreeViewItem
    {
        public bool IsBeingEdited = false;

        public Directory()
        {
            this.Header = "New Directory";
            this.FontWeight = FontWeights.Bold;
        }
    }

    public class Passwords : TreeViewItem
    {
        public List<PasswordItem> PasswordsList = new List<PasswordItem>();

        public bool IsBeingEdited = false;

        public Passwords()
        {
            this.Header = "New Passwords";
            this.FontStyle = FontStyles.Italic;
            this.FontWeight = FontWeights.Normal;
        }

        public Passwords(List<PasswordItem> passes)
        {
            PasswordsList = passes;
        }
    }
}
