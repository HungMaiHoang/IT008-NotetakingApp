using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Note.Utilities
{
    public class TimeAgoConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime targetTime)
            {
                TimeSpan timeDifference = DateTime.Now - targetTime;

                if (timeDifference.TotalMinutes < 60)
                {
                    int minutes = (int)timeDifference.TotalMinutes;
                    return $"{minutes} {(minutes > 1 ? "minutes" : "minute")} ago";
                }
                else if (timeDifference.TotalHours < 24)
                {
                    int hours = (int)timeDifference.TotalHours;
                    return $"{hours} {(hours > 1 ? "hours" : "hour")} ago";
                }
                else
                {
                    return targetTime.ToString("dd/MM/yyyy");
                }
            }

            return value; // Returning the original value in case of an issue
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
