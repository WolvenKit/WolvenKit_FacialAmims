using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class UiRadialContextEvents : InputContextTransitionEvents
	{
		[Ordinal(8)] 
		[RED("mouse")] 
		public Vector4 Mouse
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		public UiRadialContextEvents()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
