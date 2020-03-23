using System;
using System.Linq.Expressions;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class Setter<T> :ISetter<T> where T:GraphItem
    {
        private readonly UpdateMutation<T> _mutation;

        public Setter(UpdateMutation<T> mutation)
        {
            _mutation = mutation;
        }

        public ISetter<T> Set<TValue>(Expression<Func<T,TValue>> setter,TValue value)
        {
            _mutation.Reset(setter,value);
            return this;
        }

    }
}