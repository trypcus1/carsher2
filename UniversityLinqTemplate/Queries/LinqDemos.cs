using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Data;
using Microsoft.EntityFrameworkCore;

namespace UniversityLinqTemplate.Queries
{
    public static class LinqDemos
    {
        public static void DemoFilteringAndSorting(UniversityContext db)
        {
            Console.WriteLine("\n=== 1. Дорогие заказ-наряды (>= 4000 ₽), тип 'ТО' или 'Ремонт' ===");
            Console.WriteLine("    (сортировка: стоимость по убыванию, затем по дате)");

            var result = db.ServiceOrders
                .Where(o => o.Cost >= 4000m && (o.ServiceType == "ТО" || o.ServiceType == "Ремонт"))
                .OrderByDescending(o => o.Cost)
                .ThenBy(o => o.OrderDate)
                .ToList();

            foreach (var o in result)
                Console.WriteLine($"  {o.OrderDate:dd.MM.yyyy} — [{o.ServiceType}] {o.Description}: {o.Cost:F2} ₽");
        }
        
        public static void DemoProjection(UniversityContext db)
        {
            Console.WriteLine("\n=== 2. Проекция: клиент — авто — пробег — номер паспорта ===");

            var result = db.Cars
                .Include(c => c.Client)
                .Include(c => c.Passport)
                .Select(c => new
                {
                    Owner = c.Client!.FullName,
                    Vehicle = $"{c.Brand} {c.Model} ({c.Year})",
                    Plate = c.LicensePlate,
                    Mileage = c.Passport != null ? c.Passport.Mileage : 0,
                    PassportNo = c.Passport != null ? c.Passport.PassportNumber : "—"
                })
                .ToList();

            foreach (var item in result)
                Console.WriteLine($"  {item.Owner} → {item.Vehicle} [{item.Plate}] | пробег: {item.Mileage} км | паспорт: {item.PassportNo}");
        }

        public static void DemoAggregations(UniversityContext db)
        {
            Console.WriteLine("\n=== 3. Агрегатные вычисления по автосервису ===");

            int totalClients = db.Clients.Count();
            int totalOrders = db.ServiceOrders.Count();
            decimal totalRevenue = db.ServiceOrders.Sum(o => o.Cost);
            decimal avgOrderCost = db.ServiceOrders.Average(o => o.Cost);
            decimal minCost = db.ServiceOrders.Min(o => o.Cost);
            decimal maxCost = db.ServiceOrders.Max(o => o.Cost);
            int maxMileage = db.ServicePassports.Max(p => p.Mileage);

            Console.WriteLine($"  Клиентов в базе:        {totalClients}");
            Console.WriteLine($"  Заказ-нарядов всего:    {totalOrders}");
            Console.WriteLine($"  Общая выручка:          {totalRevenue:F2} ₽");
            Console.WriteLine($"  Средний чек:            {avgOrderCost:F2} ₽");
            Console.WriteLine($"  Мин. стоимость заказа:  {minCost:F2} ₽");
            Console.WriteLine($"  Макс. стоимость заказа: {maxCost:F2} ₽");
            Console.WriteLine($"  Макс. пробег авто:      {maxMileage} км");
        }
        
        public static void DemoGrouping(UniversityContext db)
        {
            Console.WriteLine("\n=== 4. Группировка заказ-нарядов по типу услуги ===");

            var summary = db.ServiceOrders
                .GroupBy(o => o.ServiceType)
                .Select(g => new
                {
                    ServiceType = g.Key,
                    OrdersCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.Cost),
                    AvgCost = g.Average(o => o.Cost)
                })
                .OrderByDescending(g => g.TotalRevenue)
                .ToList();

            foreach (var item in summary)
                Console.WriteLine($"  [{item.ServiceType}] заказов = {item.OrdersCount}, " +
                                  $"выручка = {item.TotalRevenue:F2} ₽, средний чек = {item.AvgCost:F2} ₽");
        }

        public static void DemoJoinsAndIncludes(UniversityContext db)
        {
            Console.WriteLine("\n=== 5. Клиенты → Автомобили → Паспорт → Заказ-наряды ===");

            var clients = db.Clients
                .Include(c => c.Cars)
                    .ThenInclude(car => car.Passport)
                .Include(c => c.Cars)
                    .ThenInclude(car => car.Orders)
                .ToList();

            foreach (var client in clients)
            {
                Console.WriteLine($"\n  {client.FullName} ({client.Phone}):");
                if (!client.Cars.Any())
                {
                    Console.WriteLine("    — (нет автомобилей)");
                    continue;
                }

                foreach (var car in client.Cars)
                {
                    var plate = car.LicensePlate;
                    var miles = car.Passport != null ? car.Passport.Mileage : 0;
                    Console.WriteLine($"    • {car.Brand} {car.Model} [{plate}] — пробег {miles} км");

                    if (!car.Orders.Any())
                    {
                        Console.WriteLine("        (нет заказ-нарядов)");
                        continue;
                    }

                    foreach (var o in car.Orders.OrderByDescending(o => o.OrderDate))
                        Console.WriteLine($"        – {o.OrderDate:dd.MM.yyyy} [{o.ServiceType}] {o.Description} — {o.Cost:F2} ₽");
                }
            }
        }
    }
}