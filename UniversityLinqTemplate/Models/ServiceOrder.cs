using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}