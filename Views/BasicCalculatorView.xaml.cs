using AdvancedCalculator.ViewModels;
using System.Windows;

namespace AdvancedCalculator.Views
{
    /// <summary>
    /// Interaction logic for BasicCalculatorView.xaml
    /// </summary>
    public partial class BasicCalculatorView : Window
    {
        public BasicCalculatorView()
        {
            InitializeComponent();
            DataContext = new BasicCalculatorViewModel();
        }
    }
}