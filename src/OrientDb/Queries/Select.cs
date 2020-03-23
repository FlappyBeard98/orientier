using System.Linq;

namespace OrientDb.Queries
{
    internal class Select:OrientSqlExpr
    {
        private readonly SelectStatementExpr[] _statementExprs;
        private readonly From _from;
        private readonly OrientSqlExpr _where;
        public override string Sql => $"SELECT {string.Join(",",_statementExprs.Select(p=>p.Sql))} {_from.Sql} {_where.Sql}";

        private Select(SelectStatementExpr[] statementExprs, From @from, OrientSqlExpr @where)
        {
            _statementExprs = statementExprs;
            _from = @from;
            _where = @where;
        }

        public Select(From @from,params SelectStatementExpr[] statementExprs):this(statementExprs,@from,new None())
        {
        }
        public Select(From @from, Where @where,params SelectStatementExpr[] statementExprs):this(statementExprs,@from,@where)
        {
        }
        
    }
    
    
}