﻿namespace Skyline.DataMiner.TelesteHelper.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mask
	{
		private readonly byte[] mask;
		private readonly int startParameterIndex;

		public Mask(byte[] mask, int startParameterIndex)
		{
			this.mask = mask;
			this.startParameterIndex = startParameterIndex;
		}

		private int Length
		{
			get
			{
				return mask.Length * 8;
			}
		}

		public static Mask FromResponse(byte[] data, int startIndex, int length, int startParameterIndex)
		{
			byte[] copiedMask = new byte[length];
			Array.Copy(data, startIndex, copiedMask, 0, length);

			return new Mask(copiedMask, startParameterIndex);
		}

		public List<int> Parameters
		{
			get
			{
				List<int> parameterIndices = new List<int>();
				for (int i = 0; i < Length; i++)
				{
					if (ConversionHelper.IsBitSet(mask, i)) parameterIndices.Add(i + startParameterIndex);
				}

				return parameterIndices;
			}
		}
	}
}
