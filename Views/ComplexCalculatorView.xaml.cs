using AdvancedCalculator.ViewModels;
using System.Windows.Controls;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for ComplexCalculatorView.xaml
    /// </summary>
    public partial class ComplexCalculatorView : UserControl
    {
        public ComplexCalculatorView()
        {
            InitializeComponent();
            DataContext = new ComplexCalculatorViewModel();
        }
    }
}