using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace FTAnalyzer.Utilities
{
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly IComparer comparer;
        private PropertyDescriptor propertyDescriptor;
        private int reverse;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this.propertyDescriptor = property;
            Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);
            this.comparer = (IComparer)comparerForPropertyType.InvokeMember("Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null);
            this.SetListSortDirection(direction);
        }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            var xValue = this.propertyDescriptor.GetValue(x);
            var yValue = this.propertyDescriptor.GetValue(y);
            string xString = xValue == null ? null : xValue.ToString();
            string yString = yValue == null ? null : yValue.ToString();
            if (String.IsNullOrEmpty(xString) && string.IsNullOrEmpty(yString))
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(xString))
            {
                return this.reverse;
            }
            else if (string.IsNullOrEmpty(yString))
            {
                return -1 * this.reverse;
            }
            return this.reverse * this.comparer.Compare(xValue, yValue);
        }

        #endregion

        private void SetPropertyDescriptor(PropertyDescriptor descriptor)
        {
            this.propertyDescriptor = descriptor;
        }

        private void SetListSortDirection(ListSortDirection direction)
        {
            this.reverse = direction == ListSortDirection.Ascending ? 1 : -1;
        }

        public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
        {
            this.SetPropertyDescriptor(descriptor);
            this.SetListSortDirection(direction);
        }
    }
}