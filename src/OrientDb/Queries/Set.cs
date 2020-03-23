using System.Linq;

namespace OrientDb.Queries
{
    internal class Set:OrientSqlExpr
    {
        private readonly BinaryExpr[] _assignments;
        public override string Sql { get; }

        public Set(BinaryExpr[] assignments)
        {
            _assignments = assignments ?? new BinaryExpr[0];
            Sql = _assignments.Length  == 0 
                ? "" 
                : $"SET {string.Join(", ", _assignments.Select(p => p.Sql))}";
        }
            
        public Set(Set set,BinaryExpr[] assignments) :this (set._assignments.Union(assignments).ToArray())
        {
        }
            
        public Set(Set setLeft,Set setRight) :this (setLeft,setRight._assignments)
        {
        }
    }
}