using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class PartOne
{
    public static (RuleSet, List<int[]>) ReadInput()
    {
        var rules = new List<string>();
        var updates = new List<int[]>();

        var reader = new StreamReader("/Users/aron/codebase/personal/aoc-24/day-05/input.txt");
        var line = reader.ReadLine();

        var boundCrossed = false;
        while (line != null)
        {
            if (!boundCrossed)
            {

                if (line != "")
                {
                    rules.Add(line);
                }
                else
                {
                    boundCrossed = true;
                }
            }
            else
            {
                var update = line.Split(",").Select(int.Parse).ToArray();
                if (update != null)
                {
                    updates.Add(update);
                }
            }
            line = reader.ReadLine();
        }

        return (new RuleSet(rules), updates);
    }
    public static void Solve()
    {
        var (ruleSet, updates) = ReadInput();
        if (updates == null || ruleSet == null)
        {
            throw new System.Exception("Failed to read updates");
        }

        var validMiddles = new List<int>();
        foreach (var update in updates)
        {
            if (ruleSet.Validate(update))
            {
                var idx = update.Length / 2;
                validMiddles.Add(update[idx]);
            }
        }
        Console.WriteLine(string.Format("Part One solution: {0}", validMiddles.Sum()));
    }
}

class RuleSet
{
    private readonly Dictionary<int, HashSet<int>> rules;
    public RuleSet(List<string> rawRules)
    {
        rules = new Dictionary<int, HashSet<int>>();
        foreach (var ruleDef in rawRules)
        {
            var parts = ruleDef.Split("|").Select(int.Parse).ToArray();
            var (num1, num2) = (parts[0], parts[1]);

            if (rules.TryGetValue(num2, out var mustComeAfter))
            {
                mustComeAfter.Add(num1);
            }
            else
            {
                rules.Add(num2, new HashSet<int> { num1 });
            }
        }
    }

    private bool SetContainsFully(HashSet<int> set, HashSet<int> target)
    {
        if (target.Count == 0)
        {
            return true;
        }
        return set.Intersect(target).Count() == target.Count;
    }

    public bool Validate(IEnumerable<int> line)
    {
        var seen = new HashSet<int>();
        foreach (var num in line)
        {
            if (rules.TryGetValue(num, out var mustComeAfter))
            {
                var trueRule = mustComeAfter.Intersect(line).ToHashSet();
                // if line contains number with rule, apply rule
                if (!SetContainsFully(seen, trueRule))
                {
                    return false;
                }
            }
            seen.Add(num);
        }
        return true;
    }

    private int AllSeenAt(List<int> line, HashSet<int> mustComeAfter)
    {
        var seen = new HashSet<int>();
        for (var i = 0; i < line.Count; i++)
        {
            seen.Add(line[i]);
            if (seen.Intersect(mustComeAfter).Count() == mustComeAfter.Count)
            {
                return i;
            }
        }
        // should never trigger
        throw new Exception("Could not place number");
    }

    public IEnumerable<int> Sort(IEnumerable<int> line)
    {
        var updatedLine = new List<int>(line);
        var changeMade = true;
        while (changeMade)
        {
            changeMade = false;
            for (var i = 0; i < line.Count(); i++)
            {
                var num = updatedLine[i];
                if (rules.TryGetValue(num, out var mustComeAfter))
                {

                    var trueRule = mustComeAfter.Intersect(line).ToHashSet();
                    var allSeenAt = AllSeenAt(updatedLine, trueRule);
                    if (allSeenAt > i)
                    {
                        // must be moved to a later index
                        updatedLine.Insert(allSeenAt + 1, num);
                        updatedLine.RemoveAt(i);
                        changeMade = true;
                        break;
                    }
                }
            }
        }
        return updatedLine;
    }
}