using AdvancedCalculator.ViewModels;
using System.Windows.Controls;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for FunctionPlotView.xaml
    /// </summary>
    public partial class FunctionPlotView : UserControl
    {
        public FunctionPlotView()
        {
            InitializeComponent();
            DataContext = new FunctionPlotViewModel();
        }
    }
}