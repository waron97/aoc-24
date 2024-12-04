using System;
using System.Text.RegularExpressions;

class PartTwo
{
    public static void Solve()
    {
        var content = PartOne.GetInput();
        // do() | don't() | mul(\d{1,3},\d{1,3})
        var pattern = @"(do\(\))|(don't\(\))|(mul\(\d{1,3},\d{1,3}\))";
        var matches = Regex.Matches(content, pattern);
        var enabled = true;
        var total = 0;
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                enabled = true;
            }
            else if (match.Value == "don't()")
            {
                enabled = false;
            }
            else
            {
                if (enabled)
                {
                    total += PartOne.ProcessMatch(match);
                }
            }
        }
        Console.WriteLine(string.Format("Part Two solution: {0}", total));
    }
}