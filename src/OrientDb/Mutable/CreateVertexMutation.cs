using System;
using System.Linq;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class CreateVertexMutation<T>:GraphMutation<T> where T : V
    {
        public CreateVertexMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
        }

        public override void Apply(ReadonlyGraph graph)
        {
            V dbItem = DbItem;
            if(dbItem == null)
                throw new InvalidOperationException($"vertex {Item.GetType().Name} was not created");
            
            Item.Rid = DbItem.Rid;
            graph.GraphItems[Item.GetType()].Add(Item);
        }

        private Set GetSetter()
        {
            var setters =
                Item.GetType()
                    .GetProperties()
                    .Where(p => p.DeclaringType == Item.GetType())
                    .Select(p => new {p.Name, p.PropertyType, Value = p.GetValue(Item)})
                    .Where(p => p.Value != null || p.PropertyType == typeof(string))
                    .Select(p => new BinaryExpr(p.Name,p.Value,p.PropertyType))
                    .ToArray();
           
            return new Set(setters);
        }
        public override OrientSqlExpr Query =>
            new Let(new Create(new Vertex(), new GraphClass(Item.GetType()),GetSetter()), Identifier);
    }
}