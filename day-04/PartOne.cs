using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class PartOne
{
    public static string? GetGridString(List<char[]> grid, List<(int, int)> indexes)
    {

        var str = "";
        foreach (var (x, y) in indexes)
        {
            if (x >= 0 && x < grid.Count && y >= 0 && y < grid[0].Length)
            {
                var ch = grid[x][y];
                str += ch;
            }
            else
            {
                return null;
            }

        }
        return str;

    }
    public static List<string> GetOptionsAtPos(List<char[]> grid, int x, int y)
    {
        var options = new List<string>();
        var vertical = new List<(int, int)> {
            (x, y),
            (x-1, y),
            (x-2, y),
            (x-3, y)
        };
        var horizontal = new List<(int, int)> {
            (x, y),
            (x, y-1),
            (x, y-2),
            (x, y-3)
        };
        var diagonal = new List<(int, int)> {
            (x, y),
            (x-1, y-1),
            (x-2, y-2),
            (x-3, y-3)
        };
        var diagonal2 = new List<(int, int)> {
            (x, y),
            (x-1, y+1),
            (x-2, y+2),
            (x-3, y+3)
        };
        var h = GetGridString(grid, horizontal);
        var v = GetGridString(grid, vertical);
        var d = GetGridString(grid, diagonal);
        var d2 = GetGridString(grid, diagonal2);

        if (h != null)
        {
            options.Add(h);
        }
        if (v != null)
        {
            options.Add(v);
        }
        if (d != null)
        {
            options.Add(d);
        }
        if (d2 != null)
        {
            options.Add(d2);
        }

        return options;
    }
    public static void Solve()
    {
        // solutions will be hashed as "(x,y)(x,y)(x,y)(x,y)"
        var found = 0;
        var grid = new List<char[]>();
        var reader = new StreamReader("/Users/aron/codebase/personal/aoc-24/day-04/input.txt");
        var line = reader.ReadLine();
        var x = 0;
        while (line != null)
        {
            grid.Add(line.ToCharArray());
            var y = 0;
            while (y < line.Length)
            {
                var options = GetOptionsAtPos(grid, x, y);
                var valid = options.Where(opt =>
                {
                    var chArr = opt.ToCharArray();
                    Array.Reverse(chArr);
                    var rev = new string(chArr);
                    return "XMAS" == opt || "XMAS" == rev;
                }).ToList();
                found += valid.Count;
                y++;
            }

            line = reader.ReadLine();
            x++;
        }
        Console.WriteLine(string.Format("Part One solution: {0}", found));
    }
}