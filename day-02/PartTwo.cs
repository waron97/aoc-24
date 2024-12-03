using System;
using System.Collections.Generic;
using System.Linq;

class PartTwo
{

    public static bool GetIsLineValid(List<int> line)
    {
        foreach (var i in Enumerable.Range(0, line.Count))
        {
            var copy = new List<int>(line);
            copy.RemoveAt(i);
            if (PartOne.GetIsLineValid(copy))
            {
                return true;
            }
        }
        return false;
    }

    public static void Solve()
    {
        var numSafe = 0;
        foreach (var line in PartOne.GetLines())
        {
            if (GetIsLineValid(line))
            {
                numSafe++;
            }
        }
        Console.WriteLine(string.Format("Part Two solution: {0}", numSafe));
    }
}