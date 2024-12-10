using System;
using System.Collections.Generic;
using System.Linq;

class PartTwo
{
    public static void Solve()
    {
        var (ruleSet, updates) = PartOne.ReadInput();

        if (updates == null || ruleSet == null)
        {
            throw new System.Exception("Failed to read updates");
        }

        var sortedMiddles = new List<int>();
        foreach (var update in updates)
        {
            if (!ruleSet.Validate(update))
            {
                var sorted = ruleSet.Sort(update).ToList();
                var idx = sorted.Count / 2;
                sortedMiddles.Add(sorted[idx]);
            }
        }
        Console.WriteLine(string.Format("Part Two solution: {0}", sortedMiddles.Sum()));
    }
}