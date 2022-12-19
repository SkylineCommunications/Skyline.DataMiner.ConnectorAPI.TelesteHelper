namespace Skyline.DataMiner.TelesteHelper.Communication
{
	using Skyline.DataMiner.Scripting;

	public interface ICommandHandler
	{
		int ExecuteTrigger { get; }

		bool BuildCommand(SLProtocol protocol);
	}
}
