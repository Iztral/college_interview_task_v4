# college_interview_task_v4
Zadanie 1
1. Jest to klasa abstrakcyjna która zawiera kod do wysyłania zapytań HTTP z konkretnym obiektem jako ładunek oraz parsowania odpowiedzi do później sprecyzowanego typu.

2.  	
	- implementacja obsługi nieprzewidzianych wyjątków przy wysyłaniu zapytania
	- powielone użycie referencji do System.Collections.Generic
	- funkcja Handle() jest nieczytelna ze względu na długość oraz dużą ilość parametrów
	- nieużywane zmienne

3. Klasa tworzy generyczny handler dla zapytań html, dzięki takiemu podejściu bardzo łatwo stworzyć handler dla konkretnego typu zapytań i odpowiedzi dzięki dziedziczeniu.

4. Ze względu na generyczność przyjmowanej zawartości wiadomości http mogą zaistnieć problemy przy castowaniu konkretnego typu do HttpContent. 

Zadanie 2
1. Refactoring zrobiony, rozbito na pomniejsze metody, podczyszczono kod.
2. Zaimplementowano interfejs parsowania, zaimplementowano klasę bazową jako handlery zapytań o bitmapę lub dictionary.
3. Nie stworzono przykładowych unit testów.

Zadanie 3
Na podsawie klasy stworzono przykładową aplikację konsolową wysyłającą zapytania do WebApi. API jest pośrednikiem do bazy danych przetrzymującej zdjęcia. Aplikacja pozwala na odczytanie listy dostępnych obrazów, a nastepnie pobranie ich na dysk. Do działania wymaga uruchomionego ImageStoreAPI.
