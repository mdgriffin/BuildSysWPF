using System;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using BuildSys.Models;

namespace BuildSys.ViewModels
{
    class QuoteAnalysisViewModel : BaseViewModel
    {

        public QuoteAnalysisViewModel (BaseViewModel parent)
        {
            this.parent = parent;

            // Format as currency
            YFormatter = value => value.ToString("C");
            
            // Gets the cumulative total value of quotes
            DataTable cumulativeQuoteValue = QuoteModel.getCumulativeQuoteTotal();

            //  Set the initial start month to 0
            int startMonth = 0;
            // Initial year is present year
            int startYear = DateTime.Now.Year;
            ChartValues<double> monthlyValues = new ChartValues<double>();

            if (cumulativeQuoteValue.Rows.Count > 0)
            {
                // Set the present month and year to the current year
                startMonth = Int32.Parse(cumulativeQuoteValue.Rows[0]["month_issued"].ToString()) - 1;
                startYear = Int32.Parse(cumulativeQuoteValue.Rows[0]["year_issued"].ToString());

                foreach (DataRow monthVal in cumulativeQuoteValue.Rows)
                {
                    monthlyValues.Add(Double.Parse(monthVal["cumulative_total"].ToString()));
                }
                
            }

            Labels = new String[cumulativeQuoteValue.Rows.Count];

            // Set order of the label from the correct month
            for (int i = 0; i < cumulativeQuoteValue.Rows.Count; i++)
            {
                // The correct month and year value to each label in the righr order of month and add the correct year
                Labels[i] = MONTHS[(i + startMonth) % 12] + " " + ((i + startMonth) % 12 >= startMonth ? startYear : startYear + 1);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Cumulative Value of Quotes",
                    Values = monthlyValues 
                }
            };

            // Other statistical information 
            mostRecentQuote = QuoteModel.getMostRecentQuote();
            avgQuoteValue = QuoteModel.getAverageQuoteValue().ToString("C2");
            numQuotes = QuoteModel.getNumQuotes();
        }

        // Months used to set the values of the labels
        private static readonly string[] MONTHS = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // Properties accessible from the view
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public QuoteModel mostRecentQuote { get; set; }
        public String avgQuoteValue { get; set; }
        public int numQuotes{ get; set; }
    }
}
