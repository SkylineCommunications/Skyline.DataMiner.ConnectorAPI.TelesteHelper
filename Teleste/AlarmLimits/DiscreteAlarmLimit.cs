namespace Skyline.DataMiner.TelesteHelper.AlarmLimits
{
    using System;
    using System.Collections.Generic;

    public class DiscreteAlarmLimit : IAlarmLimit
	{
		/// <summary>
		/// Maps the [ParameterID][ParameterIdx] to the user friendly name of the discrete alarm limit.
		/// </summary>
		protected virtual IReadOnlyDictionary<byte, IReadOnlyDictionary<byte, IReadOnlyDictionary<byte, string>>> ParameterNameMapping { get; }

		public byte ParamId { get; protected set; }

		public byte ParamIdx { get; protected set; }

		public byte PairId { get; protected set; }

		public byte TypeCount { get; protected set; }

		public DiscreteAlarmState State { get; protected set; }

		public byte StateValue { get; protected set; }

		public byte Value { get; protected set; }

		public Severity Severity { get; protected set; }

		public string Name
		{
			get
			{
				try
				{
					return ParameterNameMapping[ParamId][ParamIdx][PairId];
				}
				catch (Exception)
				{
					return Key;
				}
			}
		}

		public string Key
		{
			get
			{
				return $"{ParamId}.{ParamIdx}.{PairId}";
			}
		}

		public DiscreteAlarmLimit()
		{

		}

		public static IEnumerable<DiscreteAlarmLimit> FromData(byte[] data)
		{
			return FromData(data, 0);
		}

		public static IEnumerable<DiscreteAlarmLimit> FromData(byte[] data, int startIndex)
		{
			int position = startIndex;

			byte paramId = data[position];
			position++;

			byte paramIdx = data[position];
			position++;

			byte typeCount = data[position];
			position++;

			if (typeCount < 1) throw new ArgumentException($"Provided data does not represent a Discrete Alarm Limit", nameof(data));

			DiscreteAlarmState state = (DiscreteAlarmState)data[position];
			position++;

			byte stateValue = data[position];
			position++;

			byte value = data[position];
			position++;

			Dictionary<byte, Severity> pairs = new Dictionary<byte, Severity>();
			for (int i = 0; i < typeCount; i++)
			{
				byte pairId = data[position];
				position++;

				Severity severity = (Severity)data[position];
				position++;

				pairs.Add(pairId, severity);
			}

			List<DiscreteAlarmLimit> limits = new List<DiscreteAlarmLimit>();
			foreach (var pair in pairs)
			{
				limits.Add(new DiscreteAlarmLimit
				{
					ParamId = paramId,
					ParamIdx = paramIdx,
					PairId = pair.Key,
					TypeCount = typeCount,
					State = state,
					StateValue = stateValue,
					Value = value,
					Severity = pair.Value
				});
			}

			return limits;
		}

		public byte[] ToData()
		{
			byte[] data = new byte[8];
			data[0] = ParamId;
			data[1] = ParamIdx;
			data[2] = TypeCount;
			data[3] = 0;
			data[4] = 0;
			data[5] = 0;
			data[6] = PairId;
			data[7] = (byte)Severity;

			return data;
		}
	}
}
