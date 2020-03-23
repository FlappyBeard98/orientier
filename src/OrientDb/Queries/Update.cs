namespace OrientDb.Queries
{
    internal class Update:OrientSqlExpr
    {
        public override string Sql => $"UPDATE {_token.Sql} {_rid.Sql} {_set.Sql} ";
        private readonly OrientSqlExpr _token;
        private readonly Rid _rid;
        private readonly Set _set;

        private Update(OrientSqlExpr token, Rid rid, Set set)
        {
            _token = token is Vertex ? new None() : token;
            _rid = rid;
            _set = set;
        }
        
        public Update(TypeExpr token,Rid rid,Set set) :this(token as OrientSqlExpr, rid,set)
        {
           
        }
            
        public Update(Update update,Set set):  this(update._token,update._rid,new Set(update._set,set))
        {
        }
    }
}