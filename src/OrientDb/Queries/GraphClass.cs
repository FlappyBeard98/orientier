using System;

namespace OrientDb.Queries
{
    internal class GraphClass : OrientSqlExpr
    {
        public override string Sql { get; }

        public GraphClass(Type type) : this($"{type.Name}")
        {
        }
        internal GraphClass(string type)
        {
            Sql = type;
        }
    }
    
    internal class GraphClass<T> : GraphClass where T:GraphItem
    {
        public GraphClass():base(typeof(T))
        {
               
        }
    }
}