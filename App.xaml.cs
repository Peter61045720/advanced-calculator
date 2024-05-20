using AdvancedCalculator.Services;
using AdvancedCalculator.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AdvancedCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<BasicCalculatorViewModel>();
            services.AddSingleton<ScientificCalculatorViewModel>();
            services.AddSingleton<ComplexCalculatorViewModel>();
            services.AddSingleton<MatrixCalculatorViewModel>();
            services.AddSingleton<GraphVisualizationViewModel>();
            services.AddSingleton<FunctionPlotViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ObservableObject>>(provider => viewModelType => (ObservableObject)provider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}