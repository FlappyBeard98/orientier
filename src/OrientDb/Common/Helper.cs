using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace OrientDb.Common
{
    public static class Helper
    {
        public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> source) => await Task.WhenAll(source);
        public static MemberInfo GetMemberFromExpression<T,TValue>(this Expression<Func<T, TValue>> field)
        {
            var member = field.Body as MemberExpression ??
                         (field.Body as UnaryExpression)?.Operand as MemberExpression;
            if (member == null)
                throw new ArgumentException("invalid member selector");

            Debug.WriteLine($"{typeof(T).Name}.{member.Member.Name}:{member.Type.Name}");

            return member.Member;
        }
    }
}