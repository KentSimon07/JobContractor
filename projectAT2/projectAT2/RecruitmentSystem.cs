using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace projectAT2
{
    // Stores all contractors and jobs
    public class RecruitmentSystem
    {
        // List of contractors
        private List<Contractor> contractors = new List<Contractor>();

        // List of jobs
        private List<Job> jobs = new List<Job>();

        // Next contractor ID
        private int nextContractorID = 1;

        // Next job ID
        private int nextJobID = 1;

        // Add a contractor
        public void AddContractor(Contractor c)
        {
            c.ID = nextContractorID;
            nextContractorID++;
            contractors.Add(c);
        }

        // Remove a contractor
        public void RemoveContractor(Contractor c)
        {
            contractors.Remove(c);
        }

        // Add a job
        public void AddJob(Job j)
        {
            j.ID = nextJobID;
            nextJobID++;
            jobs.Add(j);
        }

        // Assign a contractor to a job
        public void AssignJob(Job j, Contractor c)
        {
            // Stop if data is invalid
            if (j == null || c == null)
                return;

            // Do not assign completed jobs
            if (j.Completed)
                return;

            // Do not assign busy contractors
            if (!c.IsAvailable)
                return;

            j.ContractorAssigned = c;
            c.IsAvailable = false;
        }

        // Complete a job
        public void completeJob(Job j)
        {
            if (j == null)
                return;

            j.Completed = true;

            if (j.ContractorAssigned != null)
            {
                j.ContractorAssigned.IsAvailable = true;
                j.ContractorAssigned = null;
            }
        }

        // Get all contractors
        public List<Contractor> GetContractors()
        {
            return contractors.ToList();
        }

        // Get all jobs
        public List<Job> GetJobs()
        {
            return jobs.ToList();
        }

        // Get available contractors only
        public List<Contractor> GetAvailableContractors()
        {
            return contractors.Where(c => c.IsAvailable).ToList();
        }

        // Get jobs that are not assigned and not completed
        public List<Job> GetUnassignedJobs()
        {
            return jobs.Where(j => j.ContractorAssigned == null && !j.Completed).ToList();
        }

        // Search jobs by cost, but only unassigned and not completed jobs
        public List<Job> GetJobByCost(double min, double max)
        {
            return jobs.Where(j => j.cost >= min &&
                                   j.cost <= max &&
                                   j.ContractorAssigned == null &&
                                   !j.Completed).ToList();
        }

        // Search contractor by ID
        public Contractor SearchContractorByID(int id)
        {
            return contractors.FirstOrDefault(c => c.ID == id);
        }

        // Search contractor by first name
        public Contractor SearchContractorByName(string name)
        {
            return contractors.FirstOrDefault(c => c.FirstName.ToLower() == name.ToLower());
        }

        // Search jobs by date
        public List<Job> SearchJobsByDate(DateTime from, DateTime to)
        {
            return jobs.Where(j => j.Date >= from && j.Date <= to).ToList();
        }
    }
}