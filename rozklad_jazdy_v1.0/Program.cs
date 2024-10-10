using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rozklad
{
    internal class Program
    {
        // Lista przechowująca wszystkie połączenia
        static List<string> rozkladJazdy = new List<string>();

        // Ścieżka do pliku, w którym zapisany jest rozkład jazdy
        static string sciezkaPliku = "rozklad_jazdy.txt";
        static string ticketPath = "bilety.txt"; // File path for tickets

        static void Main(string[] args)
        {
            // Odczytaj rozkład jazdy z pliku, jeśli plik istnieje
            if (File.Exists(sciezkaPliku))
            {
                rozkladJazdy.AddRange(File.ReadAllLines(sciezkaPliku));
            }

            // Wyświetlenie powitania i aktualnego rozkładu
            Console.WriteLine("Grupa - Romper - Rozklad");
            Console.WriteLine("O to aktualny rozklad jazdy: ");
            Aktualny_Rozklad();

            // Główna pętla programu do obsługi wyboru opcji
            while (true)
            {
                Console.WriteLine("Wybierz jedna z opcji 1-6");
                Console.WriteLine("[1] Dodaj polaczenie");
                Console.WriteLine("[2] Zaaktualizuj polaczenie");
                Console.WriteLine("[3] Usuwanie polaczenia");
                Console.WriteLine("[4] Filtruj");
                Console.WriteLine("[5] Aktualny rozklad");
                Console.WriteLine("[6] Kup Bilet");
                Console.WriteLine("[0] Zakończ");

                // Odczytanie wyboru użytkownika
                int wybor = Convert.ToInt32(Console.ReadLine());

                // Przełączanie się między opcjami menu
                switch (wybor)
                {
                    case 1:
                        DodajPolaczenie();  // Dodawanie nowego połączenia
                        break;
                    case 2:
                        Aktualizacjia();  // Aktualizowanie istniejącego połączenia
                        break;
                    case 3:
                        UsunPolaczenie();  // Usuwanie połączenia
                        break;
                    case 4:
                        FiltrujPolaczenia();  // Filtrowanie połączeń
                        break;
                    case 5:
                        Aktualny_Rozklad();  // Wyświetlenie aktualnego rozkładu
                        break;
                    case 6:
                        KupBilet();          // opcja do kupna biletu
                        break;
                    case 0:
                        ZapiszRozklad();  // Zapis rozkładu do pliku i zakończenie programu
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }

        // Funkcja wyświetlająca aktualny rozkład jazdy
        static void Aktualny_Rozklad()
        {
            if (rozkladJazdy.Count == 0)
            {
                Console.WriteLine("Brak połączeń w rozkładzie.");
            }
            else
            {
                // Wyświetlanie każdego połączenia z indeksem
                for (int i = 0; i < rozkladJazdy.Count; i++)
                {
                    Console.WriteLine($"[{i}] {rozkladJazdy[i]}");
                }
            }
        }

        // Funkcja dodająca nowe połączenie
        static void DodajPolaczenie()
        {
            Console.WriteLine("Wprowadź nowe połączenie (w formacie: 'MiastoA - MiastoB, Godzina Odjazdu (np. 06:04), Godzina Przyjazdu (np. 06:06), Cena biletu'):");

            string nowePolaczenie = Console.ReadLine();

            // Parsowanie godzin odjazdu i przyjazdu w celu obliczenia czasu dojazdu
            string[] czesci = nowePolaczenie.Split(',');
            if (czesci.Length >= 4)
            {
                TimeSpan czasOdjazdu = TimeSpan.Parse(czesci[1].Trim());
                TimeSpan czasPrzyjazdu = TimeSpan.Parse(czesci[2].Trim());

                // Obsługa sytuacji, gdy podróż trwa przez północ (czas przyjazdu < czas odjazdu)
                if (czasPrzyjazdu < czasOdjazdu)
                {
                    czasPrzyjazdu = czasPrzyjazdu.Add(new TimeSpan(24, 0, 0));  // Dodaj jeden dzień do czasu przyjazdu
                }

                // Obliczanie czasu dojazdu
                TimeSpan czasDojazdu = czasPrzyjazdu - czasOdjazdu;

                // Dodanie połączenia do listy z czasem dojazdu i ceną biletu
                string polaczenieZCzasem = $"{czesci[0]}, {czesci[1]}, {czesci[2]}, {czasDojazdu.TotalMinutes} min, {czesci[3].Trim()} PLN";
                rozkladJazdy.Add(polaczenieZCzasem);

                Console.WriteLine("Połączenie dodane.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy format danych.");
            }
        }

        // Funkcja aktualizująca istniejące połączenie
        static void Aktualizacjia()
        {
            Console.WriteLine("Wprowadź indeks połączenia do zmiany:");
            int indeks = Convert.ToInt32(Console.ReadLine());

            // Sprawdzenie, czy indeks jest prawidłowy
            if (indeks >= 0 && indeks < rozkladJazdy.Count)
            {
                Console.WriteLine("Wprowadź nową godzinę odjazdu (HH:mm):");
                string nowaGodzinaOdjazdu = Console.ReadLine();
                Console.WriteLine("Wprowadź nową godzinę przyjazdu (HH:mm):");
                string nowaGodzinaPrzyjazdu = Console.ReadLine();

                string[] czesci = rozkladJazdy[indeks].Split(',');

                // Obliczanie nowego czasu dojazdu
                TimeSpan nowyCzasOdjazdu = TimeSpan.Parse(nowaGodzinaOdjazdu);
                TimeSpan nowyCzasPrzyjazdu = TimeSpan.Parse(nowaGodzinaPrzyjazdu);
                if (nowyCzasPrzyjazdu < nowyCzasOdjazdu)
                {
                    nowyCzasPrzyjazdu = nowyCzasPrzyjazdu.Add(new TimeSpan(24, 0, 0));  // Dodaj jeden dzień do czasu przyjazdu
                }

                TimeSpan nowyCzasDojazdu = nowyCzasPrzyjazdu - nowyCzasOdjazdu;

                // Aktualizacja danych 
                rozkladJazdy[indeks] = $"{czesci[0]}, {nowaGodzinaOdjazdu}, {nowaGodzinaPrzyjazdu}, {nowyCzasDojazdu.TotalMinutes} min, {czesci[4].Trim()} PLN";
                Console.WriteLine("Połączenie zostało zaktualizowane.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy indeks.");
            }
        }

        // Funkcja usuwająca połączenie
        static void UsunPolaczenie()
        {
            Console.WriteLine("Wprowadź indeks połączenia do usunięcia:");
            int indeks = Convert.ToInt32(Console.ReadLine());

            // Sprawdzenie, czy indeks jest prawidłowy
            if (indeks >= 0 && indeks < rozkladJazdy.Count)
            {
                rozkladJazdy.RemoveAt(indeks);  // Usunięcie połączenia z listy
                Console.WriteLine("Połączenie usunięte.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy indeks.");
            }
        }

        // Funkcja obsługująca filtrowanie połączeń
        static void FiltrujPolaczenia()
        {
            Console.WriteLine("Wybierz filtrację:");
            Console.WriteLine("[1] Filtrowanie po mieście");
            Console.WriteLine("[2] Filtrowanie po cenie (np. 'do 50 PLN')");
            Console.WriteLine("[3] Filtrowanie po czasie dojazdu (np. 'do 60 minut')");

            // Wybór filtru przez użytkownika
            int wybor = Convert.ToInt32(Console.ReadLine());

            // Wywołanie odpowiedniej funkcji na podstawie wyboru
            switch (wybor)
            {
                case 1:
                    FiltrujPoMiescie();  // Filtrowanie po mieście
                    break;
                case 2:
                    FiltrujPoCenie();  // Filtrowanie po cenie
                    break;
                case 3:
                    FiltrujPoCzasie();  // Filtrowanie po czasie dojazdu
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór filtru.");
                    break;
            }
        }

        // Funkcja filtrująca po mieście
        static void FiltrujPoMiescie()
        {
            Console.WriteLine("Wprowadź miasto do filtrowania:");
            string miasto = Console.ReadLine();
            var wyniki = rozkladJazdy.Where(p => p.Contains(miasto)).ToList();

            // Wyświetlenie wyników filtrowania
            if (wyniki.Count > 0)
            {
                Console.WriteLine("Znalezione połączenia:");
                foreach (var polaczenie in wyniki)
                {
                    Console.WriteLine(polaczenie);
                }
            }
            else
            {
                Console.WriteLine("Brak połączeń dla podanego miasta.");
            }
        }

        // Funkcja filtrująca po cenie biletu
        static void FiltrujPoCenie()
        {
            Console.WriteLine("Podaj maksymalną cenę biletu (PLN):");
            decimal maxCena = Convert.ToDecimal(Console.ReadLine());

            // Sprawdza, czy jest co najmniej 5 części 
            var wyniki = rozkladJazdy.Where(p =>
            {
                string[] czesci = p.Split(',');
                if (czesci.Length >= 5 && decimal.TryParse(czesci[4].Trim().Replace("PLN", ""), out decimal cena))
                {
                    return cena <= maxCena;
                }
                return false;
            }).ToList();

            // Wyświetlenie wyników filtrowania
            if (wyniki.Count > 0)
            {
                Console.WriteLine("Znalezione połączenia:");
                foreach (var polaczenie in wyniki)
                {
                    Console.WriteLine(polaczenie);
                }
            }
            else
            {
                Console.WriteLine("Brak połączeń w tej cenie.");
            }
        }

        // Funkcja filtrująca po czasie dojazdu
        static void FiltrujPoCzasie()
        {
            Console.WriteLine("Podaj maksymalny czas dojazdu (w minutach):");
            int maxCzas = Convert.ToInt32(Console.ReadLine());

            var wyniki = rozkladJazdy.Where(p =>
            {
                string[] czesci = p.Split(',');
                if (czesci.Length >= 4 && czesci[3].Trim().EndsWith("min"))
                {
                    if (double.TryParse(czesci[3].Trim().Replace("min", "").Trim(), out double czasDojazdu))
                    {
                        return czasDojazdu <= maxCzas;
                    }
                }
                return false;
            }).ToList();

            // Wyświetlenie wyników filtrowania
            if (wyniki.Count > 0)
            {
                Console.WriteLine("Znalezione połączenia:");
                foreach (var polaczenie in wyniki)
                {
                    Console.WriteLine(polaczenie);
                }
            }
            else
            {
                Console.WriteLine("Brak połączeń z takim czasem dojazdu.");
            }
        }

        // Funkcja do kupowania biletu
        static void KupBilet()
        {
            Console.WriteLine("Wybierz połączenie do zakupu biletu (podaj indeks):");
            int indeks = Convert.ToInt32(Console.ReadLine());

            // Sprawdzenie, czy indeks jest prawidłowy
            if (indeks >= 0 && indeks < rozkladJazdy.Count)
            {
                Console.WriteLine("Podaj imię i nazwisko pasażera:");
                string imieNazwisko = Console.ReadLine();

                // Uzyskanie indeksu
                string polaczenie = rozkladJazdy[indeks];
                string[] czesci = polaczenie.Split(',');

                // Generowanie biletu
                string bilet = $"Bilet:\nPasażer: {imieNazwisko}\nPołączenie: {czesci[0]} -> {czesci[1]}\nOdjazd: {czesci[1]}\nPrzyjazd: {czesci[2]}\nCzas dojazdu: {czesci[3]}\nCena: {czesci[4]}\n\n";
                File.AppendAllText(ticketPath, bilet); // Zapisanie biletu do pliku

                Console.WriteLine("Bilet został zakupiony i zapisany(w pliku bilety.txt)");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy indeks połączenia.");
            }
        }

        // Funkcja zapisująca rozkład do pliku
        static void ZapiszRozklad()
        {
            File.WriteAllLines(sciezkaPliku, rozkladJazdy);
            Console.WriteLine($"Rozkład jazdy został zapisany do pliku: {sciezkaPliku}");
        }
    }
}
