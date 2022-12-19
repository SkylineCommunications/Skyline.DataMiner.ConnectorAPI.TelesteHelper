namespace Skyline.DataMiner.TelesteHelper.Parsing
{
	using System;
	using System.Linq;

	public enum Endianness
	{
		BigEndian,
		LittleEndian
	}

	public static class ConversionHelper
	{
		public static short ToInt16(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[2];
			Array.Copy(value, startIndex, array, 0, 2);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToInt16(array, 0);
		}

		public static int ToInt32(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[4];
			Array.Copy(value, startIndex, array, 0, 4);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToInt32(array, 0);
		}

		public static long ToInt64(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[8];
			Array.Copy(value, startIndex, array, 0, 8);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToInt64(array, 0);
		}

		public static ushort ToUInt16(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[2];
			Array.Copy(value, startIndex, array, 0, 2);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToUInt16(array, 0);
		}

		public static uint ToUInt32(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[4];
			Array.Copy(value, startIndex, array, 0, 4);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToUInt32(array, 0);
		}

		public static ulong ToUInt64(byte[] value, int startIndex, Endianness endianness)
		{
			byte[] array = new byte[8];
			Array.Copy(value, startIndex, array, 0, 8);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return BitConverter.ToUInt64(array, 0);
		}

		public static string ToString(byte[] value, int startIndex, int length)
		{
			byte[] characters = new byte[length];
			Array.Copy(value, startIndex, characters, 0, length);
			return new String(characters.Select(x => (char)x).ToArray());
		}

		public static byte[] GetBytes(short value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(int value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(long value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(ushort value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(uint value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(ulong value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(double value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte[] GetBytes(float value, Endianness endianness)
		{
			byte[] array = BitConverter.GetBytes(value);

			Endianness systemEndianness = (Endianness)Convert.ToInt32(BitConverter.IsLittleEndian);
			if (endianness != systemEndianness) array = array.Reverse().ToArray();

			return array;
		}

		public static byte GetBitValue(byte[] data, int index)
		{
			int masterIndex = index / 8;
			int slaveIndex = 7 - (index % 8);

			byte dataByte = data[masterIndex];
			return (byte)((dataByte & (1 << slaveIndex)) >> slaveIndex);
		}

		public static byte GetBitValue(byte data, int index)
		{
			int shift = 7 - index;
			return (byte)((data & (1 << shift)) >> shift);
		}

		public static bool IsBitSet(byte[] data, int index)
		{
			return GetBitValue(data, index) == 1;
		}

		public static bool IsBitSet(byte data, int index)
		{
			return GetBitValue(data, index) == 1;
		}
	}
}
