using System;
using System.Collections.Generic;
using System.Linq;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class CreateEdgeMutation<T>:GraphMutation<T> where T:E
    {

        private OrientSqlExpr _query;
        public CreateEdgeMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
        }

        public CreateEdgeMutation(T edge, V @from, V to, List<IGraphMutation> mutations, IdentifierFactory factory) : this(edge,factory)
        {
            var f =mutations.FirstOrDefault(p => p.GraphItem == @from)?.Identifier;
            var t =mutations.FirstOrDefault(p => p.GraphItem == to)?.Identifier;
            _query =  (@from?.Rid, to?.Rid, f, t) switch
            {
                (string fromV,string toV,_,_) =>
                new Let(
                    new Create(new Edge(), new GraphClass(Item.GetType()), new From(new Rid(fromV)), new To(new Rid(toV)), GetSetter()), Identifier),
                (null,string toV,Identifier fV,_) =>
                new Let(
                    new Create(new Edge(), new GraphClass(Item.GetType()), new From(fV), new To(new Rid(toV)), GetSetter()), Identifier),
                (string fromV,null,_,Identifier tV) =>
                new Let(
                    new Create(new Edge(), new GraphClass(Item.GetType()), new From(new Rid(fromV)), new To(tV), GetSetter()), Identifier),
                (null,null,Identifier fV,Identifier tV) =>
                new Let(
                    new Create(new Edge(), new GraphClass(Item.GetType()), new From(fV), new To(tV), GetSetter()), Identifier),
                _=> throw new InvalidOperationException()
            }
           ;
            
        }

        public override void Apply(ReadonlyGraph graph)
        {
            E dbItem = DbItem;
            if(dbItem == null)
                throw new InvalidOperationException($"edge {Item.GetType().Name} was not created");
            
            Item.FromV = graph.FindById(DbItem.FromRid) as V ?? throw  new NullReferenceException();
            Item.ToV = graph.FindById(DbItem.ToRid) as V ?? throw  new NullReferenceException();

            Item.In = Item.ToV.Rid;
            Item.Out = Item.FromV.Rid;
            
            Item.FromV.OutE.Add(Item);
            Item.ToV.InE.Add(Item);
            
            Item.Rid = DbItem.Rid;
            graph.GraphItems[Item.GetType()].Add(Item);
        }

        private Set GetSetter()
        {
            var props = new[] {nameof(E.Out), nameof(E.In),nameof(E.ToRid), nameof(E.FromRid),nameof(E.FromV),nameof(E.ToV)};
            var setters =
                Item.GetType()
                    .GetProperties()
                    .Where(p => p.DeclaringType == Item.GetType() && !props.Contains(p.Name))
                    .Select(p => new {p.Name,p.PropertyType, Value = p.GetValue(Item)})
                    .Select(p => new BinaryExpr(p.Name,p.Value,p.PropertyType))
                    .ToArray();
            
            return new Set(setters);
        }

        public override OrientSqlExpr Query => _query;
          

    }
}