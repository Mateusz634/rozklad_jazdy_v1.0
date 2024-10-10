<h1>SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI</h1>

<ol>
 <li>Opis projektu
 <p>Aplikacja umożliwia swobodne wyszukiwanie przejazdu autobusów, zakub biletów oraz zarządzanie użytkownikami na twoich liniach autobusowych.</p>
 </li>

 <li>Architektura aplikacji</li>
</ol>

3. Opis każdej funkcji:
<table>
 <tr>
  <th>
   FUNKCJA
  </th>
  <td>
   <td>Dzialanie funkcji:</td>
  </td>
 </tr>
 <tr>
  <td> 
   Main()
  </td>
  <td>
   • To główna funkcja aplikacji która uruchamia program i prezentuje menu wyboru opcji użytkownikownikowi
   • Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody
 </td>
 </tr>
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
    
   Aktualny_Rozklad()
  • Wyświetla aktualny rozkład jazdy.
  • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń.
</table>
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

