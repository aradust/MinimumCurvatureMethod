**Word Frequency Counter** — это программа для подсчета частоты слов в тексте и сохранения результатов в CSV-файл.

---

## 📂 Структура проекта
```
lab0_java/
|—— src/
|   |—— main/java/org/example/
|   |   |—— Main.java               # Главный класс, запускающий программу
|   |   |—— Frequency_Counter.java  # Подсчет частоты слов
|   |   |—— Writer.java             # Запись данных в CSV
|   |   |—— Reader.java             # Чтение файла
|   |—— test/java/ru/nsu/lab0/      # Папка с тестами
|   |   |—— FrequencyCounterTest.java  # Тесты для Frequency_Counter
|   |   |—— ReaderTest.java             # Тесты для Reader
|   |   |—— WriterTest.java             # Тесты для Writer
|—— build.gradle                     # Файл сборки Gradle
|—— settings.gradle                   # Конфигурация проекта
|—— README.md                         # Описание проекта
|—— input.txt                         # Исходный файл с текстом
|—— output.csv                         # Файл с результатами
```

---

## 🌟 Описание классов

### 🏁 1) `Main.java` (Главный класс)
Запускает программу:
- Читает слова из файла `Reader.java`.
- Вызывает `Frequency_Counter` для подсчета частот.
- Вызывает `Writer` для сохранения данных в CSV.

### 🔢 `Frequency_Counter.java` (Подсчет частоты)
- Принимает список слов, считает их частоту и сохраняет в `TreeMap<String, Integer>`.
- Метод `getMap()` возвращает таблицу частот.
- Метод `getSize()` возвращает общее число слов.

### 📄 `Writer.java` (Запись в CSV)
- Открывает `output.csv` и записывает заголовок:
  ```
  Слово,Частота,Частота в %
  ```
- Добавляет каждое слово, его частоту и процентное соотношение.

### 📖 `Reader.java` (Чтение файла)
- Метод `read()` открывает `input.txt` и читает его.

