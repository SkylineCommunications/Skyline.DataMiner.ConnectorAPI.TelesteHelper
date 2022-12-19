namespace Skyline.DataMiner.TelesteHelper.Parsing
{
    using Skyline.DataMiner.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public static class Parsers
	{
		public static string ParseMacAddress(byte[] macAddress)
		{
			if (macAddress?.Length != 6) throw new ArgumentException(nameof(macAddress));
			return String.Join(String.Empty, macAddress.Select(x => BitConverter.ToString(new byte[] { x }).Replace("-", String.Empty)));
		}

		public static uint ParseReplies(byte[] replies)
		{
			if (replies?.Length != 4) throw new ArgumentException(nameof(replies));
			return BitConverter.ToUInt32(replies.Reverse().ToArray(), 0);
		}

		public static ushort ParseFailures(byte[] failures)
		{
			if (failures?.Length != 2) throw new ArgumentException(nameof(failures));
			return BitConverter.ToUInt16(failures.Reverse().ToArray(), 0);
		}

		public static int ParseTxLevelToDecibelMicroVolt(byte txLevel)
		{
			// Ex. 163: 1 0 1 0 0 0 1 1
			// First bit is 1 -> remove 64 = value in dBµV
			// 99 dBµV = 39 dBmV (-60)

			// Ex. 21: 0 0 0 1 0 1 0 1
			// First bit is 0 -> add 64 = value in dBµV
			// 85 dBµV = 25 dBmV (-60)
			int value = txLevel;
			return value > 128 ? value - 64 : value + 64;
		}

		public static int ParseTxLevelToDecibelMilliVolt(byte txLevel)
		{
			// Ex. 163: 1 0 1 0 0 0 1 1
			// First bit is 1 -> remove 64 = value in dBµV
			// 99 dBµV = 39 dBmV (-60)

			// Ex. 21: 0 0 0 1 0 1 0 1
			// First bit is 0 -> add 64 = value in dBµV
			// 85 dBµV = 25 dBmV (-60)
			int value = txLevel;
			return value > 128 ? value - 124 : value + 4;
		}

		public static int ParseRxLevelToDecibelMicroVolt(byte rxLevel)
		{
			int value = rxLevel;
			return value;
		}

		public static int ParseRxLevelToDecibelMilliVolt(byte rxLevel)
		{
			int value = rxLevel;
			return value - 60;
		}

		public static DateTime ParseRegistrationTime(SLProtocol protocol, byte[] registrationTime, int uptimeParameterId)
		{
			if (registrationTime?.Length != 4) throw new ArgumentException(nameof(registrationTime));
			if (protocol.IsEmpty(uptimeParameterId)) return default(DateTime);

			uint secondsSinceUptime = ConversionHelper.ToUInt32(registrationTime, 0, Endianness.BigEndian);

			DateTime currentTime = DateTime.Now;
			double uptime = Convert.ToDouble(protocol.GetParameter(uptimeParameterId));
			DateTime date2005 = new DateTime(2005, 1, 1, 0, 0, 0, 0);
			DateTime date1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

			TimeSpan interval = TimeSpan.FromSeconds(uptime);
			DateTime difference1 = currentTime.Subtract(interval);

			DateTime date = date1970.AddSeconds(secondsSinceUptime);
			System.TimeSpan difference2 = date.Subtract(date2005);

			return difference1.Add(difference2);
		}

		public static string ParseToString(byte[] type)
		{
			return String.Join(String.Empty, type.Select(x => (char)x));
		}

		public static string ParseSoftwareVersion(byte[] softwareVersion)
		{
			if (softwareVersion?.Length != 2) throw new ArgumentException(nameof(softwareVersion));

			int[] versionNumbers = new int[]
			{
				softwareVersion[0] >> 4,
				softwareVersion[0] & 15,
				softwareVersion[1]
			};

			List<int> versionNumbersToInclude = new List<int>();
			bool shouldBeSkipped = true;
			foreach (int versionNumber in versionNumbers)
			{
				if (versionNumber == 0 && shouldBeSkipped) continue;
				shouldBeSkipped = false;
				versionNumbersToInclude.Add(versionNumber);
			}

			return String.Join(".", versionNumbersToInclude);
		}

		public static IPAddress ParseSubnetMask(byte subnetMask)
		{
			int maskLength = Int32.Parse(BitConverter.ToString(new byte[] { subnetMask }));
			uint mask = 0;
			for (int i = 0; i < 32; i++)
			{
				if (i < maskLength) mask += 1;
				mask = mask << 1;
			}

			return new IPAddress(BitConverter.GetBytes(mask).Reverse().ToArray());
		}
	}
}
