using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class PartOne
{
    public static string GetInput()
    {
        var reader = new StreamReader("input.txt");
        var content = reader.ReadToEnd();
        return content;
    }
    public static int ProcessMatch(Match m)
    {
        var digitMatches = Regex.Matches(m.Value, @"\d{1,3}");
        var v1 = int.Parse(digitMatches[0].Value);
        var v2 = int.Parse(digitMatches[1].Value);
        return v1 * v2;
    }
    public static void Solve()
    {
        var content = GetInput();
        var pattern = @"mul\(\d{1,3},\d{1,3}\)";
        var matches = Regex.Matches(content, pattern);
        var result = matches.Aggregate(
            0,
            (total, next) =>
            {
                return total + ProcessMatch(next);
            }
        );
        Console.WriteLine(string.Format("Part One result: {0}", result));
    }
}