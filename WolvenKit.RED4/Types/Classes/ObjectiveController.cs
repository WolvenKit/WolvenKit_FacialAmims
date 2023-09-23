using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class ObjectiveController : inkButtonController
	{
		[Ordinal(13)] 
		[RED("ObjectiveLabel")] 
		public inkTextWidgetReference ObjectiveLabel
		{
			get => GetPropertyValue<inkTextWidgetReference>();
			set => SetPropertyValue<inkTextWidgetReference>(value);
		}

		[Ordinal(14)] 
		[RED("ObjectiveStatus")] 
		public inkTextWidgetReference ObjectiveStatus
		{
			get => GetPropertyValue<inkTextWidgetReference>();
			set => SetPropertyValue<inkTextWidgetReference>(value);
		}

		[Ordinal(15)] 
		[RED("QuestIcon")] 
		public inkImageWidgetReference QuestIcon
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(16)] 
		[RED("TrackedIcon")] 
		public inkImageWidgetReference TrackedIcon
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(17)] 
		[RED("FrameBackground_On")] 
		public inkImageWidgetReference FrameBackground_On
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(18)] 
		[RED("FrameBackground_Off")] 
		public inkImageWidgetReference FrameBackground_Off
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(19)] 
		[RED("FrameFluff_On")] 
		public inkImageWidgetReference FrameFluff_On
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(20)] 
		[RED("FrameFluff_Off")] 
		public inkImageWidgetReference FrameFluff_Off
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(21)] 
		[RED("Folder_On")] 
		public inkImageWidgetReference Folder_On
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(22)] 
		[RED("Folder_Off")] 
		public inkImageWidgetReference Folder_Off
		{
			get => GetPropertyValue<inkImageWidgetReference>();
			set => SetPropertyValue<inkImageWidgetReference>(value);
		}

		[Ordinal(23)] 
		[RED("QuestObjectiveData")] 
		public CHandle<ABaseQuestObjectiveWrapper> QuestObjectiveData
		{
			get => GetPropertyValue<CHandle<ABaseQuestObjectiveWrapper>>();
			set => SetPropertyValue<CHandle<ABaseQuestObjectiveWrapper>>(value);
		}

		[Ordinal(24)] 
		[RED("ToTrack")] 
		public CWeakHandle<ABaseQuestObjectiveWrapper> ToTrack
		{
			get => GetPropertyValue<CWeakHandle<ABaseQuestObjectiveWrapper>>();
			set => SetPropertyValue<CWeakHandle<ABaseQuestObjectiveWrapper>>(value);
		}

		public ObjectiveController()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
