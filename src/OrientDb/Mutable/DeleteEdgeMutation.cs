using System;
using System.Linq;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class DeleteEdgeMutation<T>:GraphMutation<T> where T :E
    {
        public DeleteEdgeMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
        }

        public override void Apply(ReadonlyGraph graph)
        {
            E dbItem = DbItem;
            if(dbItem != null)
                throw new InvalidOperationException($"edge {Item.GetType().Name} with @rid = {Item.Rid} was not deleted");

            Item.FromV.OutE.Remove(Item);
            Item.ToV.InE.Remove(Item);
            
            var items = graph.GraphItems[Item.GetType()];
            var item = items.FirstOrDefault(p => p.Rid == Item.Rid);
            items.Remove(item);
        }

        public override OrientSqlExpr Query => 
            new Delete(new Edge(), new Rid(Item.Rid));
    }
}