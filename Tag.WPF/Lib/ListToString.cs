using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Tag.WPF
{
    public class ListToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<TagLib.IPicture>)
            {
                var tagTemp = value as List<TagLib.IPicture>;
                Bitmap image = null;

                if (tagTemp.Count > 0)
                {
                    var bin = tagTemp[0].Data.Data;
 
                    image = new Bitmap(Image.FromStream(new MemoryStream(bin)));
                    var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                         image.GetHbitmap(),
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions());
                    return data;
                }
                return image;
            }

            if (value is double)
            {
                return ((double)value).ToString();
            }
            if (value is int)
            {
                return ((int)value).ToString();
            }

            if (value is List<string>)
            {
                return string.Join(", ", (value as List<string>));
            }
            if (value is List<uint>)
            {
                return string.Join(", ", (value as List<uint>));
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
