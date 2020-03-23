namespace OrientDb.Tests.MatchingGraph
{
    public class GnomGraph : Graph 
    {
        public GnomGraph(GraphSettings settings) : base(settings)
        {
        }

        protected override void Setup(GraphSetup setup)
        {
            setup
                .Add<ParentTrademark>()
                .Add<Category>()
                .Add<SkuBelongsToCategory>()
                .Add<AliasToBrandElement>()
                .Add<CategoryHasChild>()
                .Add<ReceiptHasOriginalName>()
                .Add<TrademarkElement>()
                .Add<GenericTrademark>()
                .Add<OriginalSkuName>()
                .Add<Barcode>()
                .Add<StatementHasAlias>()
                .Add<FilteredSkuName>()
                .Add<SkuOriginalNameToGenerikTrademark>()
                .Add<Receipt>()
                .Add<SkuNameHasStatement>()
                .Add<SkuNameFiltered>()
                .Add<TrademarkGenericToParent>()
                .Add<SkuNameHasBarcode>()
                .Add<Essence>()
                .Add<Client>()
                .Add<EssenceHasAlias>()
                .Add<ClientToSkuOriginalName>()
                .Add<SkuBelongsToEssence>()
                .Add<StatementAlias>()
                .Add<SkuNameStatement>()
                .Add<TrademarkElementToGeneric>()
                .Add<EssenceContainsCategory>()
                .Add<GenericTrademarkHasAlias>();
        }
    }
}