using AdvancedCalculator.ViewModels;
using System.Windows.Controls;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for MatrixCalculatorView.xaml
    /// </summary>
    public partial class MatrixCalculatorView : UserControl
    {
        public MatrixCalculatorView()
        {
            InitializeComponent();
            DataContext = new MatrixCalculatorViewModel();
        }
    }
}