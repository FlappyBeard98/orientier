using System;
using System.Text.RegularExpressions;

namespace OrientDb.Queries
{
    internal class Rid : OrientSqlExpr
    {
        public override string Sql { get; }

        public Rid(string rid)
        {
                
            if(!Regex.IsMatch(rid,@"#\d+:\d+",RegexOptions.Compiled))
                throw new ArgumentException($"{rid} is not orient record id");
            Sql = rid;
        }
    }
}