using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class ChestPress : InteractiveDevice
	{
		[Ordinal(98)] 
		[RED("animFeatureData")] 
		public CHandle<AnimFeature_ChestPress> AnimFeatureData
		{
			get => GetPropertyValue<CHandle<AnimFeature_ChestPress>>();
			set => SetPropertyValue<CHandle<AnimFeature_ChestPress>>(value);
		}

		[Ordinal(99)] 
		[RED("animFeatureDataName")] 
		public CName AnimFeatureDataName
		{
			get => GetPropertyValue<CName>();
			set => SetPropertyValue<CName>(value);
		}

		public ChestPress()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
