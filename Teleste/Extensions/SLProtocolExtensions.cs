namespace Skyline.DataMiner.TelesteHelper.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using Skyline.DataMiner.Scripting;

    public static class SLProtocolExtensions
	{
		public static byte[] GetParameterBinary(this SLProtocol protocol, int paramId)
		{
			object data = protocol.GetData("PARAMETER", paramId);
			if (data == null) return new byte[0];

			object[] dataBytes = (object[])data;
			byte[] bytes = new byte[dataBytes.Length];
			Array.Copy(dataBytes, bytes, bytes.Length);

			return bytes;
		}

		public static void LogError(this SLProtocol protocol, string className, string methodName, string message)
		{
			Log(protocol, className, methodName, message, LogType.Error);
		}

		public static void LogInformation(this SLProtocol protocol, string className, string methodName, string message)
		{
			Log(protocol, className, methodName, message, LogType.Information);
		}

		/// <summary>
		/// This method checks if all provided parameters are empty.
		/// This was introduced as the regular SLProtocol.IsEmpty method does not work for parameters with fixed length.
		/// A fixed length parameter will always contain a byte[] array of the specified fixed length.
		/// Number parameter -> byte array with '0' values.
		/// String parameter -> byte array with '32' (= space) values.
		/// </summary>
		/// <param name="protocol">Link with DataMiner.</param>
		/// <param name="paramIds">IDs of the parameter to check.</param>
		/// <returns>True if all parameters are empty or have the default values.</returns>
		public static bool IsEmpty(this SLProtocol protocol, params int[] paramIds)
		{
			foreach(int paramId in paramIds)
			{
				byte[] data = protocol.GetParameterBinary(paramId);
				protocol.LogInformation(nameof(SLProtocolExtensions), nameof(IsEmpty), $"Value of param {paramId}: {String.Join(", ", data)}");
				if (!protocol.IsEmpty(data)) return false;
			}

			return true;
		}

		public static bool IsEmpty(this SLProtocol protocol, byte[] data)
		{
			if (data == null) return true;
			if (data.Length == 0) return true;
			if (data.All(x => x == 0)) return true;
			if (data.All(x => x == 32)) return true;
			return false;
		}

		public static void SetParameters<T>(this SLProtocol protocol, T value)
		{
			List<int> pids = new List<int>();
			List<object> values = new List<object>();

			foreach (var property in typeof(T).GetProperties())
			{
				if (!(property.GetCustomAttributes(false).FirstOrDefault(x => x is PidAttribute) is PidAttribute pidAttribute)) continue;

				pids.Add(pidAttribute.Pid);
				values.Add(property.GetValue(value));
			}

			if (!pids.Any()) return;

			protocol.SetParameters(pids.ToArray(), values.ToArray());
		}

		private static void Log(this SLProtocol protocol, string className, string methodName, string message, LogType logType)
		{
			protocol.Log($"QA{protocol.QActionID}|{className}|{methodName}|{message}", logType, LogLevel.NoLogging);
		}
	}
}
