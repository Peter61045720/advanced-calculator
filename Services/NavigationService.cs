using AdvancedCalculator.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvancedCalculator.Services
{
    public partial class NavigationService : ObservableObject, INavigationService
    {
        private Func<Type, ObservableObject> _viewModelFactory;

        [ObservableProperty]
        private ObservableObject _currentView;

        public NavigationService(Func<Type, ObservableObject> viewModelFactory) 
        {
            _viewModelFactory = viewModelFactory;
            _currentView = new BasicCalculatorViewModel();
        }

        public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
        {
            ObservableObject viewModel =  _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}