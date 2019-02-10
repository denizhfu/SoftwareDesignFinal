using System.Collections.Generic;

namespace SD_TimeTable{
public class SemesterFinal
{
    public int Id { get; set; }
    public int SemesterNumber { get; set; }
    //public List<KursFinal[]> AlleKurseDerWeek { get; set; }
    public KursFinal[,] AlleKurseDerWeek { get; set; }
}

}