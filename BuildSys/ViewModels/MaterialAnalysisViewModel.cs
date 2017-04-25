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

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            numMaterials = MaterialModel.getNumMaterials();
            avgMaterialCost = MaterialModel.getAvgMaterialCost();
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        /*
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
        */

        public int numMaterials { get; set; }
        public double avgMaterialCost { get; set; }
    }
}
