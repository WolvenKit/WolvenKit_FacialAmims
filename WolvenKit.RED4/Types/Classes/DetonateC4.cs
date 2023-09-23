using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class DetonateC4 : ActionBool
	{
		[Ordinal(38)] 
		[RED("itemID")] 
		public gameItemID ItemID
		{
			get => GetPropertyValue<gameItemID>();
			set => SetPropertyValue<gameItemID>(value);
		}

		public DetonateC4()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
