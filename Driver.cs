using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SD_TimeTable{

    public partial class Driver
    {

        [JsonProperty("ListeAllerStudiengaenge")]
        public List<StudiengangFinal> ListeAllerStudiengaenge { get; set; }

        [JsonProperty("semester")]
        public List<SemesterFinal> Semester { get; set; }

        [JsonProperty("dozenten")]
        public List<Dozent> Dozenten { get; set; }

        [JsonProperty("raeume")]
        public List<Raum> Raeume { get; set; }

        [JsonProperty("kurse")]
        public List<Kurs> Kurse { get; set; }

        [JsonProperty("fakultaet")]
        public Fakultaet Fakultaet { get; set; }
    }

    public partial class Dozent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Fakultaet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("dozenten")]
        public List<long> Dozenten { get; set; }

        [JsonProperty("ListeAllerStudiengaenge")]
        public List<long> ListeAllerStudiengaenge { get; set; }
    }

    public partial class Kurs
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("dozent")]
        public long Dozent { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("anforderungen")]
        public int Anforderungen { get; set; }

        [JsonProperty("studieng")]
        public string KursStudiengang { get; set; }

        [JsonProperty("semest")]
        public int KursSemester { get; set; }
    }

    public partial class Raum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("raumNr")]
        public string RaumNr { get; set; }

        [JsonProperty("ausstattung")]
        public int Ausstattung { get; set; }

        [JsonProperty("sgang")]
        public string RaumStudiengang { get; set; }
    }

    public partial class Semester
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("semesterNumber")]
        public long SemesterNumber { get; set; }

        [JsonProperty("alleKurseDerW")]
        public KursFinal[,] alleKurseDerWeek { get; set; }
    }

    public class Studiengang
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("semester", NullValueHandling = NullValueHandling.Ignore)]
        //public List<SemesterFinal> Semester { get; set; }
    }

    public partial class Driver
    {
        public static Driver FromJson(string json) => JsonConvert.DeserializeObject<Driver>(json, SD_TimeTable.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Driver self) => JsonConvert.SerializeObject(self, SD_TimeTable.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
