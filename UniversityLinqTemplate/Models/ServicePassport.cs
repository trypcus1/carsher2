using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    public class ServicePassport
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public int Mileage { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}