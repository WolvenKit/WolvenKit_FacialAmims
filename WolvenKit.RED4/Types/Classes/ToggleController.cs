using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class ToggleController : inkToggleController
	{
		[Ordinal(16)] 
		[RED("label")] 
		public inkTextWidgetReference Label
		{
			get => GetPropertyValue<inkTextWidgetReference>();
			set => SetPropertyValue<inkTextWidgetReference>(value);
		}

		[Ordinal(17)] 
		[RED("icon")] 
		public inkImageWidgetReference Icon
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(18)] 
		[RED("data")] 
		public CInt32 Data
		{
			get => GetPropertyValue<CInt32>();
			set => SetPropertyValue<CInt32>(value);
		}

		public ToggleController()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
