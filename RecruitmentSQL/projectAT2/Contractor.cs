using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace projectAT2
{
    // Represents one contractor
    public class Contractor
    {
        // Contractor id
        public int ID { get; set; }

        // First name
        public string FirstName { get; set; } = "";

        // Last name
        public string LastName { get; set; } = "";

        // Start date
        public DateTime StartDate { get; set; }

        // Hourly pay
        public double HourlyWage { get; set; }

        // Availability status
        public bool IsAvailable { get; set; } = true;

        // Full name
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        // Display in list
        public override string ToString()
        {
            return "Contractor: " + FullName + " | " + HourlyWage.ToString("C");
        }
    }
}