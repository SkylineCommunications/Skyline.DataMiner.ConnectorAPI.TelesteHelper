namespace Skyline.DataMiner.TelesteHelper.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Csv
	{
		public Csv(string[] headers, IEnumerable<string[]> rows, IEnumerable<string[]> columns)
		{
			Headers = headers;
			Rows = rows;
			Columns = columns;
		}

		public Csv(string[] headers, IEnumerable<string[]> columns)
		{
			Headers = headers;
			Columns = columns;
			Rows = GetRows(columns);
		}

		private Csv()
		{
		}

		public string[] Headers { get; private set; } = new string[0];

		public IEnumerable<string[]> Columns { get; private set; } = new string[0][];

		public IEnumerable<string[]> Rows { get; private set; } = new string[0][];

		public int ColumnCount => Columns.Count();

		public int RowCount => Rows.Count();

		public static Csv Empty()
		{
			return new Csv();
		}

		public string[] GetColumn(string header)
		{
			int columnIndex = Array.IndexOf(Headers, header);
			if (columnIndex == -1) return new string[0];
			return GetColumn(columnIndex);
		}

		public string[] GetColumn(int index)
		{
			int columnIndex = 0;
			foreach (string[] column in Columns)
			{
				if (columnIndex == index) return column;
				columnIndex++;
			}

			return new string[0];
		}

		public string[] GetRow(int index)
		{
			int rowIndex = 0;
			foreach (string[] row in Rows)
			{
				if (rowIndex == index) return row;
				rowIndex++;
			}

			return new string[0];
		}

		private static string[][] GetRows(IEnumerable<string[]> columns)
		{
			string[][] rows = new string[0][];
			if (!columns.Any())
			{
				return rows;
			}

			int rowCount = columns.Max(x => x.Length);
			int columnCount = columns.Count();

			rows = new string[rowCount][];
			for (int i = 0; i < rowCount; i++)
			{
				rows[i] = new string[columnCount];
				for (int j = 0; j < columnCount; j++)
				{
					string[] currentColumn = columns.ElementAt(j);
					rows[i][j] = i >= currentColumn.Length ? String.Empty : currentColumn[i];
				}
			}

			return rows;
		}
	}
}