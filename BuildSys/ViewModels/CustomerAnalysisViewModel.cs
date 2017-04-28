using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using BuildSys.Models;
using System.Data;

namespace BuildSys.ViewModels
{
    class CustomerAnalysisViewModel : BaseViewModel
    {

        public CustomerAnalysisViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            // Initialize the chart values to 0
            ChartValues<double> chartVals = new ChartValues<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Get the number of customers registered per month from the database
            DataTable numCustomersRegisteredPerMonth = CustomerModel.getNumCustomersRegisteredPerMonth();
            int startMonth = 0;
            int startYear = DateTime.Now.Year;

            // check that data is returned
            if (numCustomersRegisteredPerMonth.Rows.Count > 0)
            {
                // Get the month and year that are the beginning of the range
                startMonth = Int32.Parse(numCustomersRegisteredPerMonth.Rows[0]["month_code"].ToString()) - 1;
                startYear = Int32.Parse(numCustomersRegisteredPerMonth.Rows[0]["year_code"].ToString());

                // Add data to chart for each month
                foreach (DataRow numInMonth in numCustomersRegisteredPerMonth.Rows)
                {
                    chartVals[Int32.Parse(numInMonth["month_code"].ToString()) - 1] = Double.Parse(numInMonth["num_customers_registered"].ToString());
                }
            }

            GraphCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Customers Registered",
                    Values = chartVals
                }
            };

            Labels = new String[12];

            // Set the value of the label to the month names in the correct order of month
            for (int i = 0; i < 12; i++)
            {
                // The correct month and year value to each label in the righr order of month and add the correct year
                Labels[i] = MONTHS[(i + startMonth) % 12] + " " + ((i + startMonth) % 12 >= startMonth? startYear : startYear + 1);
            }

            // Formats numbers in the chart
            Formatter = value => value.ToString("N0");


            // Setting the other statistic used in the view model
            numCustomers = CustomerModel.getNumCustomers();
            numDeletedCustomers = CustomerModel.getNumCustomers("I");
            bestCustomer = CustomerModel.getBestCustomer();
        }

        // A Resuable array of the month strings
        private static readonly string[] MONTHS = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // Initialize the public variables that will be accessed from the view
        public SeriesCollection GraphCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int numCustomers { get; set; }
        public int numDeletedCustomers { get; set; }
        public CustomerModel bestCustomer { get; set; }
    }
}
