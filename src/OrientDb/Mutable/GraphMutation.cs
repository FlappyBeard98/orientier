using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrientDb.Common;
using OrientDb.OrientApiCommands;
using OrientDb.Queries;
using OrientDb.Readonly;
using RestSharp;

namespace OrientDb.Mutable
{
    internal abstract class GraphMutation<T> : IGraphMutation where T : GraphItem
    {
        protected readonly T Item;
        public Identifier Identifier { get; }
        protected T DbItem { get; private set; }

        protected GraphMutation(T item,IdentifierFactory identifierFactory)
        {
            Item = item;
            Identifier = identifierFactory.GetIdentifier(item.GetType().Name);
        }

        public GraphItem GraphItem => Item;
        public abstract void Apply(ReadonlyGraph graph);
        public abstract OrientSqlExpr Query { get;  }
        //todo сомнительное решение, но оно нужно чтобы сначала применялись мутации для вершин, а потом для ребер
        public int Idx => Item is V ? 0 : 1;

        public async Task RefreshItem(Dictionary<string, string> result, OrientConnection setupConnection, IRestClient setupClient)
        {
            var rid = new Rid(Item.IsNew() ? result[Identifier.Sql] : Item.Rid);
            var select = new Select(new From(rid));
            var batch = new OrientQueryBatch(setupConnection,select);
            var command = new Batch<List<T>>(batch,setupClient);
            var res = await command.Execute() switch
                {
                    Result<List<T>, Exception>.OkCase o => o.Payload,
                    Result<List<T>, Exception>.ErrCase e => throw e.Payload,
                    _ => throw new InvalidCastException()
                };
            DbItem = res.FirstOrDefault();
        }
    }
}