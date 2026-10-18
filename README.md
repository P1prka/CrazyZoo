# CrazyZoo

CrazyZoo on humoorikas WPF-rakendus, mis on inspireeritud Madagaskari multikast. Kasutaja saab luua erinevaid loomi, valida neile nimed ja käivitada nende tegevusi.

## Käivitamine

1. Ava CrazyZoo lahendus Visual Studios.
2. Määra CrazyZoo.WpfApp käivitusprojektiks.
3. Käivita rakendus.

## Projekti struktuur

- **CrazyZoo.Core** — loomade klassid, tegevused ja energia kontroll.
- **CrazyZoo.WpfApp** — kasutajaliides, sisendite kontroll ja tulemuste kuvamine.

WPF-projekt kasutab Core-projekti.

## Klassid ja pärilus

Abstraktne baasklass on `Animal`.

Sellest pärivad järgmised klassid:

- Lion
- Zebra
- Giraffe
- Hippo
- Penguin
- Monkey

Kõigil loomadel on nimi (`Name`) ja energia (`Energy`).

## Liidesed

Kasutusel on kaks liidest:

- `ISwimmable` — ujumise võimalus.
- `IPerformer` — esinemise võimalus.

Hippo kasutab mõlemat liidest.

## Tegevused ja CrazyAction

Igal loomal on oma `NormalAction()` ja `CrazyAction()`.

- **Lion** — korraldab suure etenduse.
- **Zebra** — plaanib suurt seiklust.
- **Giraffe** — avastab endal väljamõeldud haiguse.
- **Hippo** — hüppab basseini ja pritsib kõiki.
- **Penguin** — alustab salajast operatsiooni.
- **Monkey** — viskab külastajate poole banaanikoore.

Tegevused muudavad looma energiat. Polümorfismi abil käivitatakse iga looma jaoks õige meetod.

## Energia reeglid

- Energy väärtus on vahemikus 0–100.
- Tegevused kasutavad energiat.
- Kui energiat ei piisa, tegevust ei sooritata.
- Energia ei saa muutuda negatiivseks.
- Vigane sisend ei muuda objekti olekut.

## WPF kasutajaliides

Rakenduses kasutatakse `ObservableCollection<Animal>` kollektsiooni.

Kasutaja saab loomi lisada, valida ja eemaldada ning nende tegevusi käivitada.

Kasutasin `ProgressBar` elementi, et kuvada valitud looma energiat visuaalselt. See aitab kiiresti näha, kui palju energiat loomal alles on.

## Kontrollnäited

1. Lion, nimi Leo, Energy 70 — NormalAction vähendab energia väärtusele 65.
2. Lion, Energy 65 — CrazyAction vähendab energia väärtusele 50.
3. Penguin, Energy 80 — Swim vähendab energia väärtusele 70.
4. Hippo — nii Swim kui ka Perform on kasutatavad.
5. Monkey, Energy 5 — CrazyAction ei käivitu, sest energiat ei piisa. Energy jääb 5.
6. Vigane Energy väärtus, näiteks abc või 120 — kuvatakse veateade ja looma ei lisata.

## Git-koostöö

Kaasüliõpilane: Sofia Gubatsjova

GitHub Issue:
https://github.com/P1prka/CrazyZoo/issues/1

Pull Request:
https://github.com/P1prka/CrazyZoo/pull/2

Kaasüliõpilane lisas Monkey klassi eraldi harus. Muudatused vaadati üle ja ühendati main-harusse Pull Requesti kaudu.


## Kodutöö 4: Crazy Universe (Vol.2)

Jätkasin Kodutöö 3 CrazyZoo projekti samas Git-hoidlas. Olemasolevad loomad ja nende tegevused on säilitatud.

### Generics ja hoidlad

Lisasin `IRepository<T>` ja `Repository<T>`, mis võimaldavad loomi lisada, eemaldada, otsida ja kuvada.

Igal loomaliigil on eraldi hoidla. `where T : Animal` tagab, et hoidlasse saab lisada ainult sobivat tüüpi loomi.

### Sündmused

Rakenduses on kolm peamist sündmust:

- **Lisamine:** olemasolevad sama hoidla loomad tervitavad uut looma.
- **Eemaldamine:** allesjäänud loomad jätavad eemaldatud loomaga hüvasti.
- **CrazyAction:** sama hoidla teised loomad reageerivad tegevusele ning reaktsioon kulutab 2 energiaühikut.

Eemaldatud loom ei reageeri enam sündmustele. Teiste hoidlate loomad ei reageeri.

### LINQ

Kasutasin LINQ-päringuid loomade otsimiseks nime järgi (`Where`), sorteerimiseks energia järgi (`OrderBy`, `OrderByDescending`) ja grupeerimiseks energiataseme järgi (`GroupBy`).

Kasutajaliideses kuvatakse ka loomade arv ja keskmine energia.

### MVVM

Rakendus kasutab MVVM-mustrit.

- **Model/Core:** loomad, hoidlad, sündmused ja tegevuste loogika.
- **ViewModel:** `MainViewModel`, andmed, logi ja `RelayCommand` käsud.
- **View:** `MainWindow.xaml`, mis kasutab Binding'uid.

Loendite automaatseks uuendamiseks kasutasin `ObservableCollection<T>` ning omaduste muudatustest teavitamiseks `INotifyPropertyChanged`.

### Koostöö

Kaasüliõpilane Sofia Gubatsjova lisas kolm uut loomaliiki: `Fox`, `Panda` ja `Elephant`.

Uued loomad kasutavad olemasolevaid hoidlaid, sündmusi ja MVVM-lahendust. Muudatused vaadati üle, kiideti heaks (Approve) ja ühendati (Merge) harusse `feature/vol2`.

GitHub Issue: https://github.com/P1prka/CrazyZoo/issues/3

Pull Request: https://github.com/P1prka/CrazyZoo/pull/4

### Git-ajalugu

Kodutöö 3 lõppversioon on säilitatud harus `main`.

Kodutöö 4 arendusharu: `feature/vol2`.

Vol.1 lähte-commit: `686548c275ca7ec8f446e12a175b56126fb0bbaf`.


### AI kasutamine

Kasutasin ChatGPT-d GitHubi kasutamise õppimiseks, sealhulgas branch'ide, commit'ide, pull request'ide ja code review mõistmiseks.
