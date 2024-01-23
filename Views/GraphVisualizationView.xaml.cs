using AdvancedCalculator.ViewModels;
using System.Windows.Controls;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphVisualizationView : UserControl
    {
        public GraphVisualizationView()
        {
            InitializeComponent();
            DataContext = new GraphVisualizationViewModel();
        }
    }
}