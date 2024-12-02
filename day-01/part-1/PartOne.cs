
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class PartOne
{
    public static (List<int>, List<int>) GetLists()
    {
        using var reader = new FileStream("input.txt", FileMode.Open);
        var left = new List<int>();
        var right = new List<int>();
        var readCount = 0;
        var buffer = new byte[14];
        while ((readCount = reader.Read(buffer, 0, buffer.Length)) != 0)
        {
            var line = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Substring(0, 13);
            var nums = line.Split();
            var leftNum = Int32.Parse(nums[0]);
            var rightNum = Int32.Parse(nums[^1]);
            left.Add(leftNum);
            right.Add(rightNum); ;
        }
        return (left, right);
    }
    public static void Solve()
    {
        var (left, right) = GetLists();
        left.Sort();
        right.Sort();
        var distances = 0;
        for (var i = 0; i < left.Count; i++)
        {
            var (l, r) = (left[i], right[i]);
            var dist = Math.Abs(l - r);
            distances += dist;
        }
        Console.WriteLine(string.Format("Part One solution: {0}", distances));
    }
}