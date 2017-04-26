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

            /*
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");
            */

            DataTable cumulativeQuoteValue = QuoteModel.getCumulativeQuoteTotal();
            int startMonth = 0;
            int startYear = DateTime.Now.Year;

           

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
