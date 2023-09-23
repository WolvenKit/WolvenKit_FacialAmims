using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class SetControllerStimSource : AIbehaviortaskScript
	{
		[Ordinal(0)] 
		[RED("investigateData")] 
		public senseStimInvestigateData InvestigateData
		{
			get => GetPropertyValue<senseStimInvestigateData>();
			set => SetPropertyValue<senseStimInvestigateData>(value);
		}

		public SetControllerStimSource()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
