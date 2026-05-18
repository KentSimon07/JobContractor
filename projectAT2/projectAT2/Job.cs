using System;
using System.Collections.Generic;
using System.Text;

namespace projectAT2
{
    // Represents one job
    public class Job
    {
        // Job id
        public int ID { get; set; }

        // Job title
        public string Title { get; set; } = "";

        // Job date
        public DateTime Date { get; set; }

        // Job cost
        public double cost { get; set; }

        // Completed status
        public bool Completed { get; set; } = false;

        // Assigned contractor
        public Contractor ContractorAssigned { get; set; }

        // Assigned contractor name
        public string ContractorName
        {
            get
            {
                if (ContractorAssigned != null)
                    return ContractorAssigned.FullName;

                return "Unassigned";
            }
        }

        // Show job in list
        public override string ToString()
        {
            string status = Completed ? "Completed" : "Not Completed";
            return "Job: " + Title + " | " + cost.ToString("C") + " | " + ContractorName + " | " + status;
        }
    }
}       