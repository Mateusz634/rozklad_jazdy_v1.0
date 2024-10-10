# SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI

#### 1. Opis projektu
   Aplikacja umożliwia swobodne wyszukiwanie przejazdu autobusów, zakup biletów oraz zarządzanie użytkownikami na twoich liniach autobusowych.

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

 •Postępuj zgodnie z instrukcjami pojawiającymi się w konsoli, aby wykonać wybrane operacje.
 [1] Dodaj połączenie: Dodaj nowe połączenie, podając informacje w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu.
 [2] Zaaktualizuj połączenie: Zaktualizuj istniejące połączenie, podając jego indeks.
 [3] Usuń połączenie: Usuń połączenie na podstawie indeksu.
 [4] Filtruj: Filtruj połączenia według miasta, ceny lub czasu dojazdu.
 [5] Aktualny rozkład: Zobacz listę wszystkich połączeń.
 [6] Kup bilet: Wybierz połączenie i kup bilet, podając swoje imię i nazwisko.
 [0] Zakończ: Zapisz aktualny rozkład i zakończ program.
  
Działanie: 
pierwsze wyświetla się menu z opcjami:
<img title="ActivityWatch" src="/1.png" align="center">

**1. Zaloguj się**
2. Zarejestruj się
 https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax
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

```ruby
   test
```
