using System;
using System.Linq.Expressions;

namespace OrientDb.Mutable
{
    public interface ISetter<T>
    {
        ISetter<T> Set<TValue>(Expression<Func<T, TValue>> setter, TValue value);
    }
}