using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class SoundSystemControllerPS : MasterControllerPS
	{
		[Ordinal(108)] 
		[RED("defaultAction")] 
		public CInt32 DefaultAction
		{
			get => GetPropertyValue<CInt32>();
			set => SetPropertyValue<CInt32>(value);
		}

		[Ordinal(109)] 
		[RED("soundSystemSettings")] 
		public CArray<SoundSystemSettings> SoundSystemSettings
		{
			get => GetPropertyValue<CArray<SoundSystemSettings>>();
			set => SetPropertyValue<CArray<SoundSystemSettings>>(value);
		}

		[Ordinal(110)] 
		[RED("currentEvent")] 
		public CHandle<ChangeMusicAction> CurrentEvent
		{
			get => GetPropertyValue<CHandle<ChangeMusicAction>>();
			set => SetPropertyValue<CHandle<ChangeMusicAction>>(value);
		}

		[Ordinal(111)] 
		[RED("cachedEvent")] 
		public CHandle<ChangeMusicAction> CachedEvent
		{
			get => GetPropertyValue<CHandle<ChangeMusicAction>>();
			set => SetPropertyValue<CHandle<ChangeMusicAction>>(value);
		}

		public SoundSystemControllerPS()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
