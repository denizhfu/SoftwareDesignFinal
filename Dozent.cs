
using System;
using System.Collections.Generic;
namespace SD_TimeTable{
public class DozentFinal
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Tuple<int, int>> Blocked { get; set; } = new List<Tuple<int, int>>();

    public bool Available(int day, int block)
    {
        var find = new Tuple<int, int>(day, block);
        if (Blocked.Contains(find))
        {
            return false;
        }
        return true;
    }

    public void Book(int day, int block)
    {
        Blocked.Add(new Tuple<int, int>(day, block));
    }
}}