using System;
using System.Collections.Generic;
using System.IO;

class PartTwo
{
    public static (string?, string?) GetOptionsAtPos(List<char[]> grid, int x, int y)
    {
        var d1 = PartOne.GetGridString(grid, new List<(int, int)> {
            (x-1, y-1),
            (x, y),
            (x+1, y+1)
        });
        var d2 = PartOne.GetGridString(grid, new List<(int, int)> {
            (x-1, y+1),
            (x,y),
            (x+1, y-1)
        });

        return (d1, d2);
    }

    public static bool IsEqual(string? s, string target)
    {
        if (s == null)
        {
            return false;
        }
        var chArr = s.ToCharArray();
        Array.Reverse(chArr);
        var rev = new string(chArr);
        return target == s || target == rev;
    }
    public static void Solve()
    {
        // solutions will be hashed as "(x,y)(x,y)(x,y)(x,y)"
        var found = 0;
        var grid = new List<char[]>();
        var reader = new StreamReader("/Users/aron/codebase/personal/aoc-24/day-04/input.txt");
        var line = reader.ReadLine();

        while (line != null)
        {
            grid.Add(line.ToCharArray());
            line = reader.ReadLine();
        }

        for (var x = 0; x < grid.Count; x++)
        {
            var row = grid[x];
            var y = 0;
            while (y < row.Length)
            {
                var (d1, d2) = GetOptionsAtPos(grid, x, y);
                if (IsEqual(d1, "MAS") && IsEqual(d2, "MAS"))
                {
                    found++;
                }
                y++;
            }

        }
        Console.WriteLine(string.Format("Part Two solution: {0}", found));
    }
}