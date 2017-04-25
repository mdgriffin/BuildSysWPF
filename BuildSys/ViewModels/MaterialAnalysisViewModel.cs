using System;
using LiveCharts;
using LiveCharts.Wpf;
using BuildSys.Models;

namespace BuildSys.ViewModels
{
    class MaterialAnalysisViewModel : BaseViewModel
    {
        //MainViewModel parent;

        public MaterialAnalysisViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            GraphCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quotes Issued By Month",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 10, 50, 39, 50, 10, 50, 39, 50 }
                }
            };

            Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            Formatter = value => value.ToString("N");

            numMaterials = MaterialModel.getNumMaterials();
            avgMaterialCost = MaterialModel.getAvgMaterialCost();
        }

        public SeriesCollection GraphCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int numMaterials { get; set; }
        public double avgMaterialCost { get; set; }
    }
}
