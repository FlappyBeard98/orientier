using System;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using OrientDb.Mutable;
using OrientDb.Tests.MatchingGraph;
using Xunit;
using Xunit.Abstractions;

namespace OrientDb.Tests
{
    public class CommonTests 
    { 
       
        private readonly ITestOutputHelper _output;

        public CommonTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        private static readonly GraphSettings GraphSettings = 
            new GraphSettings(Mode.WriteThrough,"g4c-baltika","gnom",2480,"root","rootpwd");
        
        [Fact]
        public async void ConnectToDatabase()
        {
            var graph = new GnomGraph(GraphSettings);
            await graph.Connect();
            var act = graph.GetConnection();
            Assert.NotNull(act);
        }
        
        [Fact]
        public async void ReadSchemeFromDatabase()
        {
            var graph = new GnomGraph(GraphSettings);
            var act = await graph.ReadScheme();
            Assert.NotNull(act);
        }
        
        [Fact]
        public async void GenerateCodeFromScheme()
        {
            var graph = new GnomGraph(GraphSettings);
            var scheme = await graph.ReadScheme();
            var act = CodeGen.CodeGen.Generate(scheme, "OrientDb.Tests.MatchingGraph");
            Assert.NotNull(act);
            _output.WriteLine(act);
        }
        
        [Fact]
        public async void Setup()
        {
            var graph = new GnomGraph(GraphSettings);
            await graph.Setup();
            
            var act =graph.V<ParentTrademark>();;
            Assert.NotNull(act);
        }

        

        [Fact]
        public async void WhenInsertVerticesConnectedWithEdgeThenAllOfItInsertedTest()
        {
            var graph = new GnomGraph(GraphSettings);
            await graph.Setup();
            var essence = new Essence($"sample esence {Guid.NewGuid()}");
            var sku = new OriginalSkuName(true,$"sample sku {Guid.NewGuid()}",null);
            var skuBelongsToEssence = new SkuBelongsToEssence();
            
            using (var g = graph.Mutate())
            {
                g.CreateVertex(essence); 
                g.CreateVertex(sku); 
                g.CreateEdge(sku,skuBelongsToEssence,essence);
            }
            
            Assert.NotNull(essence.Rid);
            Assert.NotNull(sku.Rid);
            Assert.NotNull(skuBelongsToEssence.Rid);
            
            using (var g = graph.Mutate())
            {
                g.DeleteVertex(essence); 
                g.DeleteVertex(sku); 
            }
            
        }
        
        [Fact]
        public async void UpdateTest()
        {
            var graph = new GnomGraph(GraphSettings);
            await graph.Setup();
            
            var essence = graph.V<Essence>().FirstOrDefault();
            
            using (var g = graph.Mutate())
            {
                var x = new SkuBelongsToEssence();
                g.UpdateEdge(x)
                 .Set(p=>p.ToRid,Guid.NewGuid().ToString()); 
            }
        }


    }
   
}