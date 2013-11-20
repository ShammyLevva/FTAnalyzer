using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FTAnalyzer.Utilities
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private readonly Dictionary<Type, PropertyComparer<T>> comparers;
        private bool isSorted;
        private ListSortDirection listSortDirection;
        private PropertyDescriptor propertyDescriptor;

        public SortableBindingList()
            : base(new List<T>())
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        public SortableBindingList(IEnumerable<T> enumeration)
            : base(new List<T>(enumeration))
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return this.isSorted; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return this.propertyDescriptor; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return this.listSortDirection; }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            this.OnSortStarted();
            List<T> itemsList = (List<T>)this.Items;

            Type propertyType = property.PropertyType;
            PropertyComparer<T> comparer;
            if (!this.comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(property, direction);
                this.comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);
            MergeSort(itemsList, comparer);

            this.propertyDescriptor = property;
            this.listSortDirection = direction;
            this.isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            this.isSorted = false;
            this.propertyDescriptor = base.SortPropertyCore;
            this.listSortDirection = base.SortDirectionCore;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            int count = this.Count;
            for (int i = 0; i < count; ++i)
            {
                T element = this[i];
                if (property.GetValue(element).Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        private void MergeSort(List<T> inputList, PropertyComparer<T> comparer)
        {
            int left = 0;
            int right = inputList.Count - 1;
            InternalMergeSort(inputList, comparer, left, right);
        }

        private void InternalMergeSort(List<T> inputList, PropertyComparer<T> comparer, int left, int right)
        {
            int mid = 0;

            if (left < right)
            {
                mid = (left + right) / 2;
                InternalMergeSort(inputList, comparer, left, mid);
                InternalMergeSort(inputList, comparer, (mid + 1), right);
                MergeSortedList(inputList, comparer, left, mid, right);
            }
        }

        private void MergeSortedList(List<T> inputList, PropertyComparer<T> comparer, int left, int mid, int right)
        {
            int total_elements = right - left + 1; //BODMAS rule
            int right_start = mid + 1;
            int temp_location = left;
            List<T> tempList = new List<T>();

            while ((left <= mid) && right_start <= right)
            {
                if (comparer.Compare(inputList[left], inputList[right_start]) <= 0)
                {
                    tempList.Add(inputList[left++]);
                }
                else
                {
                    tempList.Add(inputList[right_start++]);
                }
            }

            if (left > mid)
            {
                for (int j = right_start; j <= right; j++)
                    tempList.Add(inputList[right_start++]);
            }
            else
            {
                for (int j = left; j <= mid; j++)
                    tempList.Add(inputList[left++]);
            }

            //Array.Copy(tempArray, 0, inputArray, temp_location, total_elements); // just another way of accomplishing things (in-built copy)
            for (int i = 0, j = temp_location; i < total_elements; i++, j++)
            {
                inputList[j] = tempList[i];
            }
        }
        #region EventHandler
        public event EventHandler SortStarted;
        public void OnSortStarted()
        {
            if (SortStarted != null)
                SortStarted(null, EventArgs.Empty);
        }

        #endregion

    }
}