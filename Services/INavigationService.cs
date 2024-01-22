using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvancedCalculator.Services
{
    public interface INavigationService
    {
        ObservableObject CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    }
}