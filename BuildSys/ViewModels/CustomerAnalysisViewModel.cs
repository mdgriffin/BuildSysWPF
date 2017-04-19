﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;

namespace BuildSys.ViewModels
{
    class CustomerAnalysisViewModel : BaseViewModel
    {
        //MainViewModel parent;

        public CustomerAnalysisViewModel(BaseViewModel parent)
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
        }

        // TODO: Remove all getInstance methods
        public override BaseViewModel getInstance(BaseViewModel parent)
        {
            return new CustomerAnalysisViewModel(parent);
        }

        public SeriesCollection GraphCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
