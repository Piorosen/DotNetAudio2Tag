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
using TagLib;

namespace Tag.WPF
{
    public class ListToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<IPicture>)
            {
                var tagTemp = value as List<IPicture>;
                Bitmap image = null;
                if (tagTemp == null || tagTemp.Count == 0)
                {
                    image = new Bitmap(Setting.Global.Resource.Alert);
                    image.MakeTransparent(Color.White);
                }
                else
                {
                    image = new Bitmap(new MemoryStream(tagTemp[0].Data.Data));
                }

                var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                     image.GetHbitmap(),
                     IntPtr.Zero,
                     Int32Rect.Empty,
                     BitmapSizeOptions.FromEmptyOptions());
                return data;
            }

            if (value is Image)
            {
                var tagTemp = value as Image;
                Bitmap image = null;

                if (tagTemp == null)
                {
                    image = new Bitmap(Setting.Global.Resource.Alert);
                    image.MakeTransparent(Color.White);
                }
                else
                {
                    image = new Bitmap(tagTemp);
                }

                var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                     image.GetHbitmap(),
                     IntPtr.Zero,
                     Int32Rect.Empty,
                     BitmapSizeOptions.FromEmptyOptions());
                return data;
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
                return string.Join("; ", (value as List<string>));
            }
            if (value is List<uint>)
            {
                return string.Join(", ", (value as List<uint>));
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            if (typeof(List<uint>) == targetType)
            {
                try
                {
                    result = (value as string).Split(',')
                                    .ToList()
                                    .Select((s) => uint.Parse(s))
                                    .ToList();
                }
                catch { }
            }
            else
            {
                result = (value as string).Split(';')
                                        .ToList();
            }

            return result;
        }
    }
}
