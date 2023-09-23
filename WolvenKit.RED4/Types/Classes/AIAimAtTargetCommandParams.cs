using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class AIAimAtTargetCommandParams : questScriptedAICommandParams
	{
		[Ordinal(0)] 
		[RED("targetOverrideNodeRef")] 
		public NodeRef TargetOverrideNodeRef
		{
			get => GetPropertyValue<NodeRef>();
			set => SetPropertyValue<NodeRef>(value);
		}

		[Ordinal(1)] 
		[RED("targetOverridePuppetRef")] 
		public gameEntityReference TargetOverridePuppetRef
		{
			get => GetPropertyValue<gameEntityReference>();
			set => SetPropertyValue<gameEntityReference>(value);
		}

		[Ordinal(2)] 
		[RED("duration")] 
		public CFloat Duration
		{
			get => GetPropertyValue<CFloat>();
			set => SetPropertyValue<CFloat>(value);
		}

		public AIAimAtTargetCommandParams()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
