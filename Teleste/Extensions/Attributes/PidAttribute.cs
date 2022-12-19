namespace Skyline.DataMiner.TelesteHelper.Extensions
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
	public sealed class PidAttribute : Attribute
	{
		public PidAttribute(int paramId)
		{
			Pid = paramId;
		}

		public int Pid { get; private set; }
	}
}
