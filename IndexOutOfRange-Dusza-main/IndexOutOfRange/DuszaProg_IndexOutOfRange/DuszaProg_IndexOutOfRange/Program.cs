using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace DuszaProg_IndexOutOfRange
{
    internal class Program
    {

        public const int EGYFOLYAMAT = 4;
        public static string gyoker = "";
        public static string[] files = [];
        public static List<string> szamitogepMappakElerese = new();
        public static List<SzamitogepConfig> szamitogepConfigok = new();
        public static Dictionary<ProgramFolyamat, string> szamitogepekenFutoAlkalmazasok = new();
        public static List<Kluszter> klaszterLista = [];
        public static List<string> gépNevek = [];
        public static List<string> betuk = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "x", "y", "z"];
        static void Main(string[] args)
        {
            try
            {
                Inditas();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.Clear();
                Inditas();
                
            }
        }

        public static void Inditas()
        {
            Console.WriteLine("Adja meg a gyökérkönyvtár pontos elérési útját!");
            gyoker = Console.ReadLine();
            if (Directory.GetFiles(gyoker) != null && Directory.GetFiles(gyoker).Length > 0 && Directory.GetFiles(gyoker).Any(x => x.Contains(".klaszter")))
            {
                SzamitogepMappakElerese();
                SzamitogepConfigok();
                ProgramokBeolvasása();
                KluszterCucc();
                MenuKiiratasa();
            }
            else
            {
                Console.WriteLine("Helytelen elérési út!");
            }
        }

        public static void MenuKiiratasa()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
            Console.WriteLine("[1] Monitoring");
            Console.WriteLine("[2] Számítógép törlése");
            Console.WriteLine("[3] Számítógép hozzáadása");
            Console.WriteLine("[4] Egy program leállítása");
            Console.WriteLine("[5] Egy program adatainak módosítása");
            Console.WriteLine("[6] Egy új programpéldány futtatása");
            Console.WriteLine("[7] Egy adott programpéldány leállítása");

            bool szamBeirva = false;
            KluszterCucc();
            while (szamBeirva == false)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Monitoring();
                        szamBeirva = true;
                        break;
                    case "2":
                        SzamitogepTorlese();
                        szamBeirva = true;
                        break;
                    case "3":
                        SzamitoGepHozzaAdas();
                        szamBeirva = true;
                        break;
                    case "4":
                        EgyProgramLeallitasa();
                        szamBeirva = true;
                        break;
                    case "5":
                        ProgramAdatainakModositasa();
                        szamBeirva = true;
                        break;
                    case "6":
                        ProgramPeldanyFuttatasa();
                        szamBeirva = true;
                        break;
                    case "7":
                        EgyAdottProgrampeldanyLeallitasa();
                        szamBeirva = true;
                        break;
                    default:
                        Console.WriteLine("Helytelen opciót adott meg!");
                        break;
                }
            }
        }
        public static void KluszterCucc()
        {
            string kluszterPath = Directory.GetFiles(gyoker).Where(x => x.Contains(".klaszter")).First();
            klaszterLista.Clear();
            for (int i = 0; i < File.ReadAllLines(kluszterPath).Select(x => x).ToList().Count; i++)
            {
                if (i == 0 || i % EGYFOLYAMAT == 0)
                {
                    int num = i + EGYFOLYAMAT;
                    klaszterLista.Add(new Kluszter(string.Join(";", File.ReadAllLines(kluszterPath).Select(x => x.ToString()).ToList()[i..num]).Split(";")));
                }
            }
            //kluszterLista.ForEach(x => Console.WriteLine($"{x.ProgramName};{x.MennyiActive};{x.Millimag};{x.Memoria}"));
        }
        public static void Monitoring()
        {
            int monitoringIndex = 1;
            int aktivFolyamatok = 0;
            int inaktívFolyamatok = 0;
            List<string> futoProgramok = new List<string>();
            foreach (var item in szamitogepConfigok.OrderBy(x => x.Eleres))
            {
                int osszMemoriahasznalat = 0;
                int osszProszesszorhasznalat = 0;
                Console.WriteLine($"{item.Eleres.Split(@"\").Last()}:\n\tMaximális millimag használat: {item.Millimag}\n\tMaximális memória használat: {item.Memoria}");
                foreach (var program in szamitogepekenFutoAlkalmazasok.Where(x => x.Value == item.Eleres).OrderBy(x => item.Eleres).ToList())
                {
                    if (program.Key.IsActive == "AKTÍV")
                    {
                        aktivFolyamatok++;
                    }
                    else 
                    {
                        inaktívFolyamatok++;
                    }
                    Console.WriteLine($"\tProgram neve: {program.Key.FajlNeve.Split('-')[0]}\n\t\tProgram státusza: {program.Key.IsActive}\n\t\tProgram processzorhasználata: {program.Key.ProcesszorEroforras}\n\t\tProgram memóriahasználata: {program.Key.MemoriaEroforras}\n\t\tProgram azonosítója: {program.Key.FajlNeve.Split('-')[1]}");
                    osszMemoriahasznalat += program.Key.MemoriaEroforras;
                    osszProszesszorhasznalat += program.Key.ProcesszorEroforras;
                    futoProgramok.Add(program.Key.FajlNeve.Split('-')[0]);
                    Console.WriteLine();
                }
                    Console.WriteLine($"\tSzabad millimag erőforrás: {item.Millimag-osszProszesszorhasznalat}\n\tSzabad memória erőforrás: {item.Memoria-osszMemoriahasznalat}");
                    Console.WriteLine();

                monitoringIndex++;
            }
            Console.WriteLine($"A klaszteren futó AKTÍV folyamatok száma: {aktivFolyamatok}\nA klaszteren futó INAKTÍV folyamatok száma: {inaktívFolyamatok}");
            List<string> distinctProgramok = futoProgramok.Distinct().ToList();
            Console.WriteLine();
            Console.WriteLine("Futó programok: ");
            foreach (var futoProgram in distinctProgramok)
            {
                Console.WriteLine($"\t{futoProgram}: {futoProgramok.Count(x => x == futoProgram)}");
            }

            Console.WriteLine();
            Console.WriteLine("Adja meg az egyik program nevét: ");
            string? futas = Console.ReadLine();
            if (distinctProgramok.Contains(futas))
            {
                Console.WriteLine($"{klaszterLista.Where(x => x.ProgramName == futas).First().MennyiActive} {futas} folyamat van jelenleg");
            }
            else
            {
                Console.WriteLine("Nincsen ilyen futó program!");
            }

            KluszterCucc();
            Console.ReadKey();
            MenuKiiratasa();
        }
        public static void SzamitogepMappakElerese()
        {
            szamitogepMappakElerese.Clear();
            foreach (string eleres in Directory.GetDirectories(gyoker).ToList())
            {
                szamitogepMappakElerese.Add(eleres);
                //string[] splittelt = eleres.Split('/');
                //if (splittelt.Last().Contains("szamitogep"))
                //{
                //};
            }
            KluszterCucc();

        }
        public static void SzamitogepConfigok()
        {
            szamitogepConfigok.Clear();
            foreach (var item in szamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    string[] configFajl = File.ReadAllLines(item + "/.szamitogep_config");
                    var gep = new SzamitogepConfig(Convert.ToInt32(configFajl[0]), Convert.ToInt32(configFajl[1]), item);
                    foreach (var programok in Directory.GetFiles(item))
                    {
                        if (!programok.Contains(".szamitogep_config"))
                        {
                            gep.ProgramPeldanyAzonositok.Add(programok.Split("\\").Last());
                        }
                    }
                    szamitogepConfigok.Add(gep);
                }
            }
            KluszterCucc();

        }
        public static void ProgramokBeolvasása()
        {
            szamitogepekenFutoAlkalmazasok.Clear();
            foreach (var item in szamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config"))
                    {
                        string[] fajlElemek = File.ReadAllLines(fajl);
                        szamitogepekenFutoAlkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()),item);
                    }
                }
            }
            KluszterCucc();

        }
        public static void ProgramPeldanyFuttatasa()
        {
            gépNevek.Clear();
            Console.WriteLine("Futtatható programok:");
            klaszterLista.ForEach(x => Console.WriteLine($"\tProgram Neve: {x.ProgramName}"));
            Console.WriteLine("Melyik programot szeretné futtatni? (Létező programot válasszon!)");
            string? programNeve = Console.ReadLine();
            while (klaszterLista.Any(x => x.ProgramName == programNeve) == false)
            {
                Console.WriteLine("Melyik programot szeretné futtatni? (Létező programot válasszon!)");
                programNeve = Console.ReadLine();
            }

            Console.WriteLine("Melyik gépen szeretné futtatni? (Létező gépet válasszon!)");
            szamitogepConfigok.ForEach(x => gépNevek.Add($"\t{x.Eleres.Split(@"\").Last()}"));
            Console.WriteLine();
            Console.WriteLine(String.Join("\n", gépNevek));
            Console.WriteLine();
            string? valasztottGep = Console.ReadLine();
            while (gépNevek.Any(x => x.Trim() == valasztottGep) == false)
            {
                Console.WriteLine("Melyik gépen szeretné futtatni? (Létező gépet válasszon!)");
                valasztottGep = Console.ReadLine();
            }
            if (szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Memoria > klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria &&
                szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Millimag > klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag)
            {
                szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Memoria -= klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria;
                szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Millimag -= klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag;
                File.WriteAllLines(gyoker + $"/{valasztottGep}/.szamitogep_config", szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).Select(x => x.KiIratas()).ToArray());
                var startDate = DateTime.Now.ToString();
                string[] tomb = [startDate, "AKTÍV", $"{klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag}", $"{klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria}"];
                string randomAzonosito = "";
                Random rnd = new Random();
                for (int i = 0; i < 6; i++)
                {
                    randomAzonosito += betuk[rnd.Next(0, betuk.Count)];
                }
                File.WriteAllLines(gyoker + $"/{valasztottGep}/{programNeve}-{randomAzonosito}", tomb);
                szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().ProgramPeldanyAzonositok.Add($"{programNeve}-{randomAzonosito}");
            }
            else
                Console.WriteLine("Nincs elég erőforrás!");
            Console.WriteLine("Sikeres futtatás!");
            Console.ReadKey();
            Console.Clear();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();
        }
        public static void SzamitoGepHozzaAdas()
        {
            Console.WriteLine("Adja meg a számítógép nevét! (csak az angol ABC kis- és nagybetűit és számjegyeket tartalmazhat!)");
            string? pcName = Console.ReadLine();
            while (Regex.IsMatch(pcName.ToLower(), @"[a-z]") == false || Regex.IsMatch(pcName.ToLower(), @"á|é|ő|ű|ú|ó|ü|ö|í"))
            {
                Console.WriteLine("Adja meg a számítógép nevét! (csak az angol ABC kis- és nagybetűit és számjegyeket tartalmazhat!)");
                pcName = Console.ReadLine();
            }
            Console.WriteLine("Adja meg a PC processzorkapacitását: ");
            int millimag = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Adja meg a PC memóriakapacitását: ");
            int memoria = Convert.ToInt32(Console.ReadLine());

            Directory.CreateDirectory(gyoker + $"/{pcName}");
            File.WriteAllLines(gyoker + $"/{pcName}/.szamitogep_config", $"{millimag}\n{memoria}".Split("\n"));
            //szamitogepConfigok.Add(new SzamitogepConfig(millimag,memoria,$"{gyoker}\\{pcName}"));
            Console.WriteLine("Sikeres létrehozás!");
            Console.ReadKey();
            Console.Clear();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();
        }
        public static void SzamitogepTorlese()
        {
            Console.Clear();
            Console.Write("Számítógépek:");
            szamitogepConfigok.ForEach(s => Console.Write($"\n\t{s.Eleres.Split("\\").Last()}"));
            Console.WriteLine("\n");
            Console.WriteLine("Adja meg a törlendő mappa nevét: ");
            string? mappaNev = Console.ReadLine();
            try
            {
                string[] files = Directory.GetFiles(gyoker + $"\\{mappaNev}").ToList().Select(x => x.Split("\\").Last()).ToArray();
                var futoProgramok = szamitogepekenFutoAlkalmazasok.Where(x => x.Value == gyoker+$"\\{mappaNev}").Select(x => x.Key).ToList();

                if (futoProgramok.Any(x => x.IsActive == "AKTÍV"))
                {
                    ProgramAthelyezese(futoProgramok, mappaNev);
                }

                else
                {
                    File.Delete(gyoker + $"\\{mappaNev}\\.szamitogep_config");
                    //futoProgramok.Where(x => x.IsActive == "INAKTÍV").ToList().ForEach(x => File.Delete(gyoker + $"\\{mappaNev}\\{x.FajlNeve}"));
                    Directory.Delete(gyoker + $"\\{mappaNev}");
                    Console.WriteLine("Sikeres törlése!");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                SzamitogepTorlese();
            }
            Console.ReadKey();
            Console.Clear();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();

        }
        public static void ProgramAdatainakModositasa()
        {
            klaszterLista.ForEach(x => Console.WriteLine($"Program Neve: {x.ProgramName}\nAktivitás: {x.MennyiActive}\nMillimag: {x.Millimag}\nMemória: {x.Memoria}\n{new string('-', 5)}"));
            Console.WriteLine("Melyik programot szeretné módosítani? (Létező program nevét adja meg!)");
            string? program = Console.ReadLine();
            while (klaszterLista.Any(x => x.ProgramName == program) == false)
            {
                Console.WriteLine("Melyik programot szeretné módosítani? (Létező program nevét adja meg!)");
                program = Console.ReadLine();
            }
            Console.WriteLine("Aktivitást[1], Millimagot[2], vagy a Memóriát[3] szeretné módosítani? Válasszon 1-től 3-ig!");
            string? szam = Console.ReadLine();
            while (Regex.IsMatch(szam, @"[1-3]") == false)
            {
                Console.WriteLine("Aktivitást[1], Millimagot[2], vagy a Memóriát[3] szeretné módosítani? Válasszon 1-től 3-ig!");
                szam = Console.ReadLine();
            }
            switch (szam)
            {
                case "1":
                    Console.WriteLine("Adja meg az új értéket: ");
                    int ujSzam = Convert.ToInt32(Console.ReadLine());
                    klaszterLista.Where(x => x.ProgramName == program).First().MennyiActive = ujSzam;
                    break;
                case "2":
                    Console.WriteLine("Adja meg az új értéket: ");
                    int ujSzam2 = Convert.ToInt32(Console.ReadLine());
                    klaszterLista.Where(x => x.ProgramName == program).First().Millimag = ujSzam2;
                    break;
                case "3":
                    Console.WriteLine("Adja meg az új értéket: ");
                    int ujSzam3 = Convert.ToInt32(Console.ReadLine());
                    klaszterLista.Where(x => x.ProgramName == program).First().Memoria = ujSzam3;
                    break;
                default:
                    break;
            }

            File.WriteAllLines(gyoker + "/.klaszter", klaszterLista.Select(x => x.KiIratas()).ToArray());
            Console.ReadKey();
            Console.Clear();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();
        }
        public static void ProgramAthelyezese(List<ProgramFolyamat> futoProgramok, string originalName)
        {
            Console.WriteLine("Ezen a szamítógépen még vannak AKTÍV programok! \nHelyezze át az AKTÍV programokat egy másik számítógépre!");
            futoProgramok.ForEach(x => Console.WriteLine($"\t{x.FajlNeve}:\n\t\tIndítás ideje: {x.InidtasIdeje}\n\t\tStátusz:{x.IsActive}"));
            Console.WriteLine();
            Console.WriteLine("Lehetséges számítógépek:");
            var lehetsegesSzamitogepek = szamitogepConfigok.Where(x => x.Eleres.Split("\\").Last() != originalName).ToList();
            lehetsegesSzamitogepek.ForEach(x => Console.WriteLine("\t" + x.Eleres.Split("\\").Last()));
            Console.WriteLine();
            Console.Write("Válasszon ki egy gépet: ");
            string? destinationName = Console.ReadLine();
            while (!lehetsegesSzamitogepek.Any(x => x.Eleres.Split("\\").Last() == destinationName))
            {
                Console.WriteLine("Létező gép nevét adja meg!");
                destinationName = Console.ReadLine();
            }

            Console.WriteLine("Biztos áthelyezi a programokat erre a gépre? (IGEN/NEM)");
            string? valasz = Console.ReadLine();
            if (valasz == "IGEN")
            {
                string originalPath = $"{gyoker}\\{originalName}";
                string newPath = $"{gyoker}\\{destinationName}";

                futoProgramok.Where(x => x.IsActive == "AKTÍV").ToList().ForEach(x => File.Copy($"{originalPath}\\{x.FajlNeve}", $"{newPath}\\{x.FajlNeve}", true));
                File.Delete(gyoker + $"\\{originalName}\\.szamitogep_config");
                futoProgramok.ForEach(x => File.Delete($"{originalPath}\\{x.FajlNeve}"));
                Directory.Delete(gyoker + $"\\{originalName}");
                Console.WriteLine("Sikeres törlése!");
                var torlendoGep = szamitogepConfigok.Where(x => x.Eleres.Contains(originalName)).First();
                //szamitogepConfigok.Remove(torlendoGep);
                //foreach (var item in szamitogepekenFutoAlkalmazasok)
                //{
                //    if (item.Value.Contains(originalName))
                //        szamitogepekenFutoAlkalmazasok.Remove(item.Key);
                //}
            }
            else
                MenuKiiratasa();
        }
        public static void EgyProgramLeallitasa()
        {
            string kluszterPath = Directory.GetFiles(gyoker).Where(x => x.Contains(".klaszter")).First();
            Console.WriteLine("Válassza ki a leállítandó programot:");

            klaszterLista.ForEach(x => Console.WriteLine($"\t{x.ProgramName}"));
            string? valasztottProgram = Console.ReadLine();

            while(!klaszterLista.Any(x => x.ProgramName == valasztottProgram))
            {
                Console.WriteLine("Létező program nevét adja meg!");
                valasztottProgram = Console.ReadLine();
            }

            var torlendoProgramKlaszteren = klaszterLista.Where(x => x.ProgramName == valasztottProgram).First();
            klaszterLista.Remove(torlendoProgramKlaszteren);
            klaszterLista.ForEach(x => File.WriteAllText(kluszterPath, x.KiIratas()));
            var torlendoProgramokGepeken = szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(valasztottProgram));

            foreach (var item in torlendoProgramokGepeken)
                File.Delete($"{item.Value}\\{item.Key.FajlNeve}");

            Console.WriteLine("Sikeres törlés!");
            Console.ReadKey();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();
        }
        public static void EgyAdottProgrampeldanyLeallitasa()
        {
            Console.Clear();
            List<string> programok = [];
            foreach (var item in szamitogepConfigok)
            {
                Console.WriteLine($"{item.Eleres.Split("\\").Last()}:");
                item.ProgramPeldanyAzonositok.ForEach(x =>
                {
                    programok.Add(x);
                    Console.WriteLine($"\t{x}");
                });
            }
            Console.WriteLine("Válasszon ki egy programpéldányt!");
            string? name = Console.ReadLine();
            while (!programok.Any(x => x == name))
            {
                Console.WriteLine("Létező programot válasszon!");
                name = Console.ReadLine();
            }

            var gep = szamitogepConfigok.Where(x => x.ProgramPeldanyAzonositok.Contains(name)).First().Eleres;
            File.Delete($"{gep}\\{name}");
            Console.WriteLine("Sikeres törlés!");
            Console.ReadKey();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            MenuKiiratasa();
        }
    }
}
