using System.Linq;

namespace OrientDb.Queries
{
    internal class Return:OrientSqlExpr
    {
        private readonly Let[] _lets;
        private readonly Array _array;
        public override string Sql => $"RETURN {_array.Sql}";

        private Identifier FirstRid(Identifier identifier) =>new Identifier($"{identifier.Sql}[0]");
        public Return(params Let[] let)
        {
            _lets = @let;
            var vars = _lets.Select(p =>
            {
                var a = new Array(p.StringName,FirstRid( p.Variable));
                return a;
            }).ToArray();
            _array= new Array(vars);
        }
         
       
    }
}