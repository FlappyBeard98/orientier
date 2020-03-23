using System;
using Newtonsoft.Json;

namespace OrientDb.Tests.MatchingGraph 
{

	public class SkuNameHasStatement : E <FilteredSkuName,SkuNameStatement>
	{
		[JsonConstructor]
		public SkuNameHasStatement(Nullable<System.Boolean> isOptional,System.Boolean useLevenstein,System.Boolean isQuoted)
		{
			IsOptional = isOptional; 
			UseLevenstein = useLevenstein; 
			IsQuoted = isQuoted; 
		}

		public Nullable<System.Boolean> IsOptional {get;private set; }
		public System.Boolean UseLevenstein {get;private set; }
		public System.Boolean IsQuoted {get;private set; } 
	}

	public class CategoryHasChild : E <Category,Category>
	{
		[JsonConstructor]
		public CategoryHasChild()
		{
		}
 
	}

	public class SkuBelongsToEssence : E <OriginalSkuName,Essence>
	{
		[JsonConstructor]
		public SkuBelongsToEssence()
		{
		}
 
	}

	public class TrademarkElement : V 
	{
		[JsonConstructor]
		public TrademarkElement(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class SkuBelongsToCategory : E <OriginalSkuName,Category>
	{
		[JsonConstructor]
		public SkuBelongsToCategory(System.String relationType)
		{
			RelationType = relationType; 
		}

		public System.String RelationType {get;private set; } 
	}

	public class ParentTrademark : V 
	{
		[JsonConstructor]
		public ParentTrademark(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class Client : V 
	{
		[JsonConstructor]
		public Client(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class SkuNameStatement : V 
	{
		[JsonConstructor]
		public SkuNameStatement(System.Boolean isInvalidNumber,System.Boolean isValidNumber,System.Boolean wasImported,System.Byte operatorCheckStatus,System.Boolean isSingleAlphaWord,System.Boolean isSingleWord,System.Boolean isMeasure,System.String text)
		{
			IsInvalidNumber = isInvalidNumber; 
			IsValidNumber = isValidNumber; 
			WasImported = wasImported; 
			OperatorCheckStatus = operatorCheckStatus; 
			IsSingleAlphaWord = isSingleAlphaWord; 
			IsSingleWord = isSingleWord; 
			IsMeasure = isMeasure; 
			Text = text; 
		}

		public System.Boolean IsInvalidNumber {get;private set; }
		public System.Boolean IsValidNumber {get;private set; }
		public System.Boolean WasImported {get;private set; }
		public System.Byte OperatorCheckStatus {get;private set; }
		public System.Boolean IsSingleAlphaWord {get;private set; }
		public System.Boolean IsSingleWord {get;private set; }
		public System.Boolean IsMeasure {get;private set; }
		public System.String Text {get;private set; } 
	}

	public class SkuOriginalNameToGenerikTrademark : E <GenericTrademark,OriginalSkuName>
	{
		[JsonConstructor]
		public SkuOriginalNameToGenerikTrademark()
		{
		}
 
	}

	public class SkuNameHasBarcode : E <OriginalSkuName,Barcode>
	{
		[JsonConstructor]
		public SkuNameHasBarcode()
		{
		}
 
	}

	public class GenericTrademarkHasAlias : E <GenericTrademark,StatementAlias>
	{
		[JsonConstructor]
		public GenericTrademarkHasAlias()
		{
		}
 
	}

	public class ClientToSkuOriginalName : E <Client,OriginalSkuName>
	{
		[JsonConstructor]
		public ClientToSkuOriginalName()
		{
		}
 
	}

	public class EssenceHasAlias : E <Essence,StatementAlias>
	{
		[JsonConstructor]
		public EssenceHasAlias(Nullable<System.Boolean> useLevenstein)
		{
			UseLevenstein = useLevenstein; 
		}

		public Nullable<System.Boolean> UseLevenstein {get;private set; } 
	}

	public class GenericTrademark : V 
	{
		[JsonConstructor]
		public GenericTrademark(Nullable<System.Boolean> isManufacturer,System.Boolean makeNoise,System.String name,System.Boolean followSequence)
		{
			IsManufacturer = isManufacturer; 
			MakeNoise = makeNoise; 
			Name = name; 
			FollowSequence = followSequence; 
		}

		public Nullable<System.Boolean> IsManufacturer {get;private set; }
		public System.Boolean MakeNoise {get;private set; }
		public System.String Name {get;private set; }
		public System.Boolean FollowSequence {get;private set; } 
	}

	public class OriginalSkuName : V 
	{
		[JsonConstructor]
		public OriginalSkuName(System.Boolean isNeedle,System.String text,System.String displayName)
		{
			IsNeedle = isNeedle; 
			Text = text; 
			DisplayName = displayName; 
		}

		public System.Boolean IsNeedle {get;private set; }
		public System.String Text {get;private set; }
		public System.String DisplayName {get;private set; } 
	}

	public class ReceiptHasOriginalName : E <Receipt,OriginalSkuName>
	{
		[JsonConstructor]
		public ReceiptHasOriginalName()
		{
		}
 
	}

	public class TrademarkElementToGeneric : E <TrademarkElement,GenericTrademark>
	{
		[JsonConstructor]
		public TrademarkElementToGeneric(System.Int32 indexNumber,Nullable<System.Boolean> isRequired,Nullable<System.Boolean> isAliasAviable,Nullable<System.Boolean> isLivAviable)
		{
			IndexNumber = indexNumber; 
			IsRequired = isRequired; 
			IsAliasAviable = isAliasAviable; 
			IsLivAviable = isLivAviable; 
		}

		public System.Int32 IndexNumber {get;private set; }
		public Nullable<System.Boolean> IsRequired {get;private set; }
		public Nullable<System.Boolean> IsAliasAviable {get;private set; }
		public Nullable<System.Boolean> IsLivAviable {get;private set; } 
	}

	public class SkuNameFiltered : E <OriginalSkuName,FilteredSkuName>
	{
		[JsonConstructor]
		public SkuNameFiltered()
		{
		}
 
	}

	public class FilteredSkuName : V 
	{
		[JsonConstructor]
		public FilteredSkuName(System.String text,System.Int32 versionId)
		{
			Text = text; 
			VersionId = versionId; 
		}

		public System.String Text {get;private set; }
		public System.Int32 VersionId {get;private set; } 
	}

	public class AliasToBrandElement : E <StatementAlias,TrademarkElement>
	{
		[JsonConstructor]
		public AliasToBrandElement()
		{
		}
 
	}

	public class StatementAlias : V 
	{
		[JsonConstructor]
		public StatementAlias(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class Barcode : V 
	{
		[JsonConstructor]
		public Barcode(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class Category : V 
	{
		[JsonConstructor]
		public Category(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class StatementHasAlias : E <SkuNameStatement,StatementAlias>
	{
		[JsonConstructor]
		public StatementHasAlias()
		{
		}
 
	}

	public class Essence : V 
	{
		[JsonConstructor]
		public Essence(System.String name)
		{
			Name = name; 
		}

		public System.String Name {get;private set; } 
	}

	public class EssenceContainsCategory : E <Essence,Category>
	{
		[JsonConstructor]
		public EssenceContainsCategory(System.String relationType)
		{
			RelationType = relationType; 
		}

		public System.String RelationType {get;private set; } 
	}

	public class Receipt : V 
	{
		[JsonConstructor]
		public Receipt(System.String text)
		{
			Text = text; 
		}

		public System.String Text {get;private set; } 
	}

	public class TrademarkGenericToParent : E <GenericTrademark,ParentTrademark>
	{
		[JsonConstructor]
		public TrademarkGenericToParent()
		{
		}
 
	}
 }