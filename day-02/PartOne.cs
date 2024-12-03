using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class PartOne
{
    public static List<int> ProcessLine(string line)
    {
        var segments = line.Trim().Split();
        var list = new List<int>();
        foreach (var segment in segments)
        {
            try
            {
                list.Add(int.Parse(segment));
            }
            catch (FormatException)
            {
                // Console.WriteLine(string.Format("Could not parse segment {0}", segment));
            }
        }
        return list;
    }
    public static IEnumerable<List<int>> GetLines()
    {
        var reader = new FileStream("input.txt", FileMode.Open);
        var chunkSize = 100;
        var buffer = new byte[chunkSize];
        var line = "";
        int readCount;
        while ((readCount = reader.Read(buffer, 0, buffer.Length)) != 0)
        {
            var bytes = buffer[0..readCount];
            var newLine = 0x0a;
            foreach (var b in bytes)
            {
                if (newLine == b)
                {
                    yield return ProcessLine(line);
                    line = "";
                }
                else
                {
                    line += Encoding.UTF8.GetString([b]);
                }
            }
        }
        if (line != "")
        {
            // account for file not ending in newline
            yield return ProcessLine(line);
        }
    }

    public static bool GetIsLineValid(List<int> line)
    {
        var isValid = true;
        var diffs = new List<int>();
        for (var i = 0; i < line.Count - 1; i++)
        {
            diffs.Add(line[i + 1] - line[i]);
        }
        int direction = 0;
        foreach (var diff in diffs)
        {
            if (diff == 0 || Math.Abs(diff) > 3)
            {
                isValid = false;
                break;
            }
            if (direction == 0)
            {
                // set direction after first pair
                direction = diff > 0 ? 1 : -1;
            }
            else
            {
                if (direction == 1 && diff < 0)
                {
                    isValid = false;
                    break;
                }
                if (direction == -1 && diff > 0)
                {
                    isValid = false;
                    break;
                }
            }

        }
        return isValid;
    }
    public static void Solve()
    {
        var numSafe = 0;
        foreach (var line in GetLines())
        {
            if (GetIsLineValid(line))
            {
                numSafe++;
            }
        }
        Console.WriteLine(string.Format("Part One solution: {0}", numSafe));
    }
}