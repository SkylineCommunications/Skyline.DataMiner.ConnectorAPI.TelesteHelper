namespace Skyline.DataMiner.TelesteHelper.AlarmLimits
{
	public enum AnalogAlarmState
	{
		Nominal = 1,
		HiHi = 2,
		Hi = 3,
		Lo = 4,
		LoLo = 5
	}

	public enum DiscreteAlarmState
	{
		Nominal = 0,
		Major = 6,
		Minor = 7,
		Notification = 8
	}

	public enum Severity
	{
		Disabled = 0,
		Major = 1,
		Minor = 2,
		Notification = 3
	}
}
