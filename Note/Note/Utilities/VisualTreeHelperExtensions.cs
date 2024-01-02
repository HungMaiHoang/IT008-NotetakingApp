using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Note.Utilities
{
    public static class VisualTreeHelperExtensions
    {
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T && ((FrameworkElement)child).Name == childName)
                    return (T)child;

                T result = FindChild<T>(child, childName);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
