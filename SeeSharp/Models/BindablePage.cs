using System;
using osu.Framework.Bindables;

namespace SeeSharp.Models
{
    public class BindablePage : Bindable<Page>, IComparable<BindablePage>
    {
        public BindablePage(Page page) : base(page){}
        
        public BindablePage() : base(){}

        public int CompareTo(BindablePage other) => String.Compare(Value.Name, other.Value.Name, StringComparison.Ordinal);
    }
}