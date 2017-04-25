using System;
using LiveCharts;
using LiveCharts.Wpf;
using BuildSys.Models;
using System.Windows.Input;
using System.Data.Common;
using LiveCharts.Events;
using LiveCharts.Defaults;
using System.Data;

namespace BuildSys.ViewModels
{
    class MaterialAnalysisViewModel : BaseViewModel
    {

        public MaterialAnalysisViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            /*
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "10 To 20",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "20 To 30",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "30 To 40",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "40 To 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "50 To 60",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "60 To 70",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "70 To 80",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "80 To 90",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "90 To 100",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Over 100",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                }
            };
            */

            SeriesCollection = new SeriesCollection { };

            DataTable materialsByPrice = MaterialModel.getMaterialsByCost();
            
            foreach (DataRow materialRow in materialsByPrice.Rows)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = materialRow["material_price_range"].ToString(),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Int32.Parse(materialRow["number_of_occurences"].ToString())) },
                    DataLabels = true
                });
            }

            numMaterials = MaterialModel.getNumMaterials();
            avgMaterialCost = MaterialModel.getAvgMaterialCost();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }

        public int numMaterials { get; set; }
        public double avgMaterialCost { get; set; }
    }
}
