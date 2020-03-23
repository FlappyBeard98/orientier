using System.Linq;

namespace OrientDb.Queries
{
    internal class Array:OrientSqlExpr
    {
        public override string Sql { get; }

        private Array(params OrientSqlExpr[] exprs)
        {
            var exprsString = string.Join(", ", exprs.Select(p => p.Sql));
            Sql = $"[{exprsString}]" ;
        }

        public Array(params Identifier[] identifiers) : this(identifiers.OfType<OrientSqlExpr>().ToArray())
        {
        }
        
        public Array(params Array[] arrays): this(arrays.OfType<OrientSqlExpr>().ToArray())
        {
        }
    }
}