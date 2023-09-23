using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class ForceCLSStateRequest : gameScriptableSystemRequest
	{
		[Ordinal(0)] 
		[RED("state")] 
		public CEnum<ECLSForcedState> State
		{
			get => GetPropertyValue<CEnum<ECLSForcedState>>();
			set => SetPropertyValue<CEnum<ECLSForcedState>>(value);
		}

		[Ordinal(1)] 
		[RED("sourceName")] 
		public CName SourceName
		{
			get => GetPropertyValue<CName>();
			set => SetPropertyValue<CName>(value);
		}

		[Ordinal(2)] 
		[RED("priority")] 
		public CEnum<EPriority> Priority
		{
			get => GetPropertyValue<CEnum<EPriority>>();
			set => SetPropertyValue<CEnum<EPriority>>(value);
		}

		[Ordinal(3)] 
		[RED("removePreviousRequests")] 
		public CBool RemovePreviousRequests
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		[Ordinal(4)] 
		[RED("savable")] 
		public CBool Savable
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		public ForceCLSStateRequest()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
