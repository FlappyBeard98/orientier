namespace OrientDb.Queries
{
    internal class Expand : SelectStatementExpr
    {
        private readonly Identifier _identifier;

        public Expand(Identifier identifier)
        {
            _identifier = identifier;
        }

        public override string Sql => $"EXPAND({_identifier.Sql})";
    }
}