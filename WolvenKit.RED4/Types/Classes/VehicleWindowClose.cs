using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class VehicleWindowClose : ActionBool
	{
		[Ordinal(38)] 
		[RED("slotID")] 
		public CName SlotID
		{
			get => GetPropertyValue<CName>();
			set => SetPropertyValue<CName>(value);
		}

		[Ordinal(39)] 
		[RED("speed")] 
		public CName Speed
		{
			get => GetPropertyValue<CName>();
			set => SetPropertyValue<CName>(value);
		}

		[Ordinal(40)] 
		[RED("isInteractionSource")] 
		public CBool IsInteractionSource
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		public VehicleWindowClose()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
