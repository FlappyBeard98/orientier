using System;
using System.Collections.Generic;
using System.Linq;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class DeleteVertexMutation<T>:GraphMutation<T> where T :V
    {
        public DeleteVertexMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
        }

        public override void Apply(ReadonlyGraph graph)
        {
            V dbItem = DbItem;
            if(dbItem != null)
                throw new InvalidOperationException($"vertex {Item.GetType().Name} with @rid = {Item.Rid} was not deleted");
            
            var items = graph.GraphItems[Item.GetType()];
            var item = items.FirstOrDefault(p => p.Rid == Item.Rid) as V;
            
            var edges = item?.Both()
                            .SelectMany(p=> new [] {p.FromV,p.ToV})
                            .ToList() ?? new List<V>();
            
            edges.ForEach(p=>graph.GraphItems[p.GetType()].Remove(p)); 

            items.Remove(item);
        }

        public override OrientSqlExpr Query => 
            new Delete(new Vertex(), new Rid(Item.Rid));
    }
}