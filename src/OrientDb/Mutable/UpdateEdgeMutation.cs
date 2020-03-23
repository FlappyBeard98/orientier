using System;
using System.Linq;
using System.Linq.Expressions;
using OrientDb.Common;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class UpdateEdgeMutation<T>:UpdateMutation<T> where T : E
    {
        private Update _update;
        public UpdateEdgeMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
            _update= new Update(new Edge(), new Rid(item.Rid), new Set(null));
        }

        public override void Apply(ReadonlyGraph graph)
        {  
            E dbItem = DbItem;
            if(dbItem == null)
                throw new InvalidOperationException($"edge {Item.GetType().Name} with @rid = {Item.Rid} was not exists");
            
            Item.FromV.OutE.Remove(Item);
            Item.ToV.InE.Remove(Item);
            
            Sets.ForEach(p=>p(Item));
            
            Item.FromV.OutE.Add(Item);
            Item.ToV.InE.Add(Item);
        }

        public override OrientSqlExpr Query  => _update;
        
        internal override void Reset<TValue>(Expression<Func<T, TValue>> selector, TValue value)
        {
            string GetLeft(string propName) => propName switch
            {
                nameof(E.ToRid) => nameof(E.In).ToLower(),
                nameof(E.FromRid) => nameof(E.Out).ToLower(),
                { } s => s,
                _ => throw new ArgumentException()
            };
            var leftPart = selector.GetMemberFromExpression();
            var right = value;
            var left = GetLeft(leftPart.Name);
            
            _update =  new Update(_update,new Set(new []{new BinaryExpr(left,right,typeof(TValue) )}));
            
            Sets.Add(p=>Set(selector,Item,value));
        }
    }
}