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
                Console.Write("Podaj swoje imię (Admin/User): ");
                string imie = Console.ReadLine();

                User zalogowanyUzytkownik = ZarzadzanieUzytkownikami.Zaloguj(imie);

                if (File.Exists(RozkladJazdy.sciezkaPliku))
                {
                    RozkladJazdy.rozkladJazdy.AddRange(File.ReadAllLines(RozkladJazdy.sciezkaPliku));
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
                            RozkladJazdy.Aktualny_Rozklad();
                            break;
                        case 2:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                Console.Write("Podaj nowe połączenie: ");
                                string nowePolaczenie = Console.ReadLine();
                                RozkladJazdy.DodajPolaczenie(nowePolaczenie);
                            }
                            else
                            {
                                Console.WriteLine("Nie masz uprawnień do tej funkcji.");
                            }
                            break;
                        case 3:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                RozkladJazdy.ZapiszRozklad();
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
