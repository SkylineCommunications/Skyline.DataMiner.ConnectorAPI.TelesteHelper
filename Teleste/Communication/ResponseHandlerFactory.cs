namespace Skyline.DataMiner.TelesteHelper.Communication
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Scripting;
    using Skyline.DataMiner.TelesteHelper.Extensions;

    public abstract class ResponseHandlerFactory
	{
		protected abstract IReadOnlyDictionary<int, IResponseHandler> Handlers { get; }

		public IResponseHandler GenerateHandler(SLProtocol protocol)
		{
			return GenerateHandler(protocol, protocol.GetTriggerParameter());
		}

		public IResponseHandler GenerateHandler(SLProtocol protocol, int trigger)
		{
			if (!Handlers.TryGetValue(trigger, out IResponseHandler handler))
			{
				protocol.LogError(nameof(ResponseHandlerFactory), nameof(GenerateHandler), $"No response handler defined for trigger {trigger}");
				return null;
			}

			return handler;
		}
	}
}
