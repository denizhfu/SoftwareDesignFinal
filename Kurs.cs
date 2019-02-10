using System.Collections.Generic;
namespace SD_TimeTable{
public partial class KursFinal
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Anforderungen { get; set; }

    public DozentFinal Dozent { get; set; }
    public RaumFinal Raum { get; set; }

    public string KursStudiengang { get; set; }
    public int KursSemester { get; set; }
}
}