using System;

namespace OrientDb.Queries
{
    internal  class From:OrientSqlExpr
    {
        private readonly OrientSqlExpr _expr;
        public override string Sql => $"FROM {_expr.Sql}";

        private From(OrientSqlExpr expr)
        {
            _expr = expr;
        }
        
        public From(Rid rid):this(rid as OrientSqlExpr)
        {
        }
            
        public From(GraphClass @class):this(@class as OrientSqlExpr)
        {
        }
        
        public From(Identifier id):this(new Identifier($"${id.Sql}") as OrientSqlExpr)
        {
        }
    }
}