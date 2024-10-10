##################################################
 SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI
##################################################

1. Opis projektu
   SYSTEM ZARZĄDZANIA AUTOBUSAMI ORAZ UŻYTKOWNIKAMI

   Aplikacja umożliwia swobodne wyszukiwanie przejazdu autobusów, zakub biletów oraz zarządzanie użytkownikami na twoich liniach autobusowych.

2. Architektura aplikacji

3. Opis każdej funkcji:
   
   Main()
   • To główna funkcja aplikacji która uruchamia program i prezentuje menu wyboru opcji użytkownikownikowi
   • Na podstawie wyboru użytkownika, funkcja wywołuje odpowiednie metody
 
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
    
   
   
   
   
   
   


Działanie: 
pierwsze wyświetla się menu z opcjami: 
1. Zaloguj się
2. Zarejestruj się
 
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

