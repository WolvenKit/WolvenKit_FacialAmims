using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class InventoryItemDataWrapper : IScriptable
	{
		[Ordinal(0)] 
		[RED("ItemData")] 
		public gameInventoryItemData ItemData
		{
			get => GetPropertyValue<gameInventoryItemData>();
			set => SetPropertyValue<gameInventoryItemData>(value);
		}

		[Ordinal(1)] 
		[RED("SortData")] 
		public gameInventoryItemSortData SortData
		{
			get => GetPropertyValue<gameInventoryItemSortData>();
			set => SetPropertyValue<gameInventoryItemSortData>(value);
		}

		[Ordinal(2)] 
		[RED("HasSortDataBuilt")] 
		public CBool HasSortDataBuilt
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		public InventoryItemDataWrapper()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
