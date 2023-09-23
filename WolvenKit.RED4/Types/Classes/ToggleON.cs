using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class ToggleON : ActionBool
	{
		[Ordinal(38)] 
		[RED("TrueRecordName")] 
		public CString TrueRecordName
		{
			get => GetPropertyValue<CString>();
			set => SetPropertyValue<CString>(value);
		}

		[Ordinal(39)] 
		[RED("FalseRecordName")] 
		public CString FalseRecordName
		{
			get => GetPropertyValue<CString>();
			set => SetPropertyValue<CString>(value);
		}

		public ToggleON()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
