namespace Skyline.DataMiner.TelesteHelper.AlarmLimits
{
    using Skyline.DataMiner.TelesteHelper.Parsing;
    using System;
	using System.Collections.Generic;
    using System.Linq;

    public class AnalogAlarmLimitV2 : IAlarmLimit
	{
		// [ParameterID][ParameterIdx] => Property Name
		protected virtual IReadOnlyDictionary<byte, IReadOnlyDictionary<byte, string>> ParameterNameMapping { get; }

		// [ParameterID] => Property Unit
		protected virtual IReadOnlyDictionary<byte, string> ParameterUnitMapping { get; }

		public static byte Type => 0;

		public byte ParamId { get; protected set; }

		public byte ParamIdx { get; protected set; }

		public AnalogAlarmState State { get; protected set; }

		public double StateValue { get; protected set; }

		public double Value { get; protected set; }

		public double HiHi { get; set; }

		public double Hi { get; set; }

		public double Lo { get; set; }

		public double LoLo { get; set; }

		public double Deadband { get; set; }

		public double MinValue { get; protected set; }

		public double MaxValue { get; protected set; }

		public bool HiHiEnabled { get; set; }

		public bool HiEnabled { get; set; }

		public bool LoEnabled { get; set; }

		public bool LoLoEnabled { get; set; }

		public string Name
		{
			get
			{
				if (!ParameterNameMapping.TryGetValue(ParamId, out IReadOnlyDictionary<byte, string> namesMapping)) return Key;
				if (!namesMapping.TryGetValue(ParamIdx, out string name)) return Key;
				return name;
			}
		}

		public string Unit
		{
			get
			{
				return ParameterUnitMapping.TryGetValue(ParamId, out string unit) ? unit : "-1";
			}
		}

		public string Key
		{
			get
			{
				return $"{ParamId}.{ParamIdx}";
			}
		}

		public AnalogAlarmLimitV2()
		{

		}

		public AnalogAlarmLimitV2(byte[] data, int startIndex) : this(data.Skip(startIndex).Take(23).ToArray())
        {

        }

		public AnalogAlarmLimitV2(byte[] data)
		{
			if (data.Length != 23) throw new ArgumentException($"The data of an Analog Alarm Limit should always be 23 bytes long", nameof(data));

			int position = 0;

			ParamId = data[position];
			position++;

			ParamIdx = data[position];
			position++;

			byte type = data[position];
			position++;

			if (type != Type) throw new ArgumentException($"Provided data does not represent an Analog Alarm Limit", nameof(data));

			State = (AnalogAlarmState)data[position];
			position++;

			StateValue = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			Value = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			HiHi = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			Hi = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			Lo = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			LoLo = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			Deadband = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			MinValue = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			MaxValue = ConversionHelper.ToInt16(data, position, Endianness.BigEndian) / 100d;
			position += 2;

			byte enabledByte = data[position];
			LoLoEnabled = ConversionHelper.IsBitSet(enabledByte, 7);
			LoEnabled = ConversionHelper.IsBitSet(enabledByte, 5);
			HiEnabled = ConversionHelper.IsBitSet(enabledByte, 3);
			HiHiEnabled = ConversionHelper.IsBitSet(enabledByte, 1);
		}

		public byte[] ToData()
		{
			byte[] data = new byte[23];

			data[0] = ParamId;
			data[1] = ParamIdx;

			data[2] = 0;
			data[3] = 0;
			data[4] = 0;
			data[5] = 0;
			data[6] = 0;
			data[7] = 0;

			Array.Copy(ConversionHelper.GetBytes((short)(HiHi * 100d), Endianness.BigEndian), 0, data, 8, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Hi * 100d), Endianness.BigEndian), 0, data, 10, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Lo * 100d), Endianness.BigEndian), 0, data, 12, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(LoLo * 100d), Endianness.BigEndian), 0, data, 14, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Deadband * 100d), Endianness.BigEndian), 0, data, 16, 2);

			data[18] = 0;
			data[19] = 0;
			data[20] = 0;
			data[21] = 0;

			data[22] |= Convert.ToByte(LoLoEnabled);
			data[22] |= (byte)(Convert.ToByte(LoEnabled) << 2);
			data[22] |= (byte)(Convert.ToByte(HiEnabled) << 4);
			data[22] |= (byte)(Convert.ToByte(HiHiEnabled) << 6);

			return data;
		}
	}
}
