using System;
using System.Collections.Generic;

class PartTwo
{
    public static void Solve()
    {
        var (left, right) = PartOne.GetLists();
        var hashLeft = new HashSet<int>(left);
        var countsTable = new Dictionary<int, int>();
        foreach (var num in right)
        {
            if (countsTable.ContainsKey(num))
            {
                countsTable[num] += 1;
            }
            else
            {
                countsTable[num] = 1;
            }
        }

        var diff = 0;

        foreach (var num in hashLeft)
        {
            if (countsTable.TryGetValue(num, out int count))
            {
                diff += num * count;
            }
        }

        Console.WriteLine(string.Format("Part Two solution: {0}", diff));
    }
}