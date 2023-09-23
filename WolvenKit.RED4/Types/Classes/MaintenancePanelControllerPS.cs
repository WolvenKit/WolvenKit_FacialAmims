using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class MaintenancePanelControllerPS : MasterControllerPS
	{
		[Ordinal(108)] 
		[RED("maintenancePanelSkillChecks")] 
		public CHandle<EngineeringContainer> MaintenancePanelSkillChecks
		{
			get => GetPropertyValue<CHandle<EngineeringContainer>>();
			set => SetPropertyValue<CHandle<EngineeringContainer>>(value);
		}

		public MaintenancePanelControllerPS()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
