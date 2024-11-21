using Rozklad;
using Manager; 
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

                User zalogowanyUzytkownik = Manager.listaUzytkownikow.Find(u => u.Imie == imie)
                                            ?? throw new Exception("Nie znaleziono użytkownika.");

                if (File.Exists(Rozklad.Rozklad.sciezkaPliku))
                {
                    Rozklad.Rozklad.rozkladJazdy.AddRange(File.ReadAllLines(Rozklad.Rozklad.sciezkaPliku));
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
                    if (!int.TryParse(Console.ReadLine(), out int wybor))
                    {
                        Console.WriteLine("Błędny wybór. Spróbuj ponownie.");
                        continue;
                    }

                    switch (wybor)
                    {
                        case 1:
                            Rozklad.Rozklad.Aktualny_Rozklad();
                            break;
                        case 2:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                Console.Write("Podaj nowe połączenie: ");
                                Rozklad.Rozklad.DodajPolaczenie();
                            }
                            else
                            {
                                Console.WriteLine("Nie masz uprawnień do tej funkcji.");
                            }
                            break;
                        case 3:
                            if (zalogowanyUzytkownik.Uprawnienia == "admin")
                            {
                                Rozklad.Rozklad.ZapiszRozklad();
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
