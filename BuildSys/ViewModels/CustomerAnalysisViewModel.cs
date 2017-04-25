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
        //MainViewModel parent;

        public CustomerAnalysisViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            numCustomers = CustomerModel.getNumCustomers();

            ChartValues<double> chartVals = new ChartValues<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            DataTable numCustomersRegisteredPerMonth = CustomerModel.getNumCustomersRegisteredPerMonth();
            int startMonth = 0;

            if (numCustomersRegisteredPerMonth.Rows.Count > 0)
            {
                startMonth = Int32.Parse(numCustomersRegisteredPerMonth.Rows[0]["month_code"].ToString()) - 1;

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

            for (int i = 0; i < 12; i++)
            {
                Labels[i] = MONTHS[(i + startMonth) % 12];
            }

            Formatter = value => value.ToString("N0");
        }

        private static readonly string[] MONTHS = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public SeriesCollection GraphCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int numCustomers { get; set; }
    }
}
