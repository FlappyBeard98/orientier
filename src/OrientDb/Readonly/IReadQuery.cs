using OrientDb.Queries;

namespace OrientDb.Readonly
{
    public interface IReadQuery
    {
         OrientSqlExpr Query { get; }
    }
}