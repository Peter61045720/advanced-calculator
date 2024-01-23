using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace AdvancedCalculator.ViewModels
{
    public partial class FunctionPlotViewModel : ObservableObject
    {
        [ObservableProperty]
        private SeriesCollection _pointCollection;

        [ObservableProperty]
        private Func<double, string> _yFormatter;

        public FunctionPlotViewModel()
        {
            PointCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = null
                }
            };
            YFormatter = value => $"{value:0.0}";
            GenerateData();
        }

        private void GenerateData()
        {
            var lineSeries = PointCollection[0];

            for (double x = -5; x <= 5; x += 0.5)
            {
                double y = Math.Pow(Math.E, -Math.Pow(x, 2));
                lineSeries.Values.Add(new ObservablePoint(x, y));
            }
        }
    }
}