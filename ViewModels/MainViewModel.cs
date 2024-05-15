using AdvancedCalculator.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvancedCalculator.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigationService.NavigateTo<BasicCalculatorViewModel>();
        }

        [RelayCommand]
        public void NavigateToBasicCalculator()
        {
            NavigationService.NavigateTo<BasicCalculatorViewModel>();
        }

        [RelayCommand]
        public void NavigateToScientificCalculator()
        {
            NavigationService.NavigateTo<ScientificCalculatorViewModel>();
        }

        [RelayCommand]
        public void NavigateToComplexCalculator()
        {
            NavigationService.NavigateTo<ComplexCalculatorViewModel>();
        }

        [RelayCommand]
        public void NavigateToGraphVisualization()
        {
            NavigationService.NavigateTo<GraphVisualizationViewModel>();
        }

        [RelayCommand]
        public void NavigateToFunctionPlot()
        {
            NavigationService.NavigateTo<FunctionPlotViewModel>();
        }
    }
}