using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {
        public ImagePage()
        {
            InitializeComponent();
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/8152511/how-to-save-bitmapimage-via-savefiledialog-from-wpf
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = (this.DataContext as Image).Path;
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder(); // Or JpegBitmapEncoder, or whichever encoder you want
                encoder.Frames.Add(BitmapFrame.Create(new Uri((this.DataContext as Image).Path)));
                using (var stream = saveFileDialog.OpenFile())
                {
                    encoder.Save(stream);
                }
            }
        }
    }

    class ImagePageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Image).Path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
