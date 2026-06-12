using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace projectAT2
{
    internal class DatabaseLayer
    {
        private MySqlConnection connection;

        public DatabaseLayer()
        {
            string connectionString = "host=localhost;port=3306;uid=recruitment_user;pwd=Hello123;database=recruitmentsql;";
            connection = new MySqlConnection(connectionString);
        }

        public List<Contractor> GetContractors()
        {
            List<Contractor> results = new List<Contractor>();
            try
            {
                connection.Open();
                string query = "SELECT id, FirstName, LastName, StartDate, HourlyWage, IsAvailable FROM Contractor";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contractor c = new Contractor
                    {
                        ID = (int)reader.GetInt64("id"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        StartDate = reader.GetDateTime("StartDate"),
                        HourlyWage = reader.GetDouble("HourlyWage"),
                        IsAvailable = reader.GetInt32("IsAvailable") == 1
                    };
                    results.Add(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return results;
        }

        public List<Job> GetJobs()
        {
            List<Job> results = new List<Job>();
            try
            {
                connection.Open();
                string query = "SELECT id, Title, Date, cost, Completed FROM Jobs";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Job j = new Job
                    {
                        ID = (int)reader.GetInt64("id"),
                        Title = reader.GetString("Title"),
                        Date = reader.GetDateTime("Date"),
                        cost = reader.GetDouble("cost"),
                        Completed = reader.GetInt32("Completed") == 1
                    };
                    results.Add(j);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return results;
        }

        public void CreateContractor(Contractor c)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Contractor (FirstName, LastName, StartDate, HourlyWage, IsAvailable) " +
                               "VALUES (@firstName, @lastName, @startDate, @hourlyWage, @isAvailable)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", c.FirstName);
                command.Parameters.AddWithValue("@lastName", c.LastName);
                command.Parameters.AddWithValue("@startDate", c.StartDate);
                command.Parameters.AddWithValue("@hourlyWage", c.HourlyWage);
                command.Parameters.AddWithValue("@isAvailable", c.IsAvailable ? 1 : 0);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void CreateJob(Job j)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Jobs (Title, Date, cost, Completed) " +
                               "VALUES (@title, @date, @cost, @completed)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", j.Title);
                command.Parameters.AddWithValue("@date", j.Date);
                command.Parameters.AddWithValue("@cost", j.cost);
                command.Parameters.AddWithValue("@completed", j.Completed ? 1 : 0);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteContractor(Contractor c)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Contractor WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", c.ID);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AssignJob(Job j, Contractor c)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Jobs SET ContractorID = @contractorID WHERE id = @jobID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@contractorID", c.ID);
                command.Parameters.AddWithValue("@jobID", j.ID);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void CompleteJob(Job j)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Jobs SET Completed = 1 WHERE id = @jobID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@jobID", j.ID);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}