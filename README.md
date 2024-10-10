##################################################
 SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI
##################################################

1. Opis projektu
   SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI

   Aplikacja umożliwia swobodne wyszukiwanie przejazdu autobusów, zakub biletów oraz zarządzanie użytkownikami na twoich liniach autobusowych.

2. Architektura aplikacji

3. Opis każdej funkcji:
   
   Main()
   • To główna funkcja aplikacji która uruchamia program i prezentuje menu wyboru opcji użytkownikownikowi
   • Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody
 
   Aktualny_Rozklad()
   • Wyświetla aktualny rozkład jazdy
   • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń

   DodajPolaczenie()
   • Pozwala użytkownikowi na dodanie nowego połaczenia w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu.
   • Oblicza czas dojazdu, gdzie czas przyjazdu jest po północy

   Aktualizacja()
   • Umożliwia aktualizowanie istniejącego połaczenia na podstawie jego indeksu.
   • Pozwala zmienić godzinę odjazdu, przyjazdu oraz automatycznie aktualizuje czas przejazdu.

   UsunPolaczenie()
   • Usuwa połączenie z listy na pdstawie indeksu wybranego przez użytkownika
   • Po usunięciu wyświetla że połączenie zostało usunięte

   FiltrujPolaczenia()
   • Główna funkcja filtrowania pozwala użytkownikowi wybrać sposób filtrowania: po mieście, cenie lub czasie dojazdu.

   FiltrujPoMiescie()
   • Filtruje połączenie na podstawie podanego miasta

   FiltrujPoCenie()
   • Filtruje połączenie na podstawie maksymalnej ceny biletu

   KupBilet()
   • Pozwala użytkownikowi zakupić bilet na wybrane połaczenie, zapisując dane biletu do pliku bilety.txt

   ZapiszRozklad()
   • Zapisuje aktualny rozkład jazdy do pliku rozklad_jazdy.txt przed zakończeniem programu.
    
   ChatGPT

Powiedziałeś(-aś):
Opis funkcji i działalności:using System;
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
Szczegółowy opis każdej funkcji aplikacji, z przykładami użycia.
- Instrukcje dla użytkownika dotyczące obsługi programu (np. komendy konsolowe, jak
dodawać użytkowników, obliczać raty kredytowe, zarządzać zadaniami w systemie).
ChatGPT powiedział:
ChatGPT
Aplikacja w C# przedstawiona w kodzie to konsolowy program do zarządzania rozkładem jazdy i kupowania biletów. Program oferuje szereg funkcji takich jak dodawanie, aktualizowanie, usuwanie połączeń, filtrowanie połączeń oraz zakup biletów. Każda z funkcji jest opisana poniżej, a na końcu znajdują się instrukcje obsługi aplikacji.

Funkcje aplikacji:
Main()

To główna funkcja aplikacji, która uruchamia program i prezentuje menu wyboru opcji użytkownikowi.
Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody (np. DodajPolaczenie, UsunPolaczenie itp.).
Przykład użycia: Po uruchomieniu aplikacji użytkownik wprowadza numery opcji w menu konsolowym, aby wykonać określone operacje.

Aktualny_Rozklad()

Wyświetla aktualny rozkład jazdy.
Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń.
Przykład użycia: Użytkownik wybiera opcję [5] w menu, aby zobaczyć listę wszystkich dostępnych połączeń w formacie:

arduino
Skopiuj kod
[0] MiastoA - MiastoB, 06:04, 06:06, 2 min, 10 PLN
DodajPolaczenie()

Pozwala użytkownikowi na dodanie nowego połączenia w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu.
Oblicza czas dojazdu, uwzględniając sytuacje, gdzie czas przyjazdu jest po północy.
Przykład użycia: Po wybraniu opcji [1] użytkownik wprowadza nowe połączenie:

Skopiuj kod
MiastoA - MiastoB, 06:04, 08:04, 10
Wynik:

arduino
Skopiuj kod
Połączenie dodane: MiastoA - MiastoB, 06:04, 08:04, 120 min, 10 PLN
Aktualizacjia()

Umożliwia aktualizowanie istniejącego połączenia na podstawie jego indeksu.
Pozwala zmienić godzinę odjazdu, przyjazdu oraz automatycznie aktualizuje czas przejazdu.
Przykład użycia: Po wybraniu opcji [2], użytkownik wprowadza indeks połączenia, a następnie nowe godziny odjazdu i przyjazdu:

yaml
Skopiuj kod
Wprowadź indeks połączenia: 0
Wprowadź nową godzinę odjazdu: 07:00
Wprowadź nową godzinę przyjazdu: 09:00
Wynik:

arduino
Skopiuj kod
Połączenie zostało zaktualizowane: MiastoA - MiastoB, 07:00, 09:00, 120 min, 10 PLN
UsunPolaczenie()

Usuwa połączenie z listy na podstawie indeksu wybranego przez użytkownika.
Przykład użycia: Po wybraniu opcji [3], użytkownik podaje indeks połączenia do usunięcia:

arduino
Skopiuj kod
Wprowadź indeks połączenia do usunięcia: 0
Wynik:

Skopiuj kod
Połączenie usunięte.
FiltrujPolaczenia()

Główna funkcja do filtrowania, pozwala użytkownikowi wybrać sposób filtrowania: po mieście, cenie lub czasie dojazdu.
Przykład użycia: Po wybraniu opcji [4], użytkownik wybiera rodzaj filtru:

csharp
Skopiuj kod
Wybierz filtrację:
[1] Filtrowanie po mieście
[2] Filtrowanie po cenie
[3] Filtrowanie po czasie
FiltrujPoMiescie()

Filtruje połączenia na podstawie podanego miasta.
Przykład użycia: Użytkownik wybiera opcję [1] w menu filtrowania i wprowadza miasto:

yaml
Skopiuj kod
Wprowadź miasto: MiastoA
Wynik:

arduino
Skopiuj kod
Znalezione połączenia: MiastoA - MiastoB, 06:04, 08:04, 120 min, 10 PLN
FiltrujPoCenie()

Filtruje połączenia na podstawie maksymalnej ceny biletu.
Przykład użycia: Użytkownik wybiera opcję [2] w menu filtrowania i wprowadza maksymalną cenę:

yaml
Skopiuj kod
Podaj maksymalną cenę biletu: 10
Wynik:

arduino
Skopiuj kod
Znalezione połączenia: MiastoA - MiastoB, 06:04, 08:04, 120 min, 10 PLN
FiltrujPoCzasie()

Filtruje połączenia na podstawie maksymalnego czasu dojazdu.
Przykład użycia: Użytkownik wybiera opcję [3] w menu filtrowania i wprowadza maksymalny czas dojazdu:

yaml
Skopiuj kod
Podaj maksymalny czas dojazdu: 120
Wynik:

arduino
Skopiuj kod
Znalezione połączenia: MiastoA - MiastoB, 06:04, 08:04, 120 min, 10 PLN
KupBilet()

Pozwala użytkownikowi zakupić bilet na wybrane połączenie, zapisując dane biletu do pliku bilety.txt.
Przykład użycia: Po wybraniu opcji [6], użytkownik wprowadza indeks połączenia i swoje dane:

less
Skopiuj kod
Wprowadź indeks połączenia: 0
Podaj imię i nazwisko pasażera: Jan Kowalski
Wynik:

css
Skopiuj kod
Bilet został zakupiony i zapisany w pliku bilety.txt
ZapiszRozklad()

Zapisuje aktualny rozkład jazdy do pliku rozklad_jazdy.txt przed zakończeniem programu.
Przykład użycia: Po wybraniu opcji [0] program zapisuje rozkład jazdy do pliku i kończy działanie:

arduino
Skopiuj kod
Rozkład jazdy został zapisany do pliku: rozklad_jazdy.txt
Instrukcje obsługi programu:
Uruchom program.

Wybierz jedną z dostępnych opcji:

[1] Dodaj połączenie: Dodaj nowe połączenie, podając informacje w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu.
[2] Zaaktualizuj połączenie: Zaktualizuj istniejące połączenie, podając jego indeks.
[3] Usuń połączenie: Usuń połączenie na podstawie indeksu.
[4] Filtruj: Filtruj połączenia według miasta, ceny lub czasu dojazdu.
[5] Aktualny rozkład: Zobacz listę wszystkich połączeń.
[6] Kup bilet: Wybierz połączenie i kup bilet, podając swoje imię i nazwisko.
[0] Zakończ: Zapisz aktualny rozkład i zakończ program.
Postępuj zgodnie z instrukcjami pojawiającymi się w konsoli, aby wykonać wybrane operacje.
   
   
   
   
   


Działanie: 
pierwsze wyświetla się menu z opcjami: 
1. Zaloguj się
2. Zarejestruj się
 
Logowanie na podstawie adresu email i hasła 
Rejestracja: imię, nazwisko, adres email, hasło
 
Po zalogowaniu w zależności od uprawnień:

Użytkownik:
 1. Zmiana danych użytkownika
 2. Zmiana hasła
 3. Usunięcie konta
 9. Wyloguj się
 
 Administrator:
 1. Zarządzanie użytkownikami
    a. Dodaj użytkownika
    b. Edytuj użytkownika
    c. Usuń użytkownika
 2. Przeglądanie listy uztykownikow
 9. Wyloguj się

