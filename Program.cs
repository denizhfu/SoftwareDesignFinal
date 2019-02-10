using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


namespace SD_TimeTable{
    class Program
    {
        public static List<RaumFinal> raeume = new List<RaumFinal>();
        public static string userAuswahlStudiengang;
        public static int userAuswahlSemester;
        static void Main(string[] args)
        {
            Console.WriteLine("Suchen Sie einen bestimmten Kurs? *Ja/Nein*");
           
            string userEntscheidungJaNein = Console.ReadLine();
            if (userEntscheidungJaNein == "ja" || userEntscheidungJaNein == "JA" || userEntscheidungJaNein == "Ja")
            {
                Console.WriteLine("Stundenplan für welchen Studiengang? *zb. OMB*");
                userAuswahlStudiengang = Console.ReadLine();
                Console.WriteLine("Zu welchem Semester? *zb 2*");
                userAuswahlSemester = Convert.ToInt32(Console.ReadLine());
            }
        
            

            var json = File.ReadAllText("Infos.json");
            if (json.Length == 0)
            {
                throw new Exception("Leere JSON!");
            }

            Driver Content = Driver.FromJson(json);

            List<StudiengangFinal> ListeAllerStudiengaenge = new List<StudiengangFinal>();
            foreach (StudiengangFinal einzelnerStudiengang in Content.ListeAllerStudiengaenge)
            {
                StudiengangFinal finalStudiengang = new StudiengangFinal();
                finalStudiengang.Id = einzelnerStudiengang.Id;
                finalStudiengang.Name = einzelnerStudiengang.Name;
                finalStudiengang.arrayAlleSemester = einzelnerStudiengang.arrayAlleSemester;
                ListeAllerStudiengaenge.Add(finalStudiengang);
            }

            List<DozentFinal> dozenten = new List<DozentFinal>();
            foreach (var dozent in Content.Dozenten)
            {
                DozentFinal finalDoz = new DozentFinal();
                finalDoz.Id = dozent.Id;
                finalDoz.Name = dozent.Name;
                dozenten.Add(finalDoz);
            }

            foreach (var raum in Content.Raeume)
            {
                RaumFinal finalRaum = new RaumFinal();
                finalRaum.Id = raum.Id;
                finalRaum.RaumNr = raum.RaumNr;
                finalRaum.Austattung = raum.Ausstattung;
                finalRaum.RaumStudiengang = raum.RaumStudiengang;
                finalRaum.Belegt = false;
                raeume.Add(finalRaum);
            }

            List<KursFinal> kurse = new List<KursFinal>();
            foreach (var kurs in Content.Kurse)
            {
                KursFinal finalKrs = new KursFinal();
                finalKrs.Id = kurs.Id;
                finalKrs.Name = kurs.Name;
                finalKrs.Dozent = dozenten.Single(dozent => dozent.Id == kurs.Dozent);
                finalKrs.KursSemester = kurs.KursSemester;
                finalKrs.KursStudiengang = kurs.KursStudiengang;
                kurse.Add(finalKrs);
            }

            List<SemesterFinal> semester = new List<SemesterFinal>();
            foreach (var semest in Content.Semester)
            {
                SemesterFinal finalSemester = new SemesterFinal();
                finalSemester.Id = semest.Id;
                finalSemester.SemesterNumber = semest.SemesterNumber;
                finalSemester.AlleKurseDerWeek = new KursFinal[5, 4];
                semester.Add(finalSemester);
            }

            foreach (StudiengangFinal einzelnerStudiengang in ListeAllerStudiengaenge) 
            {
                for (int semesterZähler = 1; semesterZähler <= 5; semesterZähler++)
                {
                    Console.WriteLine();
                    KursFinal[,] KursplanNachStudiengangUndSemester = new KursFinal[5, 4];

                    var jumper = false;
                   
                    var kurseNachSemesterUndStudiengang = kurse.Where(k => k.KursSemester == semesterZähler && k.KursStudiengang == einzelnerStudiengang.Name);
                    foreach (KursFinal kurs in kurseNachSemesterUndStudiengang)
                    {
                        for (int tag = 0; tag < 5; tag++)
                        {
                            for(int block = 0; block < 4; block++)
                            {
                                if (kurs.Dozent.Available(tag, block) && !CourseAlreadyBooked(KursplanNachStudiengangUndSemester, tag, block))
                                {
                                    kurs.Dozent.Book(tag, block);
                                    KursplanNachStudiengangUndSemester[tag, block] = kurs;

                                    jumper = true;
                                    break;
                                }

                                if (tag == 4 && block == 3)
                                {
                                    throw new Exception("ENDE ERREICHT");
                                }
                            }
                            if (jumper)
                                break;
                        }

                        if (jumper)
                            jumper = false;
                    }

                    if (userEntscheidungJaNein == "ja" || userEntscheidungJaNein == "JA" || userEntscheidungJaNein =="Ja")
                    {
                        if  (userAuswahlStudiengang == einzelnerStudiengang.Name && userAuswahlSemester == semesterZähler)
                        { 
                            Ausgabe.TitelAusgabe(einzelnerStudiengang.Name, semesterZähler);
                           Ausgabe.PlanAusgabe(KursplanNachStudiengangUndSemester);
                        }
                    }
                    else
                    {
                        Ausgabe.TitelAusgabe(einzelnerStudiengang.Name, semesterZähler);
                        Ausgabe.PlanAusgabe(KursplanNachStudiengangUndSemester);
                    }
                        

                  
                }

            }
            
            bool CourseAlreadyBooked(KursFinal[,] stundenplan, int day, int block)
            {
                var kurs = stundenplan[day, block];
                if (kurs == null)
                    return false;
                return true;
            }
       }
    }
}
