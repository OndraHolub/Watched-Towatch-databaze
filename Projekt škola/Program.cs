using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

internal class Program
{


    private static void Main(string[] args)
    {
        try
        {
            while (true)
            {
                Menu(); 
                char odezva = char.ToUpper(Console.ReadKey().KeyChar); //Získání vstupu, nezáleží zda je vstup zadán malým/velkým pismenem
                switch (odezva) //Rozhodování toho co se stane, ošetřeno aby uživatel mohl zadat pouze určené znaky
                {
                    case 'Q':
                        {
                            pridani pridavac = new pridani();
                            pridavac.Pridat_Noveho_Uzivatele();
                            break;
                        }
                    case 'E':
                        {
                            mazani pridavac = new mazani();
                            pridavac.Smazani_zaznam();
                            break;
                        }
                    case 'R':
                        {
                            Vypis_zaznamu pridavac = new Vypis_zaznamu();
                            pridavac.Výpis_záznamů();
                            break;
                        }
                    case 'S':
                        {
                            Console.WriteLine("Opouštite program");
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Neplatný vstup");
                            System.Threading.Thread.Sleep(1500);
                            break;
                        }
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        // Funkce

        void Menu()
        {
            Console.Clear();
            Console.WriteLine("Co chcete udělat?");
            Console.WriteLine("Q - Přidat nový film/seriál");
            Console.WriteLine("E - Smazat záznam");
            Console.WriteLine("R - Vypsat všechny záznamy");
            Console.WriteLine("S - Ukončit program");
        }
    }
    class mazani
    {
       public void Smazani_zaznam()
       {
            //deklarace souborů a proměnných
            string Soubor_serialy = @"..\..\..\..\seriály_videl.txt";
            string Soubor_filmy = @"..\..\..\..\filmy_videl.txt";
            string Soubor_serialy0 = @"..\..\..\..\seriály_nevidel.txt";
            string Soubor_filmy0 = @"..\..\..\..\filmy_nevidel.txt";
            string strodpoved;
            string soubor;
            int odpoved;
            //menu
            Console.Clear();
            Console.WriteLine("Z jakého seznamu chcete mazat?");
            Console.WriteLine("1 - Filmy, které jste viděl");
            Console.WriteLine("2 - Filmy, které jste neviděl");
            Console.WriteLine("3 - Seriály, které jste viděl");
            Console.WriteLine("4 - Seriály, které jste neviděl");
            strodpoved = Console.ReadLine();

            while(true)     //ověření zda bylo zadáno číslo, a jestli bylo v určitém rozmezí + určení z jakého souboru budeme mazat
            {
                if(int.TryParse(strodpoved, out odpoved) && odpoved <= 4 && odpoved > 0)
                {
                    if(odpoved == 1)
                    {
                        soubor = Soubor_filmy;
                        break;
                    }
                    else if(odpoved == 2)
                    {
                        soubor = Soubor_filmy0;
                        break;
                    }
                    else if(odpoved == 3)
                    {
                        soubor = Soubor_serialy;
                        break;
                    }
                    else
                    {
                        soubor = Soubor_serialy0;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Zadejte číslo, musí být 1-4");
                }
            }
           //AI ->
            string[] radky = File.ReadAllLines(soubor);

            // Výpis řádků s indexy
            Console.Clear();
            Console.WriteLine("Vyberte řádek k odstranění:");
            for (int i = 0; i < radky.Length; i++)
            {
                Console.WriteLine(radky[i]);
            }

            int vyber = korekce(1, radky.Length);

            // Vytvoření nového seznamu s přegenerovanými indexy
            // Smazání vybraného řádku a přečíslování indexů
            string[] noveRadky = radky
                .Where(line => !line.StartsWith(vyber + ",")) // Smaže řádek s vybraným indexem
                .Select((line, index) =>
                {
                    // Přepíše index (první číslo před čárkou)
                    int firstComma = line.IndexOf(',');
                    return (index + 1) + line.Substring(firstComma);
                })
                .ToArray();

            File.WriteAllLines(soubor, noveRadky);

            Console.WriteLine("Záznam byl smazán a indexy upraveny.");
            System.Threading.Thread.Sleep(2000);
       }   //<-- AI
        private int korekce(int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write("Zadejte číslo ({0}-{1}): ", min, max);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine("Neplatný vstup, zkuste to znovu.");
            }
        }
    }
    class Vypis_zaznamu
    {
        public void Výpis_záznamů()
        {
            //deklarace souborů a proměnných
            Console.Clear();
            string Soubor_serialy = @"..\..\..\..\seriály_videl.txt";
            string Soubor_filmy = @"..\..\..\..\filmy_videl.txt";
            string Soubor_serialy0 = @"..\..\..\..\seriály_nevidel.txt";
            string Soubor_filmy0 = @"..\..\..\..\filmy_nevidel.txt";
            int odpoved = 0;
            string strodpoved;
            while(true)
            {
                Console.WriteLine("Co by jste chtěl zobrazit?"); //menu
                Console.WriteLine("1 - Všechny filmy, které jste viděl");
                Console.WriteLine("2 - Všechny filmy, které jste neviděl");
                Console.WriteLine("3 - Všechny seriály, které jste viděl");
                Console.WriteLine("4 - všechny seriály, které jste neviděl");
                strodpoved = Console.ReadLine();

                if (int.TryParse(strodpoved, out odpoved) && odpoved <= 4 && odpoved > 0) //ověření zda uživatel zadal číslo, a jestli je číslo v určitém rozmezí
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Zadejte číslo, musí být 1-4");
                }
            }
            //rozhodnutí jaký soubor bude chtít uživatel načítat
            if (odpoved == 1) 
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6} | {4,-9}", "ID", "Jméno", "Žánr", "Dabing", "Hodnocení"); //vypsaní záhlaví
                Console.WriteLine("------------------------------------------------------------------------------------------");
                string[] radky = File.ReadAllLines(Soubor_filmy);
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] casti = radky[i].Split(",");
                    Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6} | {4,-9}", casti[0], casti[1], casti[2], casti[3], casti[4]); //vypsání údajů

                }
                Console.WriteLine("Pro pokračování zmáčkněte jakoukoliv klávesu");
                Console.ReadKey();
            }
            else if (odpoved == 2)
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6}", "ID", "Jméno", "Žánr", "Dabing");                      //vypsání záhlavý
                Console.WriteLine("------------------------------------------------------------------------------------------");
                string[] radky = File.ReadAllLines(Soubor_filmy0);
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] casti = radky[i].Split(",");
                    Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6}", casti[0], casti[1], casti[2], casti[3]); //vypsání údajů

                }
                Console.WriteLine("Pro pokračování zmáčkněte jakoukoliv klávesu");
                Console.ReadKey();
            }
            else if (odpoved == 3)
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6} | {4,-9}", "ID", "Jméno", "Žánr", "Dabing", "Hodnocení"); //vypsání záhlavý
                Console.WriteLine("------------------------------------------------------------------------------------------");
                string[] radky = File.ReadAllLines(Soubor_serialy);
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] casti = radky[i].Split(",");
                    Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6} | {4,-9}", casti[0], casti[1], casti[2], casti[3], casti[4]); //vypsání údajů

                }
                Console.WriteLine("Pro pokračování zmáčkněte jakoukoliv klávesu");
                Console.ReadKey();
            }
            else if(odpoved == 4)
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6}", "ID", "Jméno", "Žánr", "Dabing");                      //vypsání záhlavý
                Console.WriteLine("------------------------------------------------------------------------------------------");
                string[] radky = File.ReadAllLines(Soubor_serialy0);
                for (int i = 0; i < radky.Length; i++)
                {
                    string[] casti = radky[i].Split(",");
                    Console.WriteLine("{0,-3} | {1,-30} | {2,-30} | {3,-6}", casti[0], casti[1], casti[2], casti[3]); //vypsání údajů

                }
                Console.WriteLine("Pro pokračování zmáčkněte jakoukoliv klávesu");
                Console.ReadKey();
            }
        }
    }
    class pridani
    {
        //deklarace souborů a proměnných
        string Soubor_serialy = @"..\..\..\..\seriály_videl.txt";
        string Soubor_filmy = @"..\..\..\..\filmy_videl.txt";
        string Soubor_serialy0 = @"..\..\..\..\seriály_nevidel.txt";
        string Soubor_filmy0 = @"..\..\..\..\filmy_nevidel.txt";
        string Bob;
        int spodek;
        int vrsek;
        int videlcislo;
        string zanr1;
        string dabing1;
        int hodnoceni1;
        int odpoved;
        string soubor;
        string name;
        public void Pridat_Noveho_Uzivatele()
        {
            Console.Clear();
            film_serial();
            videl();
            if (videlcislo == 1) //program se ptá na údaje podle toho zda film videl, jestli ho neviděl tak se program neptá na hodnocení
            {
                jmeno();
                zanr();
                dabing();
                hodnoceni();
            }
            else
            {
                jmeno();
                zanr();
                dabing();

            }
            //soubor ukládá do správného souboru podle vstupu uživatele
            //Jestli film/serial videl(videlcislo == 1, tak to ukládá to souboru s film/serial_videl
            if (videlcislo == 1)
            {
                if (odpoved == 1) //proměnná odpověd zjistuje zda uživatel přidává film anebo seriál
                {
                    soubor = Soubor_filmy;
                    var pocet_rad = (File.ReadAllLines(soubor).Length + 1);
                    string[] text = { pocet_rad.ToString(), name, zanr1, dabing1, hodnoceni1.ToString() };
                    string textt = string.Join(",", text);
                    File.AppendAllText(soubor, textt + Environment.NewLine);
                    Console.WriteLine("{0} byl úspěšně přidán", Bob);
                    System.Threading.Thread.Sleep(2000);

                }
                else
                {
                    soubor = Soubor_serialy;
                    var pocet_rad = (File.ReadAllLines(soubor).Length + 1);
                    string[] text = {pocet_rad.ToString(), name, zanr1, dabing1, hodnoceni1.ToString() };
                    string textt = string.Join(",", text);
                    File.AppendAllText(soubor, textt + Environment.NewLine);
                    Console.WriteLine("{0} byl úspěšně přidán", Bob);
                    System.Threading.Thread.Sleep(2000);
                }
            }
            else
            {
                if (odpoved == 1)
                {
                    soubor = Soubor_filmy0;
                    var pocet_rad = (File.ReadAllLines(soubor).Length + 1);
                    string[] text = { pocet_rad.ToString(), name, zanr1, dabing1};
                    string textt = string.Join(",", text);
                    File.AppendAllText(soubor, textt + Environment.NewLine);
                    Console.WriteLine("{0} byl úspěšně přidán", Bob);
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    soubor = Soubor_serialy0;
                    var pocet_rad = (File.ReadAllLines(soubor).Length + 1);
                    string[] text = { pocet_rad.ToString(), name, zanr1, dabing1};
                    string textt = string.Join(",", text);
                    File.AppendAllText(soubor, textt + Environment.NewLine);
                    Console.WriteLine("{0} byl úspěšně přidán",Bob);
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }
        private void jmeno() //funkce na přidávání jména s ošetřením aby to nemohlo být prázdné anebo aby tam uživatel nezadal příliš mnoho znaků
        {
            while(true)
            {
                Console.WriteLine("jak se {0} jmenuje", Bob);
                name = Console.ReadLine();
                int namelenght = name.Length;
                if (namelenght >= 10 || string.IsNullOrEmpty(name) || namelenght < 3)
                {
                    Console.WriteLine("zadali jste moc/malo nebo žádné znaky");
                }
                else
                {
                    break;
                }
            }
        }
        private void film_serial() //funkce, která zjištuje zda uživatel přidává film či seriál
        {
            Console.WriteLine("Chcete přidat film nebo seriál");
            Console.WriteLine("1 - film");
            Console.WriteLine("2 - seriál");
            odpoved = 0;
            korekce(ref odpoved, 1, 2);

            if (odpoved == 1)
            {
                Bob = "film";
                return;
            }
            else
            {
                Bob = "serial";
                return;
            }

        }
        private void zanr() //funkce, která zjištuje žánr s ošetřením proti prázdnému vstupu anebo moc dlouhému/krátkému
        {
            while(true)
            {
                Console.WriteLine("jaký žanr má {0}", Bob);
                zanr1 = Console.ReadLine();
                int zanrpocet = zanr1.Length;
                if (zanrpocet < 3 || string.IsNullOrWhiteSpace(zanr1) || zanrpocet >= 11)
                {
                    Console.WriteLine("Zadali jste moc/málo znaků");
                }
                break;
            }
            
        }
        private void dabing() //funkce, která zjištuje jestli má film/seriál český dabing, má ošetření aby uživatel nemohl zadat něco jíného než číslo, to musí být v určitém rozsahu
        {
            Console.WriteLine("Má tento {0} český dabing?", Bob);
            Console.WriteLine("1 - Ano");
            Console.WriteLine("2 - Ne");
            int dabingg = 0;
            korekce(ref dabingg, 1, 2);

            if (dabingg == 1) //rozhodnování zda film/seriál dabing má
            {
                dabing1 = "Ano";
            }
            else if (dabingg == 2)
            {
                dabing1 = "Ne";
            }
        }
        private void videl() //funkce, která zjištuje zda uživatel film/seriál viděl, má ošetření aby bylo možno zadat jen číslo v určitém rozsahu
        {
            Console.WriteLine("Viděl jste už tento {0}", Bob);
            Console.WriteLine("1 - ano");
            Console.WriteLine("2 - ne");
            korekce(ref videlcislo, 1, 2);
        }
        private void hodnoceni() //funkce, která zjistuje hodnocení, má ošetření aby bylo možno zadat jen číslo v rozmezí 0-10
        {
            Console.WriteLine("hodnocení 0-10");
            korekce(ref hodnoceni1, 0, 10);
        }
        private void korekce(ref int y, int A, int B) //funkce, která ošetřuje vstupy, aby uživatel mohl zadat pouze číslo v určitém rozmezí
        {
            while (true)
            {
                string x = Console.ReadLine();
                y = 0;
                bool check = int.TryParse(x, out y);    
                if (check)
                {
                    if (y >= A && y <= B)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Zadejte číslo od {0} do {1}", A, B);
                        System.Threading.Thread.Sleep(3000);

                    }
                }
                else
                {
                    Console.WriteLine("Zadejte číslo");
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }
    }    
}