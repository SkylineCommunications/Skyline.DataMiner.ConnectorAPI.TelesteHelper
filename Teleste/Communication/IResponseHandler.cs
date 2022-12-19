namespace Skyline.DataMiner.TelesteHelper.Communication
{
	using Skyline.DataMiner.Scripting;

	public interface IResponseHandler
	{
		void HandleResponse(SLProtocol protocol);
	}
}
