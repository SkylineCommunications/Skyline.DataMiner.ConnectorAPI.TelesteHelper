namespace Skyline.DataMiner.TelesteHelper.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class CsvManager
	{
		public static Csv Import(string filePath, char delimiter = ';')
		{
			IEnumerable<string> lines = ReadLines(filePath);
			int lineCount = lines.Count();
			if (lineCount == 0) return Csv.Empty();

			bool init = false;
			int columnCount = 0;
			int rowIndex = 0;
			int rowCount = lineCount - 1;
			int lineNumber = 1;
			string[] headers = new string[0];
			string[][] rows = new string[rowCount][];
			string[][] columns = null;

			string[] splitLine;
			foreach (string line in lines)
			{
				splitLine = line.Split(delimiter);
				if (!init)
				{
					headers = splitLine;
					columnCount = headers.Length;
					init = true;
					columns = new string[columnCount][];
					for (int i = 0; i < columnCount; i++) columns[i] = new string[rowCount];
				}
				else if (splitLine.Length == columnCount)
				{
					rows[rowIndex] = splitLine;
					for (int i = 0; i < splitLine.Length; i++) columns[i][rowIndex] = splitLine[i];
					rowIndex++;
				}
				else
				{
					throw new FormatException(String.Format("{0} does not specify {1} columns at line {2}", filePath, columnCount, lineNumber));
				}

				lineNumber++;
			}

			return new Csv(headers, rows, columns);
		}

		public static void Export(string filePath, Csv csv, char delimiter = ';')
		{
			FileStream stream = File.Open(filePath, FileMode.Create, FileAccess.Write);
			using (StreamWriter writer = new StreamWriter(stream))
			{
				string line;
				line = String.Join(delimiter.ToString(), csv.Headers);
				writer.WriteLine(line);

				foreach (string[] row in csv.Rows)
				{
					line = String.Join(delimiter.ToString(), row);
					writer.WriteLine(line);
				}
			}
		}

		private static IEnumerable<string> ReadLines(string filePath)
		{
			if (!File.Exists(filePath)) return new string[0];

			List<string> lines = new List<string>();
			FileStream stream = File.OpenRead(filePath);
			using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
			{
				while (!reader.EndOfStream)
				{
					lines.Add(reader.ReadLine());
				}
			}

			return lines;
		}
	}
}