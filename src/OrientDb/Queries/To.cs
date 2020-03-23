namespace OrientDb.Queries
{
    internal class To:OrientSqlExpr
    {
        private readonly OrientSqlExpr _expr;
        public override string Sql => $"TO {_expr.Sql}";

        private To(OrientSqlExpr expr)
        {
            _expr = expr;
        }
        public To(Rid rid):this(rid as OrientSqlExpr)
        {
        }
        
        public To(Identifier id):this(new Identifier($"${id.Sql}") as OrientSqlExpr)
        {
        }
    }
}