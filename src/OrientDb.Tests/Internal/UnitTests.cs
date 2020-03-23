using System;
using OrientDb.Mutable;
using OrientDb.Queries;
using OrientDb.Tests.Model;
using Xunit;
using Array = OrientDb.Queries.Array;

namespace OrientDb.Tests.Internal
{
    public class UnitTests
    {
        [Fact]
        public void QueriesTest()
        {
            const string id = "#0:0";

            
            var createVertex = new Create(new Vertex(), new GraphClass<Va>(), new Set(new BinaryExpr[0]));
            var createEdge = new Create(new Edge(), new GraphClass<Eab>(), new From(new Rid(id)),
                new To(new Rid(id)), new Set(new BinaryExpr[0]));

            var updateVertex = new Update(new Vertex(), new Rid(id), new Set(new BinaryExpr[0]));
            var updateEdge = new Update(new Edge(), new Rid(id), new Set(new BinaryExpr[0]));

            var deleteVertex = new Delete(new Vertex(), new Rid(id));
            var deleteEdge = new Delete(new Edge(), new Rid(id));

            var selectAll = new Select(new From(new GraphClass<Va>()));
            var selectOne = new Select(new From(new GraphClass<Va>()), new Where(new Rid(id)));
            var x = new Array(new Identifier("x"),new Identifier("1"));
            var ret = new Return(new Let(new Identifier("x"),new Identifier("1")),new Let(new Identifier("y"),new Identifier("2")));
        }
        
        [Fact]
        public void MutationsTests()
        {

            var m = new CreateVertexMutation<Vc>(new Vc(),new IdentifierFactory());
            
            
        }
    }
}