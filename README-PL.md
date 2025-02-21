# SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI

### 1. Opis projektu
  #### Nazwa aplikacji: System zarządzania autobusami oraz użytkownikami

#### Opis:
Aplikacja ma na celu ułatwienie zarządzania połączeniami autobusowymi oraz użytkownikami. Umożliwia użytkownikom dodawanie, aktualizowanie, usuwanie oraz filtrowanie połączeń, a także zakup biletów na wybrane kursy. Dodatkowo, system zarządza kontami użytkowników, oferując różne poziomy uprawnień.

#### Problem, który aplikacja rozwiązuje
Aplikacja rozwiązuje problem braku centralnego systemu do zarządzania rozkładami jazdy autobusów oraz użytkownikami. Dzięki niej użytkownicy mogą łatwo i szybko zaktualizować swoje połączenia, dokonać zakupu biletów oraz zarządzać swoimi danymi, co zwiększa wygodę i efektywność podróżowania.

#### Wartości dostarczane użytkownikowi
Użytkownicy zyskują wygodny dostęp do aktualnych rozkładów jazdy oraz możliwości zakupu biletów, a także łatwego zarządzania swoimi kontami. Administratorzy mają narzędzie do efektywnego zarządzania użytkownikami i połączeniami.


#### 2. Architektura aplikacji


| Moduł             | Opis                                                                 |
| :---:              |     :---      |
| Program            | Główny punkt wejścia do aplikacji, który zarządza menu.             |
| Lista Połączeń     | Przechowuje wszystkie połączenia w formie listy.                    |
| Plik z Rozkładem   | Zawiera dane o połączeniach w formacie tekstowym.                   |
| Menu Główne        | Umożliwia wybór opcji do interakcji z aplikacją.                    |
| DodajPolaczenie    | Dodaje nowe połączenie do listy.                                    |
| Aktualizacjia      | Aktualizuje istniejące połączenie.                                  |
| UsunPolaczenie     | Usuwa wybrane połączenie.                                           |
| Filtruj            | Oferuje opcje filtrowania połączeń.                                 |
| KupBilet           | Umożliwia zakup biletu na wybrane połączenie.                      |
| ZapiszRozklad      | Zapisuje aktualny stan rozkładu do pliku.                           |
| Moduł logowania / zarządzania    |Zarządza procesem logowania i rejestracji użytkowników. **(dogłębnie wytłumaczone w schemacie blokowym poniżej)**              |
<details>
  <summary>Pokaż schemat blokowy</summary>
  <img src="https://github.com/user-attachments/assets/b6f53602-aebd-4837-8c82-f9dd8132e1c5" alt="Schemat blokowy">
</details>


#### 3. Opis każdej funkcji:

| FUNKCJA                | Działanie funkcji:                                                                                                                                                      |
| :---:              |     :---      |
| `Main()`                | • To główna funkcja aplikacji, która uruchamia program i prezentuje menu wyboru opcji użytkownikowi. <br> • Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody. |
| `Aktualny_Rozklad()`    | • Wyświetla aktualny rozkład jazdy. <br> • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń.                                           |
| `DodajPolaczenie()`     | • Pozwala użytkownikowi na dodanie nowego połączenia w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu. <br> • Oblicza czas dojazdu, gdzie czas przyjazdu jest po północy. |
| `Aktualizacja()`        | • Umożliwia aktualizowanie istniejącego połączenia na podstawie jego indeksu. <br> • Pozwala zmienić godzinę odjazdu, przyjazdu oraz automatycznie aktualizuje czas przejazdu. |
| `UsunPolaczenie()`      | • Usuwa połączenie z listy na podstawie indeksu wybranego przez użytkownika. <br> • Po usunięciu wyświetla, że połączenie zostało usunięte.                        |
| `FiltrujPolaczenia()`   | • Główna funkcja filtrowania pozwala użytkownikowi wybrać sposób filtrowania: po mieście, cenie lub czasie dojazdu.                                                 |
| `FiltrujPoMiescie()`    | • Filtruje połączenie na podstawie podanego miasta.                                                                                                                  |
| `FiltrujPoCenie()`      | • Filtruje połączenie na podstawie maksymalnej ceny biletu.                                                                                                         |
| `KupBilet()`            | • Pozwala użytkownikowi zakupić bilet na wybrane połączenie, zapisując dane biletu do pliku bilety.txt.                                                             |
| `ZapiszRozklad()`       | • Zapisuje aktualny rozkład jazdy do pliku rozklad_jazdy.txt przed zakończeniem programu.                                                                           |
| `Logowanie()`           | • Umożliwia użytkownikowi zalogowanie się na swoje konto.                                                                                                           |
| `Rejestracja()`         | • Umożliwia nowym użytkownikom stworzenie konta.                                                                                                                    |
| `EdycjaUzytkownika()	` | • Umożliwia administratorowi edytowanie danych użytkownika.                                                                                                         |
| `UsunUzytkownika()	`   | • Umożliwia administratorowi usunięcie konta użytkownika.                                                                                                           |
                                                                   
                                                                        
### 4. Instrukcja instalacji i uruchomienia<br>
# 1. Pobieranie kodu źródłowego aplikacji

## Kroki do pobrania i uruchomienia aplikacji:
1. **Pobierz kod źródłowy aplikacji.**
   ```ruby
   git clone https://github.com/Mateusz634/rozklad_jazdy_v1.0.git

# 2. Uruchamianie aplikacji

## Kroki do uruchomienia aplikacji:
1. **Otwórz Visual Studio.**
2. **Załaduj projekt w Visual Studio.**
3. **Skonfiguruj ścieżki do plików** `rozklad_jazdy.txt` **i** `bilety.txt`.
4. **Uruchom aplikację, klikając** "Start".

## Wymagania systemowe
- **Wersja .NET**: .NET 5.0 lub nowsza.
- **System operacyjny**: Windows 10 lub nowszy.

 ### Postępuj zgodnie z instrukcjami pojawiającymi się w konsoli, aby wykonać wybrane operacje. <br>
 ![image](https://github.com/user-attachments/assets/c1893413-ba27-4916-8eaa-8d6a52876714)

  
### Działanie: 
pierwsze wyświetla się menu z opcjami:<br>
<img title="ActivityWatch" src="/1.png" align="center">

# 1. Logowanie i rejestracja

## Kroki do zalogowania się
1. **Zaloguj się**
2. **Zarejestruj się**

### Test
- **Logowanie**: na podstawie adresu email i hasła.
- **Rejestracja**: imię, nazwisko, adres email, hasło.

## Po zalogowaniu, w zależności od uprawnień:

### **Użytkownik:**
- Zmiana danych użytkownika
- Zmiana hasła
- Usunięcie konta
- Wyloguj się

### **Administrator:**
- Zarządzanie użytkownikami:
  a. Dodaj użytkownika  
  b. Edytuj użytkownika  
  c. Usuń użytkownika  
- Przeglądanie listy użytkowników
- Wyloguj się


# 5. Dokumentacja kodu

Komentarze w kodzie opisują kluczowe funkcje i logikę, co ułatwia zrozumienie działania programu. 

## Opis najważniejszych fragmentów kodu
- **Main**: Główna pętla aplikacji z obsługą opcji użytkownika.
- **DodajPolaczenie**: Funkcja odpowiedzialna za dodanie nowego połączenia z walidacją czasu.
- **Aktualizacjia**: Funkcja aktualizująca istniejące połączenie, uwzględniająca logikę czasu dojazdu.

# 6. Przykłady użycia

## Scenariusze testowe

### Dodanie nowego połączenia:
- **Wejście**: Warszawa - Kraków, 08:00, 10:00, 50 PLN
- **Oczekiwany wynik**: Połączenie dodane.

### Aktualizacja połączenia:
- **Wejście**: Indeks 0, nowe godziny: 08:30, 10:30
- **Oczekiwany wynik**: Połączenie zaktualizowane.

### Filtracja po cenie:
- **Wejście**: do 60 PLN
- **Oczekiwany wynik**: Wyświetlenie połączeń do 60 PLN.

## Przykłady interakcji użytkownika
Użytkownik wybiera opcję w menu, wprowadza dane i otrzymuje odpowiednie komunikaty zwrotne.

# 7. Błędy i ich obsługa

## Opis obsługi błędów
Aplikacja obsługuje błędy związane z niepoprawnym wprowadzeniem danych (np. zły format godziny, nieprawidłowy indeks połączenia) poprzez komunikaty informujące użytkownika o problemie.

## Lista obsługiwanych wyjątków
- **FormatException**: Gdy wprowadzone dane nie są w odpowiednim formacie (np. nieprawidłowe godziny).
- **IndexOutOfRangeException**: Gdy użytkownik podaje nieprawidłowy indeks połączenia.


### 8. Wnioski i przyszłe usprawnienia

| Wnioski i przyszłe usprawnienia                                                              |
|     :---      |
| Dodanie modułu logowania i rejestracji użytkowników.             | 
| Możliwość edytowania danych pasażera przy zakupie biletu.                   |
| Aplikacja działa zgodnie z założeniami i umożliwia efektywne zarządzanie połączeniami.                  |
|  Napotkane trudności obejmowały problemy z walidacją danych wejściowych.                     |
 | W przyszłości warto rozważyć rozszerzenie funkcji o dodatkowe opcje związane z użytkownikami.                               |
| Wprowadzenie dodatkowych opcji bezpieczeństwa, takich jak uwierzytelnianie dwuetapowe.|
|Rozszerzenie funkcji o dodatkowe opcje związane z użytkownikami.|
|Udoskonalenie interfejsu użytkownika.|

