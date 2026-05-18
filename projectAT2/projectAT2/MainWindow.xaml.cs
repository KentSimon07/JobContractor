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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projectAT2
{
    // Handles window actions
    public partial class MainWindow : Window
    {
        // Business logic object
        private RecruitmentSystem system = new RecruitmentSystem();

        // Window constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        // Add a contractor
        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                dpStartDate.SelectedDate == null ||
                !double.TryParse(txtWage.Text, out double wage))
            {
                MessageBox.Show("Please enter contractor details correctly.");
                return;
            }

            // Create contractor object
            Contractor c = new Contractor
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                StartDate = dpStartDate.SelectedDate.Value,
                HourlyWage = wage
            };

            // Save contractor
            system.AddContractor(c);

            // Refresh display
            LoadData_Click(sender, e);
            ClearContractorFields();
            RefreshLists_Click(sender, e);
        }

        // Remove selected contractor
        private void RemoveContractor_Click(object sender, RoutedEventArgs e)
        {
            if (Listbox_People.SelectedItem is Contractor c)
            {
                system.RemoveContractor(c);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
            }
        }

        // Add a job
        private void AddJob_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtJobTitle.Text) ||
                dpJobDate.SelectedDate == null ||
                !double.TryParse(txtJobCost.Text, out double cost))
            {
                MessageBox.Show("Please enter job details correctly.");
                return;
            }

            // Create job object
            Job j = new Job
            {
                Title = txtJobTitle.Text,
                Date = dpJobDate.SelectedDate.Value,
                cost = cost
            };

            // Save job
            system.AddJob(j);

            // Refresh display
            LoadData_Click(sender, e);
            ClearJobFields();
            RefreshLists_Click(sender, e);
        }

        // Load combo box data
        private void RefreshLists_Click(object sender, RoutedEventArgs e)
        {
            cmbContractors.ItemsSource = null;
            cmbJobs.ItemsSource = null;
            cmbContractors.ItemsSource = system.GetAvailableContractors();
            cmbJobs.ItemsSource = system.GetUnassignedJobs();
        }

        // Assign contractor to job
        private void AssignJob_Click(object sender, RoutedEventArgs e)
        {
            if (cmbContractors.SelectedItem is Contractor c && cmbJobs.SelectedItem is Job j)
            {
                system.AssignJob(j, c);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a contractor and a job.");
            }
        }

        // Complete selected job
        private void CompleteJob_Click(object sender, RoutedEventArgs e)
        {
            if (Listbox_People.SelectedItem is Job j)
            {
                system.completeJob(j);
                LoadData_Click(sender, e);
                RefreshLists_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a job from the Records list.");
            }
        }

        // Search jobs by cost
        private void SearchJobs_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtMinCost.Text, out double min) ||
                !double.TryParse(txtMaxCost.Text, out double max))
            {
                MessageBox.Show("Please enter valid min and max cost.");
                return;
            }

            Listbox_People.ItemsSource = system.GetJobByCost(min, max);
        }

        // Load all records
        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            List<object> items = new List<object>();
            items.AddRange(system.GetContractors());
            items.AddRange(system.GetJobs());
            Listbox_People.ItemsSource = items;
        }

        // Clear contractor inputs
        private void ClearContractorFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtWage.Clear();
            dpStartDate.SelectedDate = null;
        }

        // Clear job inputs
        private void ClearJobFields()
        {
            txtJobTitle.Clear();
            txtJobCost.Clear();
            dpJobDate.SelectedDate = null;
        }
    }
}