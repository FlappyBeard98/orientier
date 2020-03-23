using OrientDb.Queries;

namespace OrientDb.Readonly
{
    internal class ReadSchemeQuery : IReadQuery 
    {
         public OrientSqlExpr Query => new Select(new From(new MetadataSchema()),new Expand(new Identifier("classes")));
    }
}