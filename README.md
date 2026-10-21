# LINQ + Entity Framework Core: Автосервис

Учебный проект на базе EF Core и SQLite для освоения LINQ на примере автосервиса.
Показаны связи 1:N и 1:1, фильтрация, сортировка, проекция, агрегация,
группировка и жадная загрузка через `Include / ThenInclude`.

## Требования
* Visual Studio 2026
* .NET 10.0 или выше
* NuGet-пакет `Microsoft.EntityFrameworkCore.Sqlite`

## Запуск
1. Открыть решение (.sln / .slnx).
2. **Ctrl + F5**.
3. Программа создаст файл `autoservice.db` в папке сборки, наполнит его и выполнит 5 LINQ-демо.

## Структура проекта
* **Models**
  * `Client.cs` — клиент (1:N с Car).
  * `Car.cs` — автомобиль (1:1 с ServicePassport, 1:N с ServiceOrder).
  * `ServicePassport.cs` — сервисный паспорт (1:1 с Car).
  * `ServiceOrder.cs` — заказ-наряд.
* **Data**
  * `UniversityContext.cs` — контекст БД.
  * `DbInitializer.cs` — пересоздание и сидирование.
* **Queries**
  * `LinqDemos.cs` — 5 демо-запросов.
* `Program.cs` — точка входа.

## Какие критерии LINQ покрыты
| Критерий | Где показан |
|---|---|
| Filtering (`Where` + `&&`/`\|\|`) | Demo 1 |
| Sorting (`OrderByDescending` + `ThenBy`) | Demo 1, Demo 4 |
| Projection (`Select` в анонимный тип) | Demo 2 |
| Aggregation (`Count`, `Sum`, `Average`, `Min`, `Max`) | Demo 3 |
| Grouping (`GroupBy` + агрегаты) | Demo 4 |
| Eager loading (`Include` + `ThenInclude`) | Demo 5 |