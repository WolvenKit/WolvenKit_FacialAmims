using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class HealthConsumable : gameCpoPickableItem
	{
		[Ordinal(38)] 
		[RED("interactionComponent")] 
		public CHandle<gameinteractionsComponent> InteractionComponent
		{
			get => GetPropertyValue<CHandle<gameinteractionsComponent>>();
			set => SetPropertyValue<CHandle<gameinteractionsComponent>>(value);
		}

		[Ordinal(39)] 
		[RED("meshComponent")] 
		public CHandle<entMeshComponent> MeshComponent
		{
			get => GetPropertyValue<CHandle<entMeshComponent>>();
			set => SetPropertyValue<CHandle<entMeshComponent>>(value);
		}

		[Ordinal(40)] 
		[RED("disappearAfterEquip")] 
		public CBool DisappearAfterEquip
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		[Ordinal(41)] 
		[RED("respawnTime")] 
		public CFloat RespawnTime
		{
			get => GetPropertyValue<CFloat>();
			set => SetPropertyValue<CFloat>(value);
		}

		public HealthConsumable()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
