# Book Manager – Fullstack Probetest

**Datum:** 3. Dezember 2025

---

## Kontext / Fachliche Aufgabe

Erstelle eine kleine Anwendung **„Book Manager"**, mit der Bücher verwaltet werden können.

Jedes Book hat mindestens folgende Eigenschaften:
- `id` (number)
- `title` (string)
- `author` (string)
- `publishedDate` (date/string im Format YYYY-MM-DD)
- `price` (number)
- `isAvailable` (boolean)

---

## Backend (.NET Minimal API)

### Anforderungen

- **.NET 8 Minimal-API-Projekt** ohne Datenbank
- **Statische In-Memory-Liste** von `Book` in einem Service (Repository-ähnliche Klasse)
- CORS aktivieren für Kommunikation mit Angular Frontend

### Zu implementierende Endpunkte

| Methode | Route                    | Funktion               |
|---------|--------------------------|------------------------|
| GET | `/api/books`             | Alle Bücher abrufen    |
| GET | `/api/books/titleExists` | Ob der Title bereits existiert  |
| GET | `/api/books/{id}`        | Einzelnes Buch abrufen |
| POST | `/api/books`             | Neues Buch anlegen     |
| PUT | `/api/books/{id}`        | Buch aktualisieren     |
| DELETE | `/api/books/{id}`        | Buch löschen           |

### Implementierungshinweise

- Erstelle ein `Book` Record/Class mit den genannten Properties (inkl. `publishedDate`)
- Implementiere einen `BookService` oder `BookRepository`, der eine statische `List<Book>` verwaltet
- In `Program.cs` alle Routen mit Map-Methoden registrieren
- Jeder Endpunkt soll IResult oder das Objekt direkt zurückgeben
- Error-Handling: 404 bei nicht vorhandenem Buch, 400 bei ungültigen Daten
- Testdaten mit verschiedenen Veröffentlichungsdaten vorbefüllen (z.B. 2020, 2023, 2025)

---

## Frontend (Angular)

### Projektstruktur

```
src/app/
├── models/
│   └── book.model.ts
├── services/
│   └── book.service.ts
├── components/
│   ├── book-list/
│   │   └── book-detail/
│   ├── book-form/
│   └── nav-menu/
│   └── home/
├── app.routes.ts
├── app.config.ts
└── app.ts
```

### 1. Book Model

Erstelle `src/app/models/book.model.ts`:

- Interface `Book` mit id, title, author, publishedDate (string oder Date), price, isAvailable
- Optionales Interface `CreateBookDto` für POST (ohne id)

**Beispiel:**
```typescript
export interface Book {
  id: number;
  title: string;
  author: string;
  publishedDate: string; // Format: YYYY-MM-DD
  price: number;
  isAvailable: boolean;
}

export type CreateBookDto = Omit<Book, 'id'>;
```

### 2. BookService

Erstelle `src/app/services/book.service.ts`:

- Nutze `HttpClient` und `provideHttpClient()` in `app.config.ts`
- Implementiere Methoden: `getAll()`, `getById(id)`, `create(book)`, `update(book)`, `delete(id)`
- Alle Methoden geben `Observable<...>` zurück
- Nutze `environment.apiUrl` aus der environment-Datei

### 3. Environment-Konfiguration

Erstelle oder aktualisiere `src/environments/environment.ts`:

```typescript
export const environment = {
  apiUrl: 'http://localhost:5000/api'
};
```

### 4. Routing

Konfiguriere `src/app/app.routes.ts` mit folgenden Routen:

| Route               | Komponente             | Funktion              |
|---------------------|------------------------|-----------------------|
| `/books`            | BooksOverviewComponent | Übersicht mit Tabelle und Detail-Subkomponente |
| `/books/add`        | BookFormComponent      | Neues Buch anlegen    |
| `/books/:id/edit`   | BookFormComponent      | Buch bearbeiten       |

---

## Komponenten-Anforderungen

### BooksOverviewComponent

**Datenbindung:**
- Signal `isLoading = signal<boolean>(false)` für Ladestatus
- Signal `books = signal<Book[]>([])` für die Bücherliste
- Signal `selectedBookId = signal<number | null>(null)` für ausgewähltes Buch
- Fehlerbehandlung mit Signal oder Property

**Template:**
- HTML-Tabelle mit `@for (book of books(); track book.id)` Loop
- Spalten: ID, Title, Author, Published Date (formatiert, z.B. DD.MM.YYYY), Price, isAvailable, Actions
- Action-Buttons in jeder Zeile:
  - **Details** → `(click)="selectBook(book.id)"` (zeigt Detail-Subkomponente)
  - **Bearbeiten** → `routerLink="/books/{{book.id}}/edit"`
  - **Löschen** → `(click)="deleteBook(book.id)"` (mit Bestätigung empfohlen)
- Button zum Hinzufügen: `routerLink="/books/add"`
- BookDetailComponent wird als Subkomponente angezeigt, wenn ein Buch ausgewählt ist:
  - `@if (selectedBookId()) { <app-book-detail [bookId]="selectedBookId()!" (close)="closeDetail()" /> }`

**Funktionalität:**
- `ngOnInit()`: Alle Bücher laden via `bookService.getAll()`
- `selectBook(id)`: Setzt `selectedBookId` Signal
- `closeDetail()`: Setzt `selectedBookId` auf null
- `deleteBook(id)`: Buch löschen, dann Liste neu laden
- Published Date in einer lesbaren Form anzeigen (z.B. mit Angular `date` Pipe)

---

### BookDetailComponent (Subkomponente)

**Input/Output:**
- `@Input({ required: true }) bookId!: number` - ID des anzuzeigenden Buchs
- `@Output() close = new EventEmitter<void>()` - Event zum Schließen der Detailansicht

**Datenbindung:**
- `book = signal<Book | null>(null)`
- `isLoading = signal<boolean>(false)`

**Lifecycle:**
- `ngOnInit()` oder `ngOnChanges()`: Wenn `bookId` sich ändert, `bookService.getById(bookId)` aufrufen
- Result in Signal speichern

**Template:**
- Buchdaten übersichtlich anzeigen (Title, Author, Published Date, Price, isAvailable)
- Published Date formatieren (z.B. mit `date` Pipe: `{{ book().publishedDate | date:'dd.MM.yyyy' }}`)
- Close-Button: `(click)="close.emit()"` (schließt Detail-Ansicht)
- Edit-Button: `routerLink="/books/{{bookId}}/edit"`
- Kann als Modal, Sidebar oder Inline-Panel gestaltet werden

---

### BookFormComponent

**Verwendung:**
- Wird sowohl für Add (`/books/add`) als auch für Edit (`/books/:id/edit`) genutzt
- Beim Edit: Published Date aus bestehenden Daten laden und anzeigen

**Reactive Forms (empfohlen):**

- `FormBuilder` injizieren
- FormGroup mit Controls: title, author, publishedDate (Datepicker oder Textfeld mit Format YYYY-MM-DD), price, isAvailable
- Validierung:
  - title required + unique (asynchron prüfen via `bookService`)
  - author required
  - publishedDate required und gültiges Datumsformat
  - price required + min(0.01)
  - publishedDate darf nicht in der Zukunft liegen
- Submit ruft `bookService.create()` oder `bookService.update()` auf

**Alternative: Template Driven Forms:**

- `[(ngModel)]="..."`-Bindings (wie im Cheatsheet)
- `model<T>()` für Two-Way-Binding mit Signals
- Einfachere Implementierung für diese Größe

**Template:**
- Formularfelder für alle Eigenschaften
- Datepicker oder Textfeld für publishedDate (mit Validierung)
- `[disabled]="!form.valid"` auf Submit-Button
- Success-Message nach Save
- „Zurück" Button (oder Abbrechen-Button)
- Bei Edit: vorhandene Daten laden und Formular füllen
- Published Date in Eingabefeld im Format YYYY-MM-DD anzeigen

---

## Bewertungskriterien

| Kriterium | Punkte |
|-----------|--------|
| Backend-Endpunkte vollständig und funktionsfähig | 20 |
| Angular Service mit HTTP-Requests | 20 |
| Routing mit Path-Parametern | 10 |
| Overview mit Tabelle, Delete und Detail-Integration | 20 |
| Detail-Subkomponente mit Input/Output | 15 |
| Form-Komponente für Add/Edit | 10 |
| Signals und reaktiver State | 10 |
| Validierung, Error-Handling und Datumsbehandlung | 5 |
| **Summe** | **100** |