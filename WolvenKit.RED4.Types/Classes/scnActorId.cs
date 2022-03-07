using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	[REDMeta]
	public partial class scnActorId : RedBaseClass
	{
		[Ordinal(0)] 
		[RED("id")] 
		public CUInt32 Id
		{
			get => GetPropertyValue<CUInt32>();
			set => SetPropertyValue<CUInt32>(value);
		}

		public scnActorId()
		{
			Id = 4294967295;
		}
	}
}