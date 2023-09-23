using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class worldgeometryHandIKDescriptionResult : RedBaseClass
	{
		[Ordinal(0)] 
		[RED("grabPointStart")] 
		public Vector4 GrabPointStart
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		[Ordinal(1)] 
		[RED("grabPointEnd")] 
		public Vector4 GrabPointEnd
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		public worldgeometryHandIKDescriptionResult()
		{
			GrabPointStart = new Vector4 { W = 1.000000F };
			GrabPointEnd = new Vector4 { W = 1.000000F };

			PostConstruct();
		}

		partial void PostConstruct();
	}
}
