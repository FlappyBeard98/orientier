namespace OrientDb.Queries
{
    public abstract class OrientSqlExpr
    {
        public abstract string Sql { get; }
        public override string ToString() => Sql;
    }
}