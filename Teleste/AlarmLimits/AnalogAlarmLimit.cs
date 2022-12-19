namespace Skyline.DataMiner.TelesteHelper.AlarmLimits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Skyline.DataMiner.TelesteHelper.Parsing;

    public class AnalogAlarmLimit : IAlarmLimit
	{
		protected virtual IReadOnlyDictionary<byte, IReadOnlyDictionary<byte, string>> ParameterNameMapping { get; }

		protected virtual IReadOnlyDictionary<byte, string> ParameterUnitMapping { get; }

		public static byte Type => 0;

		public byte ParamId { get; set; }

		public byte ParamIdx { get; set; }

		public AnalogAlarmState State { get; set; }

		public double StateValue { get; set; }

		public double Value { get; set; }

		public double HiHi { get; set; }

		public double Hi { get; set; }

		public double Lo { get; set; }

		public double LoLo { get; set; }

		public double Deadband { get; set; }

		public double MinValue { get; set; }

		public double MaxValue { get; set; }

		public Severity HiHiSeverity { get; set; }

		public Severity HiSeverity { get; set; }

		public Severity LoSeverity { get; set; }

		public Severity LoLoSeverity { get; set; }

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

		protected AnalogAlarmLimit(byte[] data, int startIndex) : this(data.Skip(startIndex).Take(23).ToArray())
        {

        }

		protected AnalogAlarmLimit(byte[] data)
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

			byte severityByte = data[position];
			LoLoSeverity = (Severity)(severityByte & 3);
			LoSeverity = (Severity)((severityByte & 12) >> 2);
			HiSeverity = (Severity)((severityByte & 48) >> 4);
			HiHiSeverity = (Severity)((severityByte & 192) >> 6);
		}

		public byte[] ToData()
		{
			byte[] data = new byte[23];

			data[0] = ParamId;
			data[1] = ParamIdx;
			data[2] = Type;
			data[3] = (byte)State;

			Array.Copy(ConversionHelper.GetBytes((short)(StateValue * 100d), Endianness.BigEndian), 0, data, 4, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Value * 100d), Endianness.BigEndian), 0, data, 6, 2);

			Array.Copy(ConversionHelper.GetBytes((short)(HiHi * 100d), Endianness.BigEndian), 0, data, 8, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Hi * 100d), Endianness.BigEndian), 0, data, 10, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Lo * 100d), Endianness.BigEndian), 0, data, 12, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(LoLo * 100d), Endianness.BigEndian), 0, data, 14, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(Deadband * 100d), Endianness.BigEndian), 0, data, 16, 2);

			Array.Copy(ConversionHelper.GetBytes((short)(MinValue * 100d), Endianness.BigEndian), 0, data, 18, 2);
			Array.Copy(ConversionHelper.GetBytes((short)(MaxValue * 100d), Endianness.BigEndian), 0, data, 20, 2);

			data[22] |= (byte)LoLoSeverity;
			data[22] |= (byte)((byte)LoSeverity << 2);
			data[22] |= (byte)((byte)HiSeverity << 4);
			data[22] |= (byte)((byte)HiHiSeverity << 6);

			return data;
		}
	}
}
