# JAK UŻYWAĆ GIT
# OPCJA A (cmd)
## 1. Wprowadzenie
* Zainstaluj [git lfs](https://git-lfs.com/).
* Ściągnij repozytorium używając `git clone https://github.com/LudicLab7/vigilant-octo-enigma.git`
* Wejdź w utworzony folder w terminalu <small>(cmd/Powershell/git cmd/git bash)</small>
* Zainicjuj w nim git lfs przy użyciu `git lfs install` - konfiguracja jest już zrobiona (plik .gitattributes) 
* W unity hub kliknij "Dodaj projekt z dysku" lub "Add project from disk" -> wybierz folder `vigilant-octo-enigma`
![Dodaj -> dodaj projekt z dysku](img/unity_import.png)
* Gotowe!

## 2. Jak używać git
* Przed rozpoczęciem pracy zawsze przełącz się na branch dev (`git checkout dev`) i zrób `git pull origin dev` (na dev, ważne że w tej kolejności) - zaoszczędzi ci to trochę pracy z merge'owaniem pull requestów
* Jeśli chcesz rozpocząć pracę nad nową funkcjonalnością, stwórz nowego brancha (`git checkout -b [nazwa-brancha]`), ta komenda przełącza i tworzy brancha, jeśli chcesz się przełączyć użyj `git checkout [nazwa-brancha]`, nazwa powinna być w lowercase, bez znaków specjalnych i używając myślników zamiast spacji: np. `Aktualizacja dokumentacji` -> `aktualizacja-dokumentacji`
* Wykonaj zmiany w kodzie - pamiętaj żeby wykonywać zmiany odpowiednie z przeznaczeniem brancha - od osobnych funkcji twórz osobne branche, zachowując odpowiednie nazewnictwo. Rób częste commity (`git add .`, `git commit`), możesz używać ich jako swojego rodzaju schowek, więcej informacji możesz znaleźć [tutaj](https://www.virtualmaker.dev/blog/git-and-unity-a-comprehensive-guide-to-version-control-for-game-devs). Pamiętaj o poprawnym nazewnictwie
* Jeśli skończyłeś twoje zadanie, zpushuj brancha na githuba za pomocą `git push -u origin [nazwa-brancha]` (nie dev! sprawdź czy na pewno byłeś na nowym branchu! visual studio znacznie to ułatwia przez gui). Pamiętaj o przetestowaniu twojego kodu, nawet jeśli jest bardzo prosty.
* Na githubie otwórz nowy pull request: ![Nowy pr](img/pr_create.png)
Wybierz branch do którego chcesz merge'ować (1), branch z którego chcesz merge'ować (2), Wybierz dobry tytuł oraz opis dla pull requesta - zwięzły tytuł i szczegółowy ale też zwięzły opis, w którym opisujesz co i jak zrobiłeś (nie jakoś super szczegółowo, bardziej na zasadzie "zrobiłem to i to używając tego i tego", tyle wystarczy. Jeśli bardzo nie chce ci się pisać, poproś chatbota) ![Nowy pr - wypełnienie formularza](img/pr_form.png) Po stworzeniu pull requesta poczekaj na review, jeśli wystąpią problemy, napraw je. Po zaakceptowaniu pr'a zmerge'uj go do dev, jeśli akceptujący jeszcze tego nie zrobił.

# OPCJA B (github desktop)
## 1. Wprowadzenie
* Zainstaluj [Github desktop](https://desktop.github.com/download/).
* Zainstaluj [Git LFS](https://git-lfs.com/).
* Skonfiguruj github desktop![](img/1.png)
* Pobierz repozytorium **LudicLab7/vigilant-octo-enigma** (prawdopodobnie ulegnie zmianie)![](img/2.png)
* Inicjalizuj Git LFS![](img/3.png)
* Przełączasz się na dev![](img/4.png)
* Import projektu do unity ![](img/5.png)![](img/6.png) ![](img/7.png)

## 2.Rozpoczęcie Pracy
* Robisz Pull na dev!!!
* Tworzysz nowy branch![](img/8.png)
* Przesyłasz branch do repozytorium![](img/9.png)
* Rozpoczynasz prace na projekcie (**WAŻNE aby maksymalnie jedna osoba pracowała nad daną sceną w unity projekt, bo spowoduje to konflikty**)![](img/10.png)
* W trakcie pracy możesz zauważyć, że będą pojawiać ci się pliki![](img/11.png)
* Commituj jak najczęściej ![](img/Zrzut%20ekranu%202026-02-27%20131717.png)
* Po zakończonej pracy wysyłasz wszystko do repozytorium ![](img/Zrzut%20ekranu%202026-02-27%20131732.png)
* Oraz tworzysz pull request aby ktoś przejrzał twój kod i dodał go do repozytorium![](img/Zrzut%20ekranu%202026-02-27%20131745.png) ![Zrzut ekranu 2026-02-27 131751](img/Zrzut%20ekranu%202026-02-27%20131751.png)
* Zawsze wybierasz base: dev ![Zrzut ekranu 2026-02-27 131759](img/Zrzut%20ekranu%202026-02-27%20131759.png)
* Gdy klikniesz create pull request przeniesie cię na stronę githuba tam uzupełniasz wszystkie detale i wysyłasz do weryfikacji ![Zrzut ekranu 2026-02-27 131934](img/Zrzut%20ekranu%202026-02-27%20131934.png)
* To wszystko klikasz create pull request i kwestionujesz swoje wybory życiowe
