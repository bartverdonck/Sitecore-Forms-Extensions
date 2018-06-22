using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class FieldBindingTokenKeyAlpabetSorter : IComparer<FieldBindingTokenKey>
    {
        public int Compare(FieldBindingTokenKey x, FieldBindingTokenKey y)
        {
            Debug.Assert(x != null, nameof(x) + " != null");
            Debug.Assert(y != null, nameof(y) + " != null");
            var xKey = x.Category + x.Label;
            var yKey = y.Category + y.Label;
            return string.Compare(xKey, yKey, StringComparison.Ordinal);
        }
    }
}