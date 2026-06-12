using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projectAT2
{
    /// <summary>Main window logic.</summary>
    public partial class MainWindow : Window
    {
        private RecruitmentSystem system = new RecruitmentSystem();
        private DatabaseLayer db = new DatabaseLayer();

        /// <summary>Initializes the window.</summary>
        public MainWindow()
        {
            InitializeComponent();
            
            foreach (Contractor c in db.GetContractors())
                system.AddContractor(c);
            foreach (Job j in db.GetJobs())
                system.AddJob(j);

            LoadData_Click(this, new RoutedEventArgs());
            RefreshLists_Click(this, new RoutedEventArgs());
        }

        /// <summary>Adds a new contractor.</summary>
        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                dpStartDate.SelectedDate == null ||
                !double.TryParse(txtWage.Text, out double wage))
            {
                MessageBox.Show("Please enter contractor details correctly.");
                return;
            }

            Contractor c = new Contractor
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                StartDate = dpStartDate.SelectedDate.Value,
                HourlyWage = wage
            };

            system.AddContractor(c);
            db.CreateContractor(c);
            LoadData_Click(sender, e);
            RefreshLists_Click(sender, e);
            ClearContractorFields();
        }

        /// <summary>Removes the selected contractor.</summary>
        private void RemoveContractor_Click(object sender, RoutedEventArgs e)
        {
            if (Listbox_Contractors.SelectedItem is Contractor c)
            {
                system.RemoveContractor(c);
                db.DeleteContractor(c);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
            }
        }

        /// <summary>Adds a new job.</summary>
        private void AddJob_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJobTitle.Text) ||
                dpJobDate.SelectedDate == null ||
                !double.TryParse(txtJobCost.Text, out double cost))
            {
                MessageBox.Show("Please enter job details correctly.");
                return;
            }

            Job j = new Job
            {
                Title = txtJobTitle.Text,
                Date = dpJobDate.SelectedDate.Value,
                cost = cost
            };

            system.AddJob(j);
            db.CreateJob(j);
            LoadData_Click(sender, e);
            RefreshLists_Click(sender, e);
            ClearJobFields();
        }

        /// <summary>Refreshes dropdown lists.</summary>
        private void RefreshLists_Click(object sender, RoutedEventArgs e)
        {
            cmbContractors.ItemsSource = null;
            cmbJobs.ItemsSource = null;
            cmbContractors.ItemsSource = system.GetAvailableContractors();
            cmbJobs.ItemsSource = system.GetUnassignedJobs();
        }

        /// <summary>Assigns a job to a contractor.</summary>
        private void AssignJob_Click(object sender, RoutedEventArgs e)
        {
            if (cmbContractors.SelectedItem is Contractor c && cmbJobs.SelectedItem is Job j)
            {
                system.AssignJob(j, c);
                db.AssignJob(j, c);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a contractor and a job.");
            }
        }

        /// <summary>Completes the selected job.</summary>
        private void CompleteJob_Click(object sender, RoutedEventArgs e)
        {
            if (Listbox_Jobs.SelectedItem is Job j)
            {
                system.completeJob(j);
                db.CompleteJob(j);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
                MessageBox.Show("Job completed.");
            }
            else
            {
                MessageBox.Show("Please select a job from the Jobs list.");
            }
        }

        /// <summary>Searches jobs by cost.</summary>
        private void SearchJobs_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtMinCost.Text, out double min) ||
                !double.TryParse(txtMaxCost.Text, out double max))
            {
                MessageBox.Show("Please enter valid min and max cost.");
                return;
            }

            Listbox_Jobs.ItemsSource = system.GetJobByCost(min, max);
        }

        /// <summary>Loads contractors and jobs into the lists.</summary>
        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            Listbox_Contractors.ItemsSource = null;
            Listbox_Jobs.ItemsSource = null;

            Listbox_Contractors.ItemsSource = system.GetContractors();
            Listbox_Jobs.ItemsSource = system.GetJobs();
        }

        /// <summary>Clears contractor input fields.</summary>
        private void ClearContractorFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtWage.Clear();
            dpStartDate.SelectedDate = null;
        }

        /// <summary>Clears job input fields.</summary>
        private void ClearJobFields()
        {
            txtJobTitle.Clear();
            txtJobCost.Clear();
            dpJobDate.SelectedDate = null;
        }
    }
}