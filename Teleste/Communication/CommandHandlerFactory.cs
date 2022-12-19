namespace Skyline.DataMiner.TelesteHelper.Communication
{
    using Skyline.DataMiner.Scripting;
    using Skyline.DataMiner.TelesteHelper.Extensions;
    using System.Collections.Generic;

    public abstract class CommandHandlerFactory
	{
		protected abstract IReadOnlyDictionary<int, ICommandHandler> Handlers { get; }

		public ICommandHandler GenerateHandler(SLProtocol protocol)
		{
			return GenerateHandler(protocol, protocol.GetTriggerParameter());
		}

		public ICommandHandler GenerateHandler(SLProtocol protocol, int trigger)
		{
			if (!Handlers.TryGetValue(trigger, out ICommandHandler handler))
			{
				protocol.LogError(nameof(CommandHandlerFactory), nameof(GenerateHandler), $"No command handler defined for trigger {trigger}");
				return null;
			}

			return handler;
		}
	}
}
