using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;

namespace TkwEF.Core.BLL
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public abstract class BindingListView<T> : BindingList<T>, IBindingListView, IRaiseItemChangedEvents, IBindingListViewT<T> where T : BLL.IBiz
    {
        #region Fields

        private bool m_Sorted = false;
        private bool m_Filtered = false;
        private string m_FilterString = null;
        private ListSortDirection m_SortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor m_SortProperty = null;
        private ListSortDescriptionCollection m_SortDescriptions = new ListSortDescriptionCollection();
        private List<T> m_OriginalCollection = new List<T>();

        #endregion

        #region enum

        /// <summary>
        /// Тип фильтрации
        /// </summary>
        private enum FilterMode
        {
            None, Like, Not_Like, Equals, Not_Equals
        }

        #endregion

        #region ctor

        public BindingListView()
            : base() { }
        public BindingListView(List<T> list)
            : base(list) { }

        #endregion

        #region Function

        public void AddRangeT(IEnumerable<T> list)
        {
            if (list == null) return;
            for (int i = 0; i < list.Count(); i++)
                this.Add(list.ElementAt(i));
        }

        public void AddRangeTClone(IEnumerable<T> list)
        {
            for (int i = 0; i < list.Count(); i++)
                this.Add((T)UtilityCore.CloneReflection(list.ElementAt(i)));
        }

        private void ApplySortInternal(SortComparer<T> comparer)
        {
            if (m_OriginalCollection.Count == 0)
            {
                m_OriginalCollection.AddRange(this);
            }
            List<T> listRef = this.Items as List<T>;
            if (listRef == null)
                return;

            OnListSorting(this, new EventArgs());
            listRef.Sort(comparer);
            m_Sorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected virtual void UpdateFilter()
        {
            UtilityCore.ByFilter.FilterMode fMode = UtilityCore.ByFilter.FilterMode.None;

            if (m_OriginalCollection.Count == 0)
            {
                m_OriginalCollection.AddRange(this);
            }

            List<T> currentCollection = new List<T>(this);
            Clear();

            _applyFilter(fMode, currentCollection);
        }
        /// <summary>
        /// Возвращает индекс в массиве по id для BLL.BIZ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int IndexOfBiz(int id)
        {
            if (!typeof(T).Equals(typeof(BIZ_Base))) return -1;
            for (int i = 0; i < this.Items.Count; i++)
            {
                BIZ_Base biz = this.Items[i] as BIZ_Base;
                if ((this.Items[i] as BIZ_Base).Id == id)
                    return i;
            }
            return -1;
        }

        #endregion

        #region IBindingListView Members

        void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
        {
            m_SortProperty = null;
            m_SortDescriptions = sorts;
            SortComparer<T> comparer = new SortComparer<T>(sorts);
            ApplySortInternal(comparer);
        }

        string IBindingListView.Filter
        {
            get
            {
                return m_FilterString;
            }
            set
            {
                //SetProcessWorkingSetSizeMinimum();
                m_FilterString = value;
                m_Filtered = true;
                UpdateFilter();
                //SetProcessWorkingSetSizeMinimum();
            }
        }

        void IBindingListView.RemoveFilter()
        {
            if (!m_Filtered)
                return;
            m_FilterString = null;
            m_Filtered = false;
            m_Sorted = false;
            m_SortDescriptions = null;
            m_SortProperty = null;
            Clear();
            foreach (T item in m_OriginalCollection)
            {
                Add(item);
            }
            m_OriginalCollection.Clear();
        }

        ListSortDescriptionCollection IBindingListView.SortDescriptions
        {
            get
            {
                return m_SortDescriptions;
            }
        }

        bool IBindingListView.SupportsAdvancedSorting
        {
            get
            {
                return true;
            }
        }

        bool IBindingListView.SupportsFiltering
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region IBindingList overrides

        bool IBindingList.AllowNew
        {
            get
            {
                return CheckReadOnly();
            }
        }

        bool IBindingList.AllowRemove
        {
            get
            {
                return CheckReadOnly();
            }
        }

        private bool CheckReadOnly()
        {
            if (m_Sorted || m_Filtered)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region IRaiseItemChangedEvents Members

        bool IRaiseItemChangedEvents.RaisesItemChangedEvents
        {
            get { return true; }
        }

        #endregion

        #region События дополнительно

        /// <summary>
        /// Происходит в начале сортировки
        /// </summary>
        public event EventHandler ListSorting;
        /// <summary>
        /// Вызывает событие ListSorting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnListSorting(object sender, EventArgs e)
        {
            if (ListSorting != null)
                ListSorting(sender, e);
        }


        #endregion

        #region Override

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            // Simple iteration:
            //for (int i = 0; i < Count; i++)
            //{
            //    T item = this[i];
            //    if (property.GetValue(item) == key)
            //    {
            //        return i;
            //    }
            //}
            //return -1; // Not found

            // Using List.FindIndex:
            Predicate<T> pred = delegate(T item)
            {
                if (property.GetValue(item).Equals(key))
                    return true;
                else
                    return false;
            };
            List<T> list = Items as List<T>;
            if (list == null)
                return -1;
            return list.FindIndex(pred);
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return m_Sorted; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return m_SortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return m_SortProperty; }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            m_SortDirection = direction;
            m_SortProperty = property;
            SortComparer<T> comparer = new SortComparer<T>(property, direction);
            ApplySortInternal(comparer);
        }

        protected override void RemoveSortCore()
        {
            if (!m_Sorted)
                return;

            Clear();
            foreach (T item in m_OriginalCollection)
            {
                Add(item);
            }
            m_OriginalCollection.Clear();
            m_SortProperty = null;
            m_SortDescriptions = null;
            m_Sorted = false;
        }

        public override string ToString()
        {
            return string.Format("Количество строк = {0}", Count);
        }

        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            m_OriginalCollection.Clear();
            m_OriginalCollection.AddRange(this);
            base.OnAddingNew(e);
        }

        #endregion

        #region Private function

        private void _applyFilter(UtilityCore.ByFilter.FilterMode fMode, List<T> currentCollection)
        {
            foreach (T item in currentCollection)
            {
                bool isTrue = false;

                #region Фильтры OR
                // Фильтры ИЛИ
                isTrue = _applyFilterOR(ref fMode, this.m_FilterString, item);

                #endregion

                if (isTrue)
                    Add(item);
            }
        }

        private bool _applyFilterOR(ref UtilityCore.ByFilter.FilterMode fMode, string filter, T item, bool isBegin = true)
        {
            string[] filtersOR = UtilityCore.ByFilter.GetArrayBySeparator(filter, UtilityCore.ByFilter.Separator.OR).ToArray<string>();
            foreach (string curFilter in filtersOR)
            {
                if (_applyFilterAND(ref fMode, curFilter, item))
                    return true;
            }
            return false;
        }

        private bool _applyFilterAND(ref UtilityCore.ByFilter.FilterMode fMode, string filter, T item)
        {
            bool isTrue = false;
            string[] filtersAND = UtilityCore.ByFilter.GetArrayBySeparator(filter, UtilityCore.ByFilter.Separator.AND).ToArray<string>();

            int equalsPos = 0, equalZnak = 0, equalBegin = 0;

            if (filtersAND.Count() > 1)
            {
                //Фильтры И
                foreach (string fltr in filtersAND)
                {
                    if (!_applyFilterOR(ref fMode, fltr, item, false))
                        return false;
                }
                return true;
            }
            else if (filtersAND.Count() == 1 && filtersAND[0] == filter)
            {
                //isTrue = _applyFilterOR(ref fMode, new string[] { fltr }, item);
                //if (!isTrue)
                //    break;
                UtilityCore.ByFilter.GetFilterItem(filter, ref fMode, ref equalBegin, ref equalsPos, ref equalZnak);
                // Get property name
                string propName = filter.Substring(0, equalsPos).Trim();
                // Get Filter criteria
                string criteria = filter.Substring(equalsPos + equalZnak).Trim();
                criteria = criteria.Replace("'", "");

                // Get a property descriptor for the filter property
                PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(T))[propName];
                decimal dec;
                if (decimal.TryParse(criteria, out dec))
                    isTrue = _addInFilter(fMode, dec, propDesc: propDesc, item: item);
                else
                    isTrue = _addInFilter(fMode, criteria, propDesc: propDesc, item: item);
            }
            return isTrue;
            //if (isTrue)
            //    break;
            //}
            //return isTrue;
        }
        /// <summary>
        /// Возвращает массив с разделителем OR
        /// </summary>
        /// <returns></returns>
        private string[] _getArrayFiltersOR()
        {
            string[] filters;
            filters = m_FilterString.Split(new string[] { " OR " }, StringSplitOptions.RemoveEmptyEntries);
            return filters;
        }
        /// <summary>
        /// Разбор строки фильтра по скобкам
        /// </summary>
        /// <param name="m_FilterString"></param>
        /// <returns></returns>
        private string[] _getArrayFiltersBrackets(string fltr)
        {
            const char bracketLeft = '(', bracketRight = ')';
            int iLeft = 0, iRight = 0;//Количество левых и правых скобок
            int posLeft = 0, posRight = 0; // Текущая позиция левой и правой скобки
            int pos = 0; // Текущая проверяемая позиция
            pos = fltr.IndexOf(bracketLeft);
            if (pos == 0)
            {
                return new[] { fltr };
            }

            iLeft = 1;
            while (iLeft < iRight || pos < fltr.Length || posRight == 0)
            {
                posLeft = fltr.IndexOf(bracketLeft, pos);
                posRight = fltr.IndexOf(bracketRight, pos);
                if (posLeft < posRight)
                {
                    iLeft++;
                    pos = posLeft;
                }
                else
                {
                    iRight++;
                    pos = posRight;
                }
            }
            return null;
        }
        /// <summary>
        /// При выполнении условий добавляется в список
        /// </summary>
        /// <param name="fMode"></param>
        /// <param name="criteries"></param>
        /// <param name="propDesc"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool _addInFilter(UtilityCore.ByFilter.FilterMode fMode, string criteries, PropertyDescriptor propDesc, T item)
        {
            if (propDesc == null)
                return false;
            object value = propDesc.GetValue(item);
            //if (value == null)
            //    value = string.Empty;
            switch (fMode)
            {
                case UtilityCore.ByFilter.FilterMode.None:
                    return true;
                case UtilityCore.ByFilter.FilterMode.Like:
                    if (value == null || criteries.Equals("null", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    if (criteries.Length == 0 && value.ToString().Length == 0)
                        return true;
                    if (criteries.Length != 0)
                    {
                        criteries = criteries.Replace(" ", "%");
                        if (!criteries.StartsWith("%"))
                            criteries = "%" + criteries;
                        if (!criteries.EndsWith("%"))
                            criteries += "%";
                        return _isEqualByLikeCriteria(criteries, value);
                    }
                    else
                    {
                        return false;
                    }
                case UtilityCore.ByFilter.FilterMode.Not_Like:
                    if (value == null || criteries.Equals("null", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    if (criteries.Length == 0 && value.ToString().Length == 0)
                        return false;
                    if (criteries.Length != 0)
                    {
                        if (criteries.StartsWith("%") && criteries.EndsWith("%"))
                            return value.ToString().ToUpper().IndexOf(criteries.Replace("%", "").ToUpper()) == -1;
                        else if (criteries.StartsWith("%"))
                            return !value.ToString().ToUpper().EndsWith(criteries.Replace("%", "").ToUpper());
                        else if (criteries.EndsWith("%"))
                            return !value.ToString().ToUpper().StartsWith(criteries.Replace("%", "").ToUpper());
                        else
                            return !value.ToString().Equals(criteries);
                    }
                    else
                        return true;
                case UtilityCore.ByFilter.FilterMode.Equals:
                    if (value == null)
                        return criteries.Equals("null", StringComparison.OrdinalIgnoreCase);
                    return value.ToString() == criteries;
                case UtilityCore.ByFilter.FilterMode.Not_Equals:
                    if (value == null)
                        return false;
                    return value.ToString() != criteries;
                case UtilityCore.ByFilter.FilterMode.Bolshe:
                    if (value == null)
                        return false;
                    //if(value is decimal || value is int || value is short || value is byte)
                    //{
                        return (decimal)value > decimal.Parse(criteries);
                //}
                case UtilityCore.ByFilter.FilterMode.BolsheRavno:
                    if (value == null)
                        return false;
                    return (decimal)value >= decimal.Parse(criteries);
                case UtilityCore.ByFilter.FilterMode.Menshe:
                    if (value == null)
                        return false;
                    return (decimal)value < decimal.Parse(criteries);
                case UtilityCore.ByFilter.FilterMode.MensheRavno:
                    if (value == null)
                        return false;
                    return (decimal)value <= decimal.Parse(criteries);
                default:
                    return false;
            }
        }
        private bool _addInFilter(UtilityCore.ByFilter.FilterMode fMode, decimal criteries, PropertyDescriptor propDesc, T item)
        {
            if (propDesc == null)
                return false;
            object value = propDesc.GetValue(item);
            //if (value == null)
            //    value = string.Empty;
            if (value == null)
                return false;
            string strCrinteries;
            decimal res;
            switch (fMode)
            {
                case UtilityCore.ByFilter.FilterMode.None:
                    return true;
                case UtilityCore.ByFilter.FilterMode.Like:
                    if (value.ToString().Length == 0)
                        return false;
                    strCrinteries = criteries.ToString("0.########");
                    strCrinteries = "%" + criteries;
                    strCrinteries += "%";
                    return _isEqualByLikeCriteria(strCrinteries, value);
                case UtilityCore.ByFilter.FilterMode.Not_Like:
                    if (value.ToString().Length == 0)
                        return false;
                    strCrinteries = criteries.ToString("0.########");
                    return !value.ToString().Equals(strCrinteries);
                case UtilityCore.ByFilter.FilterMode.Equals:
                    return criteries.ToString().Equals(value.ToString());
                case UtilityCore.ByFilter.FilterMode.Not_Equals:
                    return !criteries.ToString().Equals(value.ToString());
                case UtilityCore.ByFilter.FilterMode.Bolshe:
                    if (value is decimal)
                        return (decimal)value > criteries;
                    if (!decimal.TryParse(value.ToString(), out res))
                        return false;
                    return res > criteries;
                case UtilityCore.ByFilter.FilterMode.BolsheRavno:
                    if (value is decimal)
                        return (decimal)value >= criteries;
                    if (!decimal.TryParse(value.ToString(), out res))
                        return false;
                    return res >= criteries;
                case UtilityCore.ByFilter.FilterMode.Menshe:
                    if (value is decimal)
                        return (decimal)value < criteries;
                    if (!decimal.TryParse(value.ToString(), out res))
                        return false;
                    return res < criteries;
                case UtilityCore.ByFilter.FilterMode.MensheRavno:
                    if (value is decimal)
                        return (decimal)value <= criteries;
                    if (!decimal.TryParse(value.ToString(), out res))
                        return false;
                    return res <= criteries;
                default:
                    return false;
            }
        }

        private static bool _isEqualByLikeCriteria(string criteries, object value)
        {
            List<string> criteriaList =
                criteries.Split(new char[] { '%', '*' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            try
            {
                if (criteriaList.Count == 0)
                    return true;
                if ((criteries.StartsWith("%") && criteries.EndsWith("%")) || (criteries.StartsWith("*") && criteries.EndsWith("*")))
                {
                    int pos = -1;
                    foreach (string criteria in criteriaList)
                    {
                        //return value.ToString().ToUpper().IndexOf(criteries.Replace("%", "").ToUpper()) >= 0;
                        pos = value.ToString().IndexOf(criteria, pos + 1, StringComparison.CurrentCultureIgnoreCase);
                        if (pos < 0)
                            return false;
                    }
                    return true;
                }
                else if (criteries.StartsWith("%") || criteries.StartsWith("*"))
                {
                    //return value.ToString().ToUpper().EndsWith(criteries.Replace("%", "").ToUpper());
                    int pos = -1;
                    for (int i = criteriaList.Count - 1; i >= 0; --i)
                    {
                        if (pos < 0)
                        {
                            if (!value.ToString().EndsWith(criteriaList[i], StringComparison.CurrentCultureIgnoreCase))
                                return false;
                            else
                                pos = value.ToString().Length - criteriaList[i].Length;
                        }
                        else
                        {
                            pos = value.ToString().LastIndexOf(criteriaList[i], pos - 1, StringComparison.CurrentCultureIgnoreCase);
                            if (pos < 0)
                                return false;
                        }
                    }
                    return true;
                }
                else if (criteries.EndsWith("%") || criteries.EndsWith("*"))
                {
                    //return value.ToString().ToUpper().StartsWith(criteries.Replace("%", "").ToUpper());
                    int pos = -1;
                    foreach (string criteria in criteriaList)
                    {
                        if (pos < 0)
                        {
                            if (!value.ToString().StartsWith(criteria, StringComparison.CurrentCultureIgnoreCase))
                                return false;
                            else
                                pos = criteria.Length;
                        }
                        else
                        {
                            pos = value.ToString().IndexOf(criteria, pos + 1, StringComparison.CurrentCultureIgnoreCase);
                            if (pos < 0)
                                return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //return value.ToString().Equals(criteries);
                    int pos = -1;
                    if (pos < 0)
                    {
                        if (!value.ToString().StartsWith(criteriaList[0], StringComparison.CurrentCultureIgnoreCase) ||
                            !value.ToString().EndsWith(criteriaList[criteriaList.Count - 1], StringComparison.CurrentCultureIgnoreCase))
                            return false;
                        else
                            pos = criteriaList[0].Length;
                    }
                    else
                    {
                        if (criteriaList.Count > 2)
                        {
                            for (int i = 1; i < criteriaList.Count - 2; i++)
                            {
                                pos = value.ToString().IndexOf(criteriaList[i], pos + 1, StringComparison.CurrentCultureIgnoreCase);
                                if (pos < 0)
                                    return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return true;
                }
            }
            finally
            {
                criteriaList = null;
            }
        }

        #endregion

        #region Comment

        //[DllImport("kernel32.dll")]
        //public static extern bool SetProcessWorkingSetSize(IntPtr handle,
        //    int minimumWorkingSetSize, int maximumWorkingSetSize);
        //public static bool SetProcessWorkingSetSizeMinimum()
        //{
        //    return SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        //}

        //private void _getFilterItem(string filterString, ref FilterMode fMode, ref int equalBegin, ref int equalsPos, ref int equalZnak)
        //{
        //    if (filterString.IndexOf('=', equalBegin) > -1)
        //    {
        //        fMode = FilterMode.Equals;
        //        //equalsPos = filterString.Substring(0, filterString.IndexOf('=')).Trim().Length + "=".Length;
        //        equalsPos = filterString.IndexOf('=', equalBegin);
        //        equalZnak = "=".Length;
        //    }
        //    else if (filterString.IndexOf("NOT LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase) > -1)
        //    {
        //        fMode = FilterMode.Not_Like;
        //        equalsPos = filterString.IndexOf("NOT LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase);
        //        equalZnak = "NOT LIKE".Length;
        //    }
        //    else if (filterString.IndexOf("LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase) > -1)
        //    {
        //        fMode = FilterMode.Like;
        //        equalsPos = filterString.IndexOf("LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase);
        //        equalZnak = "Like".Length;
        //    }
        //    else if (filterString.IndexOf("<>", equalBegin) > -1)
        //    {
        //        fMode = FilterMode.Not_Equals;
        //        equalsPos = filterString.Substring(0, filterString.IndexOf("<>", equalBegin)).Trim().Length;
        //        equalZnak = "<>".Length;
        //    }
        //    else
        //    {
        //        fMode = FilterMode.Equals;
        //        equalsPos = 0;
        //        equalZnak = 0;
        //        equalBegin = 0;
        //    }
        //    //equalEnd = filterString.ToUpper().IndexOf("OR");
        //    //if (equalEnd == -1)
        //    //    equalEnd = filterString.ToUpper().IndexOf("AND");
        //}

        #region Убрал из-за утечки
        //protected override void InsertItem(int index, T item)
        //{
        //    foreach (PropertyDescriptor propDesc in TypeDescriptor.GetProperties(item))
        //    {
        //        if (propDesc.SupportsChangeEvents)
        //        {
        //            propDesc.AddValueChanged(item, OnItemChanged);
        //        }
        //    }
        //    base.InsertItem(index, item);
        //}

        //protected override void RemoveItem(int index)
        //{
        //    T item = Items[index];
        //    PropertyDescriptorCollection propDescs = TypeDescriptor.GetProperties(item);
        //    foreach (PropertyDescriptor propDesc in propDescs)
        //    {
        //        if (propDesc.SupportsChangeEvents)
        //        {
        //            propDesc.RemoveValueChanged(item, OnItemChanged);
        //        }
        //    }
        //    base.RemoveItem(index);
        //}

        //void OnItemChanged(object sender, EventArgs args)
        //{
        //    int index = Items.IndexOf((T)sender);
        //    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        //} 
        #endregion

        #endregion
    }
}
