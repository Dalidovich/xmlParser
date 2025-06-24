# XML Parser Project

Проект представляет собой библиотеку для парсинга XML-документов, извлечения данных и их десериализации в объекты C#.

## Основные компоненты

### 1. Интерфейсы
- **ITagRegexManager**: Управление регулярными выражениями для работы с XML-тегами.
- **IObjectDetectiveService<T>**: Сервис для поиска и десериализации XML-элементов в объекты типа `T`.

### 2. Реализации
- **XmlTagRegexManager**: Реализация `ITagRegexManager` для работы с открывающими и закрывающими тегами XML.
- **ObjectDetectiveService<T>**: Реализация `IObjectDetectiveService<T>` для фильтрации и десериализации XML-элементов.
- **XmlParserService**: Основной сервис для загрузки и парсинга XML-документов.

### 3. Модели данных
- **BaseElement**: Базовый класс для представления XML-элементов с атрибутами и дочерними элементами.
- **TestModel**: Пример модели данных для тестирования.
- **InnerTestModel**: Вложенная модель данных.

### 4. Вспомогательные классы
- **Program**: Точка входа в приложение с примером использования.

## Как использовать

### 1. Парсинг XML-документа
```csharp
var parserService = await XmlParserService.CreateAsync("test.xml");
var elements = parserService.GetParsedElements();
```

### 2. Десериализация XML-элементов в объекты
```csharp
var detective = new ObjectDetectiveService<TestModel>(elements, true);
var models = detective.GetListObjectElements().ToArray();
```

### 3. Настройка фильтрации
Параметр liteFilter в ObjectDetectiveService позволяет игнорировать проверку количества дочерних элементов.

Пример XML-документа
```xml
<TestModel>
    <Name>John Doe</Name>
    <mail>john@example.com</mail>
    <age>30</age>
    <InnerTestModel>
        <name>Inner John</name>
        <mail>inner@example.com</mail>
    </InnerTestModel>
</TestModel>
```
