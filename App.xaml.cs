using Game_Safe_of_the_pilot_brothers.Model;
using Game_Safe_of_the_pilot_brothers.View;
using System.Windows;
using Autofac;
using Game_Safe_of_the_pilot_brothers.ViewModel;

namespace Game_Safe_of_the_pilot_brothers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void _Startup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<GameModule>();
            builder.Register(c => new MainViewModel());
            var container = builder.Build();
            GameModule.Container = container;
            var model = container.Resolve<MainViewModel>();
            var view = new MainWindow { DataContext = model };
            view.Show();
        }
    }
}
