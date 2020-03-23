namespace OrientDb.Queries
{
    internal class Where:OrientSqlExpr
    {
        private readonly Rid _rid;
        public override string Sql => $"WHERE @rid = {_rid.Sql}";

        public Where(Rid rid)
        {
            _rid = rid;
        }
    }
}