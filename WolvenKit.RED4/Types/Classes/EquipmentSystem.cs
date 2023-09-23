using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class EquipmentSystem : gameIEquipmentSystem
	{
		[Ordinal(0)] 
		[RED("ownerData")] 
		public CArray<CHandle<EquipmentSystemPlayerData>> OwnerData
		{
			get => GetPropertyValue<CArray<CHandle<EquipmentSystemPlayerData>>>();
			set => SetPropertyValue<CArray<CHandle<EquipmentSystemPlayerData>>>(value);
		}

		public EquipmentSystem()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
