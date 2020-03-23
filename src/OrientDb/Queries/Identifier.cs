namespace OrientDb.Queries
{
    internal class Identifier:OrientSqlExpr
    {
        public override string Sql { get; }

        public Identifier(string identifier)
        {
            Sql = identifier;
        }
    }
}