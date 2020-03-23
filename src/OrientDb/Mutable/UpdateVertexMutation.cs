using System;
using System.Linq.Expressions;
using OrientDb.Common;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class UpdateVertexMutation<T>:UpdateMutation<T> where T : V
    {
        private Update _update;
        public UpdateVertexMutation(T item,IdentifierFactory identifierFactory) : base(item,identifierFactory)
        {
            _update= new Update(new Vertex(), new Rid(item.Rid), new Set(null));
        }

        public override void Apply(ReadonlyGraph graph)
        {
            V dbItem = DbItem;
            if(dbItem == null)
                throw new InvalidOperationException($"vertex {Item.GetType().Name} with @rid = {Item.Rid} was not exists");
            
            Sets.ForEach(p=>p(Item));
        }


        public override OrientSqlExpr Query => _update;

        
        
        internal override void Reset<TValue>(Expression<Func<T, TValue>> selector, TValue value)
        {
            var leftPart = selector.GetMemberFromExpression();
            var right = value;
            var left = leftPart.Name;
               
            _update =  new Update(_update,new Set(new []{new BinaryExpr(left,right,typeof(TValue) )}));
            
            Sets.Add(p=>Set(selector,Item,value));
        }
    }
}