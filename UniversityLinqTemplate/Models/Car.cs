using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public ServicePassport? Passport { get; set; }

        public List<ServiceOrder> Orders { get; set; } = new();
    }
}