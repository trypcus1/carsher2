using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UniversityContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var client1 = new Client { FullName = "Иванов Иван Иванович", Phone = "+7 (900) 123-45-67", Email = "ivanov@mail.ru" };
            var client2 = new Client { FullName = "Петрова Мария Сергеевна", Phone = "+7 (911) 765-43-21", Email = "petrova@mail.ru" };
            var client3 = new Client { FullName = "Сидоров Олег Николаевич", Phone = "+7 (922) 555-66-77", Email = "sidorov@mail.ru" };
            context.Clients.AddRange(client1, client2, client3);

            var car1 = new Car { Brand = "Toyota", Model = "Camry", Year = 2020, LicensePlate = "А123БВ 77", Client = client1 };
            var car2 = new Car { Brand = "BMW", Model = "X5", Year = 2022, LicensePlate = "В456ГД 99", Client = client1 };
            var car3 = new Car { Brand = "Kia", Model = "Rio", Year = 2019, LicensePlate = "Е789ЖЗ 50", Client = client2 };
            var car4 = new Car { Brand = "Lada", Model = "Vesta", Year = 2021, LicensePlate = "К321ЛМ 77", Client = client3 };
            context.Cars.AddRange(car1, car2, car3, car4);

            context.ServicePassports.AddRange(
                new ServicePassport { Car = car1, PassportNumber = "СП-0001", Mileage = 45000, IssueDate = DateTime.Now.AddYears(-3) },
                new ServicePassport { Car = car2, PassportNumber = "СП-0002", Mileage = 18000, IssueDate = DateTime.Now.AddYears(-2) },
                new ServicePassport { Car = car3, PassportNumber = "СП-0003", Mileage = 72000, IssueDate = DateTime.Now.AddYears(-5) },
                new ServicePassport { Car = car4, PassportNumber = "СП-0004", Mileage = 12000, IssueDate = DateTime.Now.AddYears(-1) }
            );

            context.ServiceOrders.AddRange(
                new ServiceOrder { Car = car1, OrderDate = DateTime.Now.AddDays(-90), ServiceType = "ТО", Description = "Замена масла и фильтров", Cost = 5500m },
                new ServiceOrder { Car = car1, OrderDate = DateTime.Now.AddDays(-20), ServiceType = "Диагностика", Description = "Проверка подвески", Cost = 1500m },
                new ServiceOrder { Car = car2, OrderDate = DateTime.Now.AddDays(-60), ServiceType = "ТО", Description = "ТО-1 (регламентное)", Cost = 12000m },
                new ServiceOrder { Car = car3, OrderDate = DateTime.Now.AddDays(-30), ServiceType = "Ремонт", Description = "Замена тормозных колодок", Cost = 4800m },
                new ServiceOrder { Car = car3, OrderDate = DateTime.Now.AddDays(-5), ServiceType = "Диагностика", Description = "Компьютерная диагностика", Cost = 2000m },
                new ServiceOrder { Car = car4, OrderDate = DateTime.Now.AddDays(-15), ServiceType = "Ремонт", Description = "Замена ремня ГРМ", Cost = 9800m }
            );

            context.SaveChanges();
        }
    }
}