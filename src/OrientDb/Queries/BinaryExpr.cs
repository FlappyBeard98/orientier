using System;
using RestSharp.Extensions;

namespace OrientDb.Queries
{
    internal  class BinaryExpr:OrientSqlExpr
    {
        public override string Sql { get; }

        private static string ToStringValue(object str) =>
            str?.ToString().Replace("'", "\\'").UrlEncode();  
        public BinaryExpr(string left,object right,Type rightType = null )
        {
            rightType ??= typeof(object);
            if(string.IsNullOrWhiteSpace(left))
                throw new ArgumentNullException(nameof(left));
               
            var r = rightType == typeof(string) ? $"'{ToStringValue(right)}'" : right?.ToString();
            r = right == null ? "null" : r;
                
            Sql = $"{left} = {r}";
        }
    }
}