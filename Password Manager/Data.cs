using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Password_Manager
{
    class Data : INotifyPropertyChanged
    {
        // These fields hold the values for the public properties.
        TreeViewItem file;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public TreeViewItem File
        {
            get
            {
                return this.file;
            }

            set
            {
                if (value != this.file)
                {
                    this.file = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}