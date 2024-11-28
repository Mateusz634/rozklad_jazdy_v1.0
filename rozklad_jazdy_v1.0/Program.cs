using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;

namespace Manager
{
    internal class Manager
    {
        public class User
        {
            public string Imie { get; private set; }
            public string Nazwisko { get; private set; }
            public string Email { get; private set; }
            public string Haslo { get; private set; }
            public string Uprawnienia { get; private set; }

            public User(string imie, string nazwisko, string email, string haslo, string uprawnienia)
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Email = email;
                Haslo = haslo;
                Uprawnienia = uprawnienia;
            }

            public void zmianaHasla()
            {
                string stareHaslo;
                string noweHaslo;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPodaj stare haslo: ");
                stareHaslo = Console.ReadLine()!;
                Console.WriteLine("Podaj nowe haslo: ");
                noweHaslo = Console.ReadLine()!;
                if (SprawdzHaslo(stareHaslo) == true)
                {
                    if (SprawdzPoprawnoscHasla(noweHaslo) == true)
                    {
                        Haslo = noweHaslo;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Haslo zostalo poprawnie zmienione!\n");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Blad - zły format hasla!\n");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bledne haslo!\n");
                }


            }
            public bool SprawdzHaslo(string podaneHaslo)
            {
                return Haslo == podaneHaslo;
            }

            public bool SprawdzEmail(string podanyEmail)
            {
                return Email == podanyEmail;
            }

            public string sprawdzUprawnienia(string podaneUprawnienia)
            {
                return Uprawnienia;
            }

            public void WyswietlDane()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n------");
                Console.WriteLine(" DANE ");
                Console.WriteLine("------");
                Console.WriteLine($"Imię: {Imie}, Nazwisko: {Nazwisko}, Email: {Email}");
                Console.WriteLine("---------------");
                Console.WriteLine($"Haslo: {Haslo}");
                Console.WriteLine("---------------------------------------------------\n");
            }
            public void edycjaDanych(string imie, string nazwisko, string email, string haslo, string uprawnienia)
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Email = email;
                Haslo = haslo;
                Uprawnienia = uprawnienia;
            }
        }

        static List<string> rozkladJazdy = new List<string>();

        static List<User> listaUzytkownikow = new List<User>();

        // Ścieżka do pliku, w którym zapisany jest rozkład jazdy
        static string sciezkaPliku = "rozklad_jazdy.txt";
        static string ticketPath = "bilety.txt";

        public static bool SprawdzPoprawnoscEmail(string email)
        {

            string wzorzec = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, wzorzec);
        }
        private static bool CzyIstniejEmail(string email)
        {
            if (listaUzytkownikow.Any(u => u.SprawdzEmail(email)))
            {
                return true;
            }
            else return false;
        }

        public static bool SprawdzPoprawnoscHasla(string haslo)
        {
            if (haslo.Length >= 5 || haslo.Length <= 14)
            {
                if (haslo.Any(char.IsUpper))
                {
                    if (haslo.Any(char.IsLower))
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;

        }
        private static void Main(string[] args)
        {

            if (!File.Exists("users.txt"))
            {
                File.Create("users.txt").Dispose();

            }

            if (File.Exists(sciezkaPliku))
            {
                rozkladJazdy.AddRange(File.ReadAllLines(sciezkaPliku));
            }

            foreach (var line in File.ReadLines("users.txt"))
            {
                string[] parts = line.Split(';');

                if (parts.Length <= 5)
                {
                    listaUzytkownikow.Add(new User(parts[0], parts[1], parts[2], parts[3], parts[4]));
                }
            }
            do
            {
                try
                {
                    int wybor = 0;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("===================================");
                    Console.WriteLine(" Program Zarządzania Użytkownikami ");
                    Console.WriteLine("===================================");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nWybierz opcję:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("1. Zaloguj się");
                    Console.WriteLine("2. Zarejestruj się");
                    Console.WriteLine("9. Zakończ działanie programu");
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write("\nTwój wybór: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    wybor = int.Parse(Console.ReadLine()!);

                    switch (wybor)
                    {
                        case 1:
                            Console.WriteLine("\n");
                            logowanie();
                            wybor = 0;
                            break;
                        case 2:
                            Console.WriteLine("\n");
                            rejestracja();
                            wybor = 0;
                            break;

                        case 9:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" ZAKOŃCZENIE DZIAŁANIA PROGRAMU ");
                            Console.WriteLine("================================");
                            Thread.Sleep(1000);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            return;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nBłąd! - Nie ma takiej opcji\n");
                            break;
                    }
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBłąd! - {error.Message} \n");
                }

            } while (true);


        }

        public static void Panel(string email)
        {
            bool inPanel = true;
            int wybor = 0;
            var listaUprawnien = new string[] { "admin", "user" };

            User uzytkownik = listaUzytkownikow.Find(u => u.SprawdzEmail(email))!;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("-----------------------");
            Console.WriteLine(" Zalogowano pomyślnie! ");
            Console.WriteLine("-----------------------");
            Thread.Sleep(2000);
            Console.Clear();
            do
            {
                try
                {
                    if (uzytkownik.Uprawnienia == "admin")
                    {
                        Start:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("======================");
                        Console.WriteLine(" PANEL ADMINISTRATORA ");
                        Console.WriteLine("======================");

                        string data = DateTime.Now.ToShortDateString();
                        string godzina = DateTime.Now.ToString("HH:mm");
                        Console.WriteLine($"\nGodzina: {godzina}");
                        Console.WriteLine($"\nData {data}\n");
                        Console.WriteLine("===================");

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\nWybierz opcję:");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("1. Rozklad jazdy");
                        Console.WriteLine("2. Ustawienia");
                        Console.WriteLine("9. Wyloguj");
                        Console.ForegroundColor = ConsoleColor.Red;

                        wybor = 0;
                        Console.Write("\nTwój wybór: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        wybor = int.Parse(Console.ReadLine()!);


                        if (wybor == 9)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine("-----------------------");
                            Console.WriteLine(" Wylogowano pomyślnie! ");
                            Console.WriteLine("-----------------------");
                            Thread.Sleep(2000);
                            Console.Clear();
                            inPanel = false;
                            break;
                        }

                        switch(wybor)
                        {
                            case 1:

                                while(true)
                                { 
                                Aktualny_Rozklad();
                                    Console.Clear();
                                    Console.WriteLine("Wybierz jedna z opcji 1-6");
                                    Console.WriteLine("[1] Dodaj polaczenie");
                                    Console.WriteLine("[2] Zaaktualizuj polaczenie");
                                    Console.WriteLine("[3] Usuwanie polaczenia");
                                    Console.WriteLine("[4] Filtruj");
                                    Console.WriteLine("[5] Aktualny rozklad");
                                    Console.WriteLine("[6] Kup Bilet");
                                    Console.WriteLine("[9] Zakończ");

                                    // Odczytanie wyboru użytkownika
                                    int wyborRozklad = Convert.ToInt32(Console.ReadLine());

                                    // Przełączanie się między opcjami menu
                                    switch (wyborRozklad)
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
                                        case 9:
                                            ZapiszRozklad();  // Zapis rozkładu do pliku i zakończenie programu
                                            Console.Clear();
                                            goto Start;
                                        default:
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine("\nBłąd! - Nie ma takiej opcji");
                                        break;
                                    }
                                }
                            case 2:

                                while (true)
                                {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("\nWybierz opcję:");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("1. Wyswietl liste uzytkownikow");
                                Console.WriteLine("2. Zmiana hasla");
                                Console.WriteLine("3. Usuwanie uzytkownika");
                                Console.WriteLine("4. Dodaj uzytkownika");
                                Console.WriteLine("5. Edytuj uzytkownika");
                                Console.WriteLine("9. Wroc do menu glownego");
                                Console.ForegroundColor = ConsoleColor.Red;

                                wybor = 0;
                                Console.Write("\nTwój wybór: ");
                                Console.ForegroundColor = ConsoleColor.White;
                                wybor = int.Parse(Console.ReadLine()!);

                                    switch (wybor)
                                    {
                                        case 1:
                                            // WYSWIETLANIE LISTY UZYTKOWNIKA
                                            wyswietlUzytkownikow(email);
                                            break;
                                        case 2:
                                            // ZMIANA HASLA
                                            Console.Clear();
                                            uzytkownik.zmianaHasla();
                                            zapisywanieUzytkownikowDoPliku("users.txt");
                                            break;
                                        case 3:
                                            // USUWANIE UZYTKOWNIKA
                                            usuwanieUzytkownikow(email);
                                            break;
                                        case 4:
                                            // DODAJ UZYTKOWNIKA
                                            dodawanieUzytkownika();
                                            break;
                                        case 5:
                                            // EDYCJA UZYTKOWNIKA
                                            edytowanieUzytkownika(email);
                                            break;
                                        case 9:
                                            Console.Clear();
                                            goto Start;
                                        default:
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine("\nBłąd! - Nie ma takiej opcji");
                                            break;
                                    }
                                }

                        }
                        
                    }
                    else if (uzytkownik.Uprawnienia == "user")
                    {

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("===================");
                        Console.WriteLine(" PANEL UŻYTKOWNIKA ");
                        Console.WriteLine("===================");


                        string data = DateTime.Now.ToShortDateString();
                        string godzina = DateTime.Now.ToString("HH:mm");
                        Console.WriteLine($"\nGodzina: {godzina}");
                        Console.WriteLine($"\nData {data}\n");
                        Console.WriteLine("===================");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\nWybierz opcję:");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("1. Wyswietl dane uzytkownika");
                        Console.WriteLine("2. Zmiana hasla");
                        Console.WriteLine("3. Zmiana danych");
                        Console.WriteLine("4. Usuwanie konta");
                        Console.WriteLine("9. Wyloguj");
                        Console.ForegroundColor = ConsoleColor.Red;

                        wybor = 0;
                        Console.Write("\nTwój wybór: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        wybor = int.Parse(Console.ReadLine()!);

                        if (wybor == 9)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine("-----------------------");
                            Console.WriteLine(" Wylogowano pomyślnie! ");
                            Console.WriteLine("-----------------------");
                            Thread.Sleep(2000);
                            Console.Clear();
                            inPanel = false;
                            break;
                        }

                        switch (wybor)
                        {
                            case 1:
                                uzytkownik.WyswietlDane();
                                break;
                            case 2:
                                uzytkownik.zmianaHasla();

                                zapisywanieUzytkownikowDoPliku("users.txt");

                                break;
                            case 3:
                                string in_imie, in_nazwisko, in_email, in_haslo = uzytkownik.Haslo, in_uprawnienia = "user";

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDane uzytkownika: ");
                                Console.WriteLine($"Imie: {uzytkownik.Imie} \nNazwisko: {uzytkownik.Nazwisko} \nHaslo: {uzytkownik.Haslo}");
                                Console.WriteLine($"Email: {uzytkownik.Email} \nUprawnienia: {uzytkownik.Uprawnienia}\n");

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\n-----------------");
                                Console.WriteLine(" Podaj nowe dane \n\n");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("\nPodaj nowe imie:");
                                in_imie = Console.ReadLine()!;
                                Console.WriteLine("\nPodaj nowe nazwisko:");
                                in_nazwisko = Console.ReadLine()!;
                                Console.WriteLine("\nPodaj nowe email:");
                                in_email = Console.ReadLine()!;

                                if (SprawdzPoprawnoscEmail(in_email) == true)
                                {
                                    uzytkownik.edycjaDanych(in_imie, in_nazwisko, in_email, in_haslo, in_uprawnienia);

                                    zapisywanieUzytkownikowDoPliku("users.txt");

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Dane pomyślnie zmienione!\n");
                                    Thread.Sleep(500);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nBłąd - zły format emaila\n");
                                }
                                break;
                            case 4:
                                int usuwanieKonta;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------");
                                Console.WriteLine(" USUWANIE KONTA ");
                                Console.Write("\nJestes pewien?: Y/N ");
                                usuwanieKonta = Console.ReadLine()!.ToLower() == "y" ? 1 : 0;
                                if (usuwanieKonta == 1)
                                {
                                    listaUzytkownikow.Remove(uzytkownik);

                                    zapisywanieUzytkownikowDoPliku("users.txt");

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Wylogowywanie...\n");
                                    Thread.Sleep(2000);
                                    return;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Anulowanie...\n");
                                    Thread.Sleep(500);
                                    break;
                                }
                            default:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nBłąd! - Nie ma takiej opcji");
                                break;
                        }
                    }
                    else if (!listaUprawnien.Contains(uzytkownik.Uprawnienia))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine(" Błąd logowania! - brak uprawnień ");
                        Console.WriteLine("-----------------------------------");
                        Thread.Sleep(4000);
                        Console.Clear();
                        break;
                    }
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBłąd! - {error.Message} \n");
                }

            } while (inPanel);
        }
        public static void logowanie()
        {
            string email, haslo;
            Console.Clear();
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("===========");
                    Console.WriteLine(" LOGOWANIE ");
                    Console.WriteLine("===========");
                    Console.Write("\nJeśli chcesz wyjść wcisnij '0'");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nPodaj adres email: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    email = Console.ReadLine()!;

                    if (email == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Clear();
                        Console.WriteLine("-------------");
                        Console.WriteLine(" Opuszczanie ");
                        Console.WriteLine("--------------");
                        Thread.Sleep(1000);
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPodaj haslo: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    haslo = Console.ReadLine()!;


                    if (SprawdzPoprawnoscEmail(email) == false)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nBłąd! - Adres email jest niepoprawny. Spróbuj jeszcze raz.\n");
                        continue;
                    }


                    User znalezionyUzytkownik = listaUzytkownikow.Find(u => u.SprawdzEmail(email))!;
                    if (znalezionyUzytkownik == null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBłąd! - Nie znaleziono użytkownika o podanym adresie email.\n");
                    }
                    else if (!znalezionyUzytkownik.SprawdzHaslo(haslo))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBłąd! - Niepoprawne hasło.\n");
                    }
                    else
                    {
                        Panel(email);
                        return;
                    }

                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBłąd! - {error.Message} \n");
                }
            } while (true);
        }

        public static void rejestracja()
        {
            string imie, nazwisko, email, haslo, uprawnienia;
            Console.Clear();
            do
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("=============");
                    Console.WriteLine(" REJESTRACJA ");
                    Console.WriteLine("=============");
                    Console.Write("\nJesli chcesz wyjsc wcisnij '0'");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nPodaj swoje imie: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    imie = Console.ReadLine()!;

                    if (imie == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpuszczanie...\n");
                        Thread.Sleep(1000);
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nPodaj swoje nazwisko: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    nazwisko = Console.ReadLine()!;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nPodaj adres email: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    email = Console.ReadLine()!;

                    if (CzyIstniejEmail(email))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Clear();
                        Console.WriteLine("\nBłąd, istnieje juz konto z takim emailem!");
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n(Haslo powinno zawierac jeden duzy znak, oraz od 5 do 14 liter)");
                    Console.WriteLine("Podaj haslo: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    haslo = Console.ReadLine()!;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCzy jesteś administratorem? (podaj haslo administratora) / n: ");
                    uprawnienia = Console.ReadLine()!.ToLower() == "admin123" ? "admin" : "user";

                    if (SprawdzPoprawnoscEmail(email) == false)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nBłąd! - Adres email jest niepoprawny. Sprobuj jeszcze raz.\n");
                        continue;
                    }

                    if (SprawdzPoprawnoscHasla(haslo) == false)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nBłąd! - Haslo jest niepoprawne. Sprobuj jeszcze raz.\n");
                        continue;
                    }
                    User nowyUzytkownik = new User(imie, nazwisko, email, haslo, uprawnienia);
                    listaUzytkownikow.Add(nowyUzytkownik);

                    zapisywanieUzytkownikowDoPliku("users.txt");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine(" Rejestracja zakończona pomyślnie! ");
                    Console.WriteLine("-----------------------------------");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBłąd! - {error.Message} \n");
                }
            } while (true);

        }


        private static void zapisywanieUzytkownikowDoPliku(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var user in listaUzytkownikow)
                {
                    writer.WriteLine($"{user.Imie};{user.Nazwisko};{user.Email};{user.Haslo};{user.Uprawnienia}");
                }
            }
        }

        static void wyswietlUzytkownikow(string email)
        {
            User uzytkownik = listaUzytkownikow.Find(u => u.SprawdzEmail(email))!;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--------------------");
            Console.WriteLine(" LISTA UZYTKOWNIKOW ");
            Console.WriteLine("--------------------");
            int i = 1;
            listaUzytkownikow.ForEach(delegate (User u)
            {
                if (u.Email != uzytkownik.Email)
                {
                    Console.WriteLine($"{i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                }
                else
                {
                    Console.WriteLine($"* {i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                    Console.WriteLine($"Haslo: {u.Haslo}");
                }
                i++;
            });
            Console.WriteLine("-----------------------------------------------------------------------------------------------\n");

        }

        static void usuwanieUzytkownikow(string email)
        {
            User uzytkownik = listaUzytkownikow.Find(u => u.SprawdzEmail(email))!;
            Console.Clear();
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            listaUzytkownikow.ForEach(delegate (User u)
            {
                if (u.Email != uzytkownik.Email)
                {
                    Console.WriteLine($"{i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                }
                else
                {
                    Console.WriteLine($"* {i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                }
                i++;
            });
            i = 0;
            Console.WriteLine("-----------------------------------------------------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Podaj id uzytkownika do usuniecia: ");
            i = int.Parse(Console.ReadLine()!);
            if (listaUzytkownikow[i - 1].Email == uzytkownik.Email)
            {
                Console.Clear();
                Console.WriteLine("Nie mozesz usunac swojego konta!");
            }
            else
            {
                Console.Clear();
                listaUzytkownikow.Remove(listaUzytkownikow[i - 1]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Usunieto pomyslnie!");
                Thread.Sleep(500);
            }
        }

        static void dodawanieUzytkownika()
        {
            string in_imie, in_nazwisko, in_haslo, in_email, in_uprawnienia;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------------------------");
            Console.WriteLine(" Dodawanie uzytkownika: \n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Podaj imie:");
            in_imie = Console.ReadLine()!;
            Console.WriteLine("Podaj nazwisko:");
            in_nazwisko = Console.ReadLine()!;
            Console.WriteLine("Podaj email:");
            in_email = Console.ReadLine()!;
            Console.WriteLine("Podaj haslo:");
            in_haslo = Console.ReadLine()!;
            Console.WriteLine("Czy ma miec uprawnienia administratora? Y/N:");
            in_uprawnienia = Console.ReadLine()!.ToLower() == "y" ? "admin" : "user";

            if (SprawdzPoprawnoscEmail(in_email) == true)
            {
                if (SprawdzPoprawnoscHasla(in_haslo) == true)
                {
                    listaUzytkownikow.Add(new User(in_imie, in_nazwisko, in_email, in_haslo, in_uprawnienia));

                    zapisywanieUzytkownikowDoPliku("users.txt");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Uzytkownik pomyslnie dodany\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nBłąd - zły format hasla\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nBłąd - zły format emaila\n");
            }
        }

        static void edytowanieUzytkownika(string email)
        {
            User uzytkownik = listaUzytkownikow.Find(u => u.SprawdzEmail(email))!;
            string in_imie, in_nazwisko, in_haslo, in_email, in_uprawnienia;
            int i = 0;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---------------------");
            Console.WriteLine(" Edycja uzytkownika:\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            i = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            listaUzytkownikow.ForEach(delegate (User u)
            {
                if (u.Email != uzytkownik.Email)
                {
                    Console.WriteLine($"{i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                }
                else
                {
                    Console.WriteLine($"* {i}. Imie: {u.Imie}, Nazwisko: {u.Nazwisko} , Email: {u.Email}, Uprawnienia: {u.Uprawnienia}");
                }
                i++;
            });

            i = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----------------------------------------------------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Podaj id uzytkownika do edycji: ");
            i = int.Parse(Console.ReadLine()!);
            i = i - 1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDane uzytkownika: ");
            Console.WriteLine($"Imie: {listaUzytkownikow[i].Imie} \nNazwisko: {listaUzytkownikow[i].Nazwisko} \nHaslo: {listaUzytkownikow[i].Haslo}");
            Console.WriteLine($"Email: {listaUzytkownikow[i].Email} \nUprawnienia: {listaUzytkownikow[i].Uprawnienia}\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n-----------------");
            Console.WriteLine(" Podaj nowe dane \n\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Podaj nowe imie:");
            in_imie = Console.ReadLine()!;
            Console.WriteLine("Podaj nowe nazwisko:");
            in_nazwisko = Console.ReadLine()!;
            Console.WriteLine("Podaj nowe email:");
            in_email = Console.ReadLine()!;
            Console.WriteLine("Podaj nowe haslo:");
            in_haslo = Console.ReadLine()!;
            Console.WriteLine("Czy ma miec uprawnienia administratora? Y/N:");
            in_uprawnienia = Console.ReadLine()!.ToLower() == "y" ? "admin" : "user";


            if (SprawdzPoprawnoscEmail(in_email) == true)
            {
                if (SprawdzPoprawnoscHasla(in_haslo) == true)
                {
                    listaUzytkownikow[i].edycjaDanych(in_imie, in_nazwisko, in_email, in_haslo, in_uprawnienia);

                    zapisywanieUzytkownikowDoPliku("users.txt");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Uzytkownik pomyslnie dodany\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nBłąd - zły format hasla\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nBłąd - zły format emaila\n");
            }

        }
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
 
