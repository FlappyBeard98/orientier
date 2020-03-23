using System.Collections.Generic;
using System.Threading.Tasks;
using OrientDb.OrientApiCommands;
using OrientDb.Queries;
using OrientDb.Readonly;
using RestSharp;

namespace OrientDb.Mutable
{
    internal interface IGraphMutation
    {
        Identifier Identifier { get; }
        GraphItem GraphItem { get; }
        void Apply(ReadonlyGraph graph);
        OrientSqlExpr Query { get; }

        int Idx { get;  }

        //todo пока так но запихивать исполнение запроса сюда - не правильно 
        Task RefreshItem(Dictionary<string, string> result, OrientConnection setupConnection, IRestClient setupClient);
    }
}