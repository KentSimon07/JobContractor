using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace projectAT2
{
    /// <summary>
    /// Stores all contractors and jobs.
    /// </summary>
    public class RecruitmentSystem
    {
        private List<Contractor> contractors = new List<Contractor>();
        private List<Job> jobs = new List<Job>();
        private int nextContractorID = 1;
        private int nextJobID = 1;

        /// <summary>
        /// Add a contractor.
        /// </summary>
        public void AddContractor(Contractor c)
        {
            c.ID = nextContractorID++;
            contractors.Add(c);
        }

        /// <summary>
        /// Remove a contractor.
        /// </summary>
        public void RemoveContractor(Contractor c)
        {
            contractors.Remove(c);
        }

        /// <summary>
        /// Add a job.
        /// </summary>
        public void AddJob(Job j)
        {
            j.ID = nextJobID++;
            jobs.Add(j);
        }

        /// <summary>
        /// Assign a contractor to a job.
        /// </summary>
        public void AssignJob(Job j, Contractor c)
        {
            if (j == null || c == null) return;
            if (j.Completed) return;
            if (!c.IsAvailable) return;

            j.ContractorAssigned = c;
            c.IsAvailable = false;
        }

        /// <summary>
        /// Complete a job and return contractor to available pool.
        /// </summary>
        public void completeJob(Job j)
        {
            if (j == null) return;

            j.Completed = true;

            if (j.ContractorAssigned != null)
            {
                j.ContractorAssigned.IsAvailable = true;
                j.ContractorAssigned = null;
            }
        }

        /// <summary>
        /// Get all contractors.
        /// </summary>
        public List<Contractor> GetContractors()
        {
            return contractors.ToList();
        }

        /// <summary>
        /// Get all jobs.
        /// </summary>
        public List<Job> GetJobs()
        {
            return jobs.ToList();
        }

        /// <summary>
        /// Get only available contractors.
        /// </summary>
        public List<Contractor> GetAvailableContractors()
        {
            return contractors.Where(c => c.IsAvailable).ToList();
        }

        /// <summary>
        /// Get jobs that are not assigned and not completed.
        /// </summary>
        public List<Job> GetUnassignedJobs()
        {
            return jobs.Where(j => j.ContractorAssigned == null && !j.Completed).ToList();
        }

        /// <summary>
        /// Search jobs by cost, only available jobs.
        /// </summary>
        public List<Job> GetJobByCost(double min, double max)
        {
            return jobs.Where(j =>
                j.cost >= min &&
                j.cost <= max &&
                j.ContractorAssigned == null &&
                !j.Completed).ToList();
        }

        /// <summary>
        /// Search contractor by ID.
        /// </summary>
        public Contractor SearchContractorByID(int id)
        {
            return contractors.FirstOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Search contractor by first name.
        /// </summary>
        public Contractor SearchContractorByName(string name)
        {
            return contractors.FirstOrDefault(c => c.FirstName.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Search jobs by date.
        /// </summary>
        public List<Job> SearchJobsByDate(DateTime from, DateTime to)
        {
            return jobs.Where(j => j.Date >= from && j.Date <= to).ToList();
        }
    }
}