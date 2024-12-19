using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;
using static Program;

internal class Program
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

    private static List<User> listaUzytkownikow = new List<User>();

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

        if(!File.Exists("users.txt"))
        {
            File.Create("users.txt").Dispose();

        }

        foreach (var line in File.ReadLines("users.txt"))
        {
            string[] parts = line.Split(';');

            if (parts.Length <= 5)
            {
                listaUzytkownikow.Add(new User(parts[0], parts[1], parts[2], parts[3], parts[4]));
            }
        }

        /* 
        foreach (var user in listaUzytkownikow)
        {
            Console.WriteLine($"{user.Imie} - {user.Nazwisko} - {user.Email} - {user.Uprawnienia}");
        }
        */

        // ==============
        // PROJEKT
        // KRYSTIAN KOZA 
        // 3TP
        // ==============

        // TESTOWI UZYTKOWNICY

        // ADMIN - administrator 
        // mail: admin@gmail.com, haslo: admin

        // USER - normalny uzytkownik
        // mail: user@gmail.com, haslo: user

        // TESTOWY UZYTKOWNIK - nie powinno go zalogowac bo nie nalezy do grup admin/user
        // mail: test@gmail.com, haslo: test

        //Console.Clear();
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
                    Console.WriteLine("1. Wyswietl liste uzytkownikow");
                    Console.WriteLine("2. Zmiana hasla");
                    Console.WriteLine("3. Usuwanie uzytkownika");
                    Console.WriteLine("4. Dodaj uzytkownika");
                    Console.WriteLine("5. Edytuj uzytkownika");
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

                            break;
                        case 2:
                            Console.Clear();
                            uzytkownik.zmianaHasla();

                            zapisywanieUzytkownikowDoPliku("users.txt");

                            break;
                        case 3:
                            Console.Clear();
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
                            Console.WriteLine("-----------------------------------------------------------------------------------------------\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("Podaj id uzytkownika do usuniecia: ");
                            i = int.Parse(Console.ReadLine()!);
                            if (listaUzytkownikow[i - 1].Email == uzytkownik.Email)
                            {
                                Console.WriteLine("Nie mozesz usunac swojego konta!");
                            }
                            else
                            {
                                listaUzytkownikow.Remove(listaUzytkownikow[i - 1]);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Usunieto pomyslnie!");
                                Thread.Sleep(500);
                            }

                            break;
                        case 4:
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
                            break;
                        case 5:
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

                            break;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nBłąd! - Nie ma takiej opcji");
                            break;
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

                if(CzyIstniejEmail(email)) {
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
        using(StreamWriter writer = new StreamWriter(filePath))
        {
            foreach(var user in listaUzytkownikow)
            {
                writer.WriteLine($"{user.Imie};{user.Nazwisko};{user.Email};{user.Haslo};{user.Uprawnienia}");
            }
        }
    }
}
