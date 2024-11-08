using System;
namespace TkwEF.Core.BLL
{
    public interface IBindingListViewT<T> : IBindingListViewT
    {
        void AddRangeT(System.Collections.Generic.IEnumerable<T> list);
        void AddRangeTClone(System.Collections.Generic.IEnumerable<T> list);
    }
    public interface IBindingListViewT
    {
        event EventHandler ListSorting;
    }
}
