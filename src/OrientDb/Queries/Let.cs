namespace OrientDb.Queries
{
    internal class Let:OrientSqlExpr
    {
        private readonly OrientSqlExpr _expr;
        private readonly Identifier _identifier;
        public override string Sql => $"LET {_identifier.Sql} = {_expr.Sql}";
        public Identifier Variable => new Identifier($"${_identifier.Sql}");
        public Identifier StringName => new Identifier($"'{_identifier.Sql}'");

        private Let(Identifier identifier, OrientSqlExpr expr)
        {
            _identifier = identifier;
            _expr = expr;
        }
        
        public Let(Create create,Identifier identifier):this(identifier,create)
        {
        }
        
        public Let(Identifier left,Identifier right):this(left,right as OrientSqlExpr)
        {
        }
    }
}