namespace Skyline.DataMiner.TelesteHelper.Extensions.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
	public sealed class IndexAttribute : Attribute
	{
		public IndexAttribute(uint index)
		{
			Index = index;
		}

		public uint Index { get; private set; }
	}
}
