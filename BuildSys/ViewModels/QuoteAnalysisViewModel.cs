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

            YFormatter = value => value.ToString("C");
            
            DataTable cumulativeQuoteValue = QuoteModel.getCumulativeQuoteTotal();
            int startMonth = 0;
            int startYear = DateTime.Now.Year;
            ChartValues<double> monthlyValues = new ChartValues<double>();

            if (cumulativeQuoteValue.Rows.Count > 0)
            {
                startMonth = Int32.Parse(cumulativeQuoteValue.Rows[0]["month_issued"].ToString()) - 1;
                startYear = Int32.Parse(cumulativeQuoteValue.Rows[0]["year_issued"].ToString());

                foreach (DataRow monthVal in cumulativeQuoteValue.Rows)
                {
                    monthlyValues.Add(Double.Parse(monthVal["cumulative_total"].ToString()));
                }
                
            }

            Labels = new String[cumulativeQuoteValue.Rows.Count];

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

        }

        private static readonly string[] MONTHS = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
