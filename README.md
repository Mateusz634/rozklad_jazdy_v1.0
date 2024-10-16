# SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI

### 1. Opis projektu
  #### Aplikacja ma na celu ułatwienie zarządzania połączeniami autobusowymi. Umożliwia użytkownikom dodawanie, aktualizowanie, usuwanie oraz filtrowanie połączeń, a także zakup biletów na wybrane kursy

####  Aplikacja rozwiązuje problem braku centralnego systemu do zarządzania rozkładami jazdy autobusów. Dzięki niej użytkownicy mogą łatwo i szybko zaktualizować swoje połączenia oraz dokonać zakupu biletów, co zwiększa wygodę i efektywność podróżowania.

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
| `Aktualny_Rozklad()`    | • Wyświetla aktualny rozkład jazdy. <br> • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń.                                           |
4. Instrukcja instalacji i uruchomienia<br>
<br>Krok po kroku 
1.Pobierz kod źródłowy aplikacji.
<br>2.Otwórz Visual Studio.<br>
3.Załaduj projekt w Visual Studio.
<br>4.Skonfiguruj ścieżki do plików rozklad_jazdy.txt i bilety.txt.<br>
5.Uruchom aplikację, klikając "Start".
Wymagania systemowe
Wersja .NET: .NET 5.0 lub nowsza.
System operacyjny: Windows 10 lub nowszy.

 ### Postępuj zgodnie z instrukcjami pojawiającymi się w konsoli, aby wykonać wybrane operacje. <br>
 [1] Dodaj połączenie<br>
 [2] Zaaktualizuj połączenie<br>
 [3] Usuń połączenie<br>
 [4] Filtruj<br>
 [5] Aktualny rozkład<br>
 [6] Kup bilet<br>
 [0] Zakończ<br>
  
### Działanie: 
pierwsze wyświetla się menu z opcjami:<br>
<img title="ActivityWatch" src="/1.png" align="center">

**1. Zaloguj się**<br>
2. Zarejestruj się <br>
<a href="https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax">test</a>
Logowanie na podstawie adresu email i hasła 
Rejestracja: imię, nazwisko, adres email, hasło
 
Po zalogowaniu w zależności od uprawnień:

 *** Użytkownik:
 1. Zmiana danych użytkownika
 2. Zmiana hasła
 3. Usunięcie konta
 9. Wyloguj się
 
 *** Administrator:
 1. Zarządzanie użytkownikami
    a. Dodaj użytkownika
    b. Edytuj użytkownika
    c. Usuń użytkownika
 2. Przeglądanie listy uztykownikow
 9. Wyloguj się

```ruby
   test
```
