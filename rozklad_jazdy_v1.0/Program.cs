using Rozklad;
using UserManagement;

namespace MainApp
{
    internal class MainProgram
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Witaj w programie zarządzania rozkładem jazdy!");
                Console.Write("Podaj swoje imię (np. Admin lub User): ");
                string imie = Console.ReadLine();

                // Logowanie użytkownika
                var zalogowanyUzytkownik = Program.Zaloguj(imie);

                // Wczytanie rozkładu jazdy, jeśli istnieje
                if (File.Exists(Program.sciezkaPliku))
                {
                    Program.rozkladJazdy.AddRange(File.ReadAllLines(Program.sciezkaPliku));
                }

                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("=== MENU ===");
                    Console.WriteLine("[1] Wyświetl rozkład");
                    if (zalogowanyUzytkownik.Uprawnienia == "admin")
                    {
                        Console.WriteLine("[2] Dodaj połączenie");
                        Console.WriteLine("[3] Zapisz rozkład");
                    }
                    Console.WriteLine("[0] Wyjdź");

                    Console.Write("Wybierz opcję: ");
                    int wybor = int.Parse(Console.ReadLine());

                    switch (wybor)
                    {
                        case 1:
                            // Wyświetlanie rozkładu jazdy
                            Program.Aktualny_Rozklad();
                            break;
                        case 2:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                Console.Write("Podaj nowe połączenie w formacie: MiastoA - MiastoB, Godz. Odjazdu, Godz. Przyjazdu, Cena: ");
                                string nowePolaczenie = Console.ReadLine();
                                Program.DodajPolaczenie(nowePolaczenie);
                            }
                            else
                            {
                                Console.WriteLine("Nie masz uprawnień do tej funkcji.");
                            }
                            break;
                        case 3:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                Program.ZapiszRozklad();
                            }
                            else
                            {
                                Console.WriteLine("Nie masz uprawnień do tej funkcji.");
                            }
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowa opcja.");
                            break;
                    }

                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }
}
