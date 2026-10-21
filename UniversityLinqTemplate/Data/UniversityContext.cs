using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public class UniversityContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<ServicePassport> ServicePassports => Set<ServicePassport>();
        public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=autoservice.db");
        }
    }
}