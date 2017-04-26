using System;
using LiveCharts;
using LiveCharts.Wpf;
using BuildSys.Models;
using System.Windows.Input;
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

            SeriesCollection = new SeriesCollection { };

            DataTable materialsByPrice = MaterialModel.getMaterialsByCost();
            
            foreach (DataRow materialRow in materialsByPrice.Rows)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = materialRow["material_price_range"].ToString(),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Int32.Parse(materialRow["number_of_occurences"].ToString())) },
                    DataLabels = true,
                    LabelPoint = PointLabel
                });
            }

            numMaterials = MaterialModel.getNumMaterials();
            avgMaterialCost = MaterialModel.getAvgMaterialCost();
            mostUsedMaterial = MaterialModel.getMostUsedMaterial();
        }

        public SeriesCollection SeriesCollection { get; set; }
        private Func<ChartPoint, string> PointLabel;
        
        public int numMaterials { get; set; }
        public double avgMaterialCost { get; set; }
        public MaterialModel mostUsedMaterial { get; set; }
    }
}
