using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Password_Manager
{
    class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value as ObservableCollection<PasswordItem>).Count == 0)
                return null;
            return (value as ObservableCollection<PasswordItem>).ToArray();
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class MultiDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] as string == null || values[1] as string == "")
                return (values[0] as List<PasswordItem>).ToArray();
            string filter = (values[1] as string).ToLower();
            List<PasswordItem> toRet = new List<PasswordItem>();
            foreach (var item in values[0] as List<PasswordItem>)
            {
                if (item.Name.ToLower().Contains(filter))
                    toRet.Add(item);
            }
            return toRet.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as PasswordItem).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class PasswordStrengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string pass = (string)value;
            PasswordStrength strength = PasswordStrengthUtils.CalculatePasswordStrength(pass);
            return strength.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ProgressBarForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string pass = (string)value;
            PasswordStrength strength = PasswordStrengthUtils.CalculatePasswordStrength(pass);
            switch (strength)
            {
                case PasswordStrength.Invalid: return "Transparent";
                case PasswordStrength.VeryWeak: return "Red";
                case PasswordStrength.Weak: return "Orange";
                case PasswordStrength.Average: return "Yellow";
                case PasswordStrength.Strong: return "YellowGreen";
                case PasswordStrength.VeryStrong: return "Green";
                default: return "Gray";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ProgressBarValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string pass = (string)value;
            PasswordStrength strength = PasswordStrengthUtils.CalculatePasswordStrength(pass);
            switch (strength)
            {
                case PasswordStrength.VeryWeak: return 20;
                case PasswordStrength.Weak: return 40;
                case PasswordStrength.Average: return 60;
                case PasswordStrength.Strong: return 80;
                case PasswordStrength.VeryStrong: return 100;
                default: return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value != "")
                return "Visible";
            else
                return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class GetFirstLetter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value as string) == "")
                return String.Empty;
            return (value as string).First().ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class GetImageDetails : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;
            if (value == null)
                return String.Empty;
            var bitmap = value as System.Windows.Media.Imaging.BitmapImage;
            return $"Resolution: {bitmap.PixelWidth}x{bitmap.PixelHeight}\nDPI: {bitmap.DpiX}x{bitmap.DpiY}\nFormat: {bitmap.Format}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class GetImgButtonText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Select";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class MaskPassword : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string toRet = "";
            foreach (var letter in (value as string))
            {
                toRet += '\u2022';
            }
            return toRet;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class MailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"mailto:{value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
