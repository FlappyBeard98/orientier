using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using OrientDb.Common;

namespace OrientDb.Mutable
{
    internal abstract class UpdateMutation<T> : GraphMutation<T> where T : GraphItem
    {
        protected readonly List<Action<T>> Sets; 
        protected UpdateMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
            if(item.IsNew())
                throw new ArgumentException(nameof(item));
            Sets= new List<Action<T>>();
        }
        protected static void Set<TValue>(Expression<Func<T, TValue>> selector,T item,TValue value)
        {
            var member = selector.GetMemberFromExpression();
            switch (member)
            {
                case PropertyInfo pi:
                    pi.SetValue(item,value);
                    break;
                case FieldInfo fi:
                    fi.SetValue(item,value);
                    break;
                default:
                    throw new ArgumentException("expected field or property");
            }
        }
        internal abstract void Reset<TValue>(Expression<Func<T, TValue>> selector, TValue value);
        
    }
}