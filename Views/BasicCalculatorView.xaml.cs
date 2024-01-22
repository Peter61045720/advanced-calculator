using AdvancedCalculator.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for BasicCalculatorView.xaml
    /// </summary>
    public partial class BasicCalculatorView : UserControl
    {
        public BasicCalculatorView()
        {
            InitializeComponent();
            DataContext = new BasicCalculatorViewModel();
        }
    }
}