using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	public partial class DelayedOperationEvent : redEvent
	{
		[Ordinal(0)] 
		[RED("operationHandler")] 
		public CHandle<DeviceOperations> OperationHandler
		{
			get => GetPropertyValue<CHandle<DeviceOperations>>();
			set => SetPropertyValue<CHandle<DeviceOperations>>(value);
		}

		[Ordinal(1)] 
		[RED("operation")] 
		public SBaseDeviceOperationData Operation
		{
			get => GetPropertyValue<SBaseDeviceOperationData>();
			set => SetPropertyValue<SBaseDeviceOperationData>(value);
		}

		public DelayedOperationEvent()
		{
			PostConstruct();
		}

		partial void PostConstruct();
	}
}
