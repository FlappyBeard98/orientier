namespace OrientDb.Queries
{
    internal class Delete:OrientSqlExpr
    {
        public override string Sql => $"DELETE {_token.Sql} {_rid.Sql}";
        private readonly TypeExpr _token;
        private readonly Rid _rid;

        public Delete(TypeExpr token, Rid rid) 
        {
            _token = token;
            _rid = rid;
        }
    }
}