<h1>SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI</h1>

<ol>
 <li>Opis projektu
 <p>Aplikacja umożliwia swobodne wyszukiwanie przejazdu autobusów, zakub biletów oraz zarządzanie użytkownikami na twoich liniach autobusowych.</p>
 </li>

 <li>Architektura aplikacji</li>
 <table border="1">
    <thead>
        <tr>
            <th>Moduł</th>
            <th>Opis</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Program</td>
            <td>Główny punkt wejścia do aplikacji, który zarządza menu.</td>
        </tr>
        <tr>
            <td>Lista Połączeń</td>
            <td>Przechowuje wszystkie połączenia w formie listy.</td>
        </tr>
        <tr>
            <td>Plik z Rozkładem</td>
            <td>Zawiera dane o połączeniach w formacie tekstowym.</td>
        </tr>
        <tr>
            <td>Menu Główne</td>
            <td>Umożliwia wybór opcji do interakcji z aplikacją.</td>
        </tr>
        <tr>
            <td>DodajPolaczenie</td>
            <td>Dodaje nowe połączenie do listy.</td>
        </tr>
        <tr>
            <td>Aktualizacjia</td>
            <td>Aktualizuje istniejące połączenie.</td>
        </tr>
        <tr>
            <td>UsunPolaczenie</td>
            <td>Usuwa wybrane połączenie.</td>
        </tr>
        <tr>
            <td>Filtruj</td>
            <td>Oferuje opcje filtrowania połączeń.</td>
        </tr>
        <tr>
            <td>KupBilet</td>
            <td>Umożliwia zakup biletu na wybrane połączenie.</td>
        </tr>
        <tr>
            <td>ZapiszRozklad</td>
            <td>Zapisuje aktualny stan rozkładu do pliku.</td>
        </tr>
    </tbody>
</table>

</ol>

3. Opis każdej funkcji:
<table>
 <tr>
   <th>
    FUNKCJA
   </th>
  <td>Dzialanie funkcji:</td>
 </tr>
 <tr>
  <td> 
   Main()
  </td>
  <td>
   • To główna funkcja aplikacji która uruchamia program i prezentuje menu wyboru opcji użytkownikownikowi <br>
   • Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody
 </td>
 </tr>
 <tr>
  <td>
   Aktualny_Rozklad()
  </td>
  <td>
   • Wyświetla aktualny rozkład jazdy <br>
   • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń
  </td>
</tr> 
<tr>
 <td>
   DodajPolaczenie()
 </td>
 <td>
   • Pozwala użytkownikowi na dodanie nowego połaczenia w formacie: MiastoA - MiastoB, Godzina Odjazdu, Godzina Przyjazdu, Cena biletu. <br>
   • Oblicza czas dojazdu, gdzie czas przyjazdu jest po północy
</td>
</tr>
<tr>
<td>
   Aktualizacja()
 </td>
 <td>
   • Umożliwia aktualizowanie istniejącego połaczenia na podstawie jego indeksu. <br>
   • Pozwala zmienić godzinę odjazdu, przyjazdu oraz automatycznie aktualizuje czas przejazdu.
</td>
</tr>
<tr>
 <td>
   UsunPolaczenie()
  </td>
 <td>
   • Usuwa połączenie z listy na pdstawie indeksu wybranego przez użytkownika <br>
   • Po usunięciu wyświetla że połączenie zostało usunięte
</td>
</tr>
<tr>
 <td>
   FiltrujPolaczenia()
  </td>
 <td>
   • Główna funkcja filtrowania pozwala użytkownikowi wybrać sposób filtrowania: po mieście, cenie lub czasie dojazdu.
</td>
</tr>
<tr>
 <td>
   FiltrujPoMiescie()
 </td>
 <td>
   • Filtruje połączenie na podstawie podanego miasta
</td>
</tr>
<tr>
 <td>
   FiltrujPoCenie()
  </td>
 <td>
   • Filtruje połączenie na podstawie maksymalnej ceny biletu
</td>
</tr>
<tr>
 <td>
   KupBilet()
  </td>
 <td>
   • Pozwala użytkownikowi zakupić bilet na wybrane połaczenie, zapisując dane biletu do pliku bilety.txt
</td>
</tr>
<tr>
 <td>
   ZapiszRozklad()
 </td>
 <td>
   • Zapisuje aktualny rozkład jazdy do pliku rozklad_jazdy.txt przed zakończeniem programu.
</td>
</tr>
<tr>
 <td>
   Aktualny_Rozklad()
  </td>
 <td>
  • Wyświetla aktualny rozkład jazdy. <br>
  • Jeśli lista rozkladJazdy jest pusta, informuje użytkownika o braku połączeń.
  </td>
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

