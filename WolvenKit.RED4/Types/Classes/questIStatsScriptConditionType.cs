using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public abstract partial class questIStatsScriptConditionType : questIStatsConditionType
	{
		[Ordinal(1)] 
		[RED("scriptCondition")] 
		public CHandle<IScriptable> ScriptCondition
		{
			get => GetPropertyValue<CHandle<IScriptable>>();
			set => SetPropertyValue<CHandle<IScriptable>>(value);
		}

		public questIStatsScriptConditionType()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
