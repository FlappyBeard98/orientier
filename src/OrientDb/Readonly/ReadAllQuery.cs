using OrientDb.Queries;

namespace OrientDb.Readonly
{
    internal class ReadAllQuery<T> : IReadQuery where T : GraphItem
    {
        public OrientSqlExpr Query => new Select(new From(new GraphClass(typeof(T))));
    }
}