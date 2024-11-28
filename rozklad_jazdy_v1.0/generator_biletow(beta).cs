// Program umozliwnia generowanie biletow w formie jpg jest to ulepszona wersja program1.cs jednakze nie jest jeszcze dopracowana i moga wystepowac bledy
// Zeby program dzialal trzeba dodac System.Drawing w odwolaniach oraz pobrac biblioteke QRCoder
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
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
         
            Console.ForegroundColor = ConsoleColor.Blue;

            // Wyświetlenie powitania i aktualnego rozkładu
            Console.WriteLine("   ____                                                                             _    _           _ ");
            Console.WriteLine("  / ___|_ __ _   _ _ __   __ _   _ __ ___  _ __ ___  _ __   ___ _ __   _ __ ___ ___| | _| | __ _  __| |");
            Console.WriteLine(" | |  _| '__| | | | '_ \\ / _` | | '__/ _ \\| '_ ` _ \\| '_ \\ / _ | '__| | '__/ _ |_  | |/ | |/ _` |/ _` |");
            Console.WriteLine(" | |_| | |  | |_| | |_) | (_| | | | | (_) | | | | | | |_) |  __| |    | | | (_) / /|   <| | (_| | (_| |");
            Console.WriteLine("  \\____|_|   \\__,_| .__/ \\__,_| |_|  \\___/|_| |_| |_| .__/ \\___|_|    |_|  \\___/___|_|\\_|_|\\__,_|\\__,_|");
            Console.WriteLine("                  |_|                               |_|                                                ");
           
         
            Console.ResetColor();
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("O to aktualny rozklad jazdy: ");
            Console.ResetColor();
            Aktualny_Rozklad();

            // Główna pętla programu do obsługi wyboru opcji
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWybierz jedna z opcji 1-6: \n");
                Console.ResetColor();
                Console.WriteLine("[1] Dodaj polaczenie");
                Console.WriteLine("[2] Zaaktualizuj polaczenie");
                Console.WriteLine("[3] Usuwanie polaczenia");
                Console.WriteLine("[4] Filtruj");
                Console.WriteLine("[5] Aktualny rozklad");
                Console.WriteLine("[6] Kup Bilet");
                Console.WriteLine("[7] Zapisz rozklad");
                Console.WriteLine("[0] Zakończ");


                try
                {
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
                        case 7:
                            ZapiszRozklad();  // Zapis rozkładu do pliku i zakończenie programu
                            break;
                        case 0:
                            ZapiszRozklad();
                            return;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wprowadzono nieprawidłowy format. Proszę wprowadzić liczbę.");
                    Console.ResetColor();
                }
            }
            }

            // Funkcja wyświetlająca aktualny rozkład jazdy
            static void Aktualny_Rozklad()
        {
            
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.ResetColor();
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
                string nowaGodzinaOdjazdu;
                string nowaGodzinaPrzyjazdu;
                TimeSpan nowyCzasOdjazdu;
                TimeSpan nowyCzasPrzyjazdu;

                // Wprowadzanie godziny odjazdu z obsługą błędu
                while (true)
                {
                    Console.WriteLine("Wprowadź nową godzinę odjazdu (HH:mm):");
                    nowaGodzinaOdjazdu = Console.ReadLine();

                    if (TimeSpan.TryParse(nowaGodzinaOdjazdu, out nowyCzasOdjazdu))
                    {
                        // Jeśli godzina jest poprawna, wychodzimy z pętli
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Błędny format godziny! Spróbuj ponownie.");
                    }
                }

                // Wprowadzanie godziny przyjazdu z obsługą błędu
                while (true)
                {
                    Console.WriteLine("Wprowadź nową godzinę przyjazdu (HH:mm):");
                    nowaGodzinaPrzyjazdu = Console.ReadLine();

                    if (TimeSpan.TryParse(nowaGodzinaPrzyjazdu, out nowyCzasPrzyjazdu))
                    {
                        // Jeśli godzina jest poprawna, wychodzimy z pętli
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Błędny format godziny! Spróbuj ponownie.");
                    }
                }

                string[] czesci = rozkladJazdy[indeks].Split(',');

                // Obliczanie nowego czasu dojazdu
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

            // Validate user input for index
            if (!int.TryParse(Console.ReadLine(), out int indeks) || indeks < 0 || indeks >= rozkladJazdy.Count)
            {
                Console.WriteLine("Nieprawidłowy indeks połączenia. Spróbuj ponownie.");
                return;
            }

            Console.WriteLine("Podaj imię pasażera:");
            string imie = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko pasażera:");
            string Nazwisko = Console.ReadLine();

            string polaczenie = rozkladJazdy[indeks];

            string[] czesci = polaczenie.Split(',').Select(c => c.Trim()).ToArray();



            if (czesci.Length < 5)
            {
                Console.WriteLine("Nieprawidłowe połączenie. Sprawdź dane rozkładu.");
                return;
            }


            string bilet = $"Bilet:\nPasażer: {imie}{Nazwisko}\nPołączenie: {czesci[0]} -> {czesci[1]}\nOdjazd: {czesci[1]}\nPrzyjazd: {czesci[2]}\nCena: {czesci[4]}\n\n";
            File.AppendAllText(ticketPath, bilet);


            GenerateTicketImage(imie, Nazwisko, czesci[0], czesci[1], czesci[1], czesci[2], czesci[4]);

            Console.WriteLine("Bilet został zakupiony i zapisany (w pliku bilety.txt)");
        }





        private static void GenerateTicketImage(string imie, string Nazwisko, string from, string to, string godzOdjazdu, string godzinaPrzyjazdu, string cena)
        {
            using (Bitmap image1 = new Bitmap("bilet.jpg", true))
            {
                Graphics graphics = Graphics.FromImage(image1);
                Brush brush = new SolidBrush(Color.Black);
                Font arial = new Font("Arial", 10, FontStyle.Regular);
                Font arialBoldItalic = new Font("Arial", 15, FontStyle.Bold | FontStyle.Italic);

                graphics.DrawString(imie, arial, brush, new Rectangle(200, 1360, 450, 100));
                graphics.DrawString(Nazwisko, arial, brush, new Rectangle(270, 1470, 450, 100));
                graphics.DrawString(cena, arial, brush, new Rectangle(670, 1410, 550, 100));
                graphics.DrawString(to, arialBoldItalic, brush, new Rectangle(85, 570, 450, 1000));
                graphics.DrawString(from, arialBoldItalic, brush, new Rectangle(110, 500, 650, 100));
                graphics.DrawString(godzinaPrzyjazdu, arialBoldItalic, brush, new Rectangle(530, 570, 450, 100));

                string qrText = $"{imie} {Nazwisko}, {cena}, Trasa: {from}, GodzinaWyjazdu: {to}, GodzinaPrzyjazdu: {godzinaPrzyjazdu}";
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.L))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            Bitmap qrCodeImage = qrCode.GetGraphic(20);
                            graphics.DrawImage(qrCodeImage, new Rectangle(300, 1650, 350, 350));
                        }
                    }
                }

                string fileName = $"bilet_{DateTime.Now:ddssHH}.jpg";
                image1.Save(fileName, GetEncoderInfo("image/jpeg"), new EncoderParameters(1) { Param = { [0] = new EncoderParameter(Encoder.Quality, 25L) } });
            }
        }


        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        // Funkcja zapisująca rozkład do pliku
        static void ZapiszRozklad()
        {
            File.WriteAllLines(sciezkaPliku, rozkladJazdy);
            Console.WriteLine($"Rozkład jazdy został zapisany do pliku: {sciezkaPliku}");
            
        }
    }
}



