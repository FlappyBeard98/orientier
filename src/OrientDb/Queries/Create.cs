namespace OrientDb.Queries
{
    internal class Create:OrientSqlExpr
    {
        public override string Sql => $"CREATE {_token.Sql} {_graphClass.Sql} {_from.Sql} {_to.Sql} {_set.Sql}";
        private readonly TypeExpr _token;
        private readonly GraphClass _graphClass;
        private readonly OrientSqlExpr _from;
        private readonly OrientSqlExpr _to;
        private readonly OrientSqlExpr _set;

        private Create(TypeExpr token,GraphClass graphClass,OrientSqlExpr @from,OrientSqlExpr @to,OrientSqlExpr set)
        { 
            _token = token;
            _graphClass = graphClass;
            _from = @from;
            _to = to;
            _set = set;
        }

        public Create(TypeExpr vertex,GraphClass graphClass,Set set) :  this(vertex ,graphClass,new None() ,new None(), set as OrientSqlExpr)
        {
        }
            
        public Create(TypeExpr edge,GraphClass graphClass,From @from,To to,Set set):  this(edge , graphClass,@from as OrientSqlExpr,to,set)
        {
        }
    }
}