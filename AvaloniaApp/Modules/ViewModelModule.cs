using Autofac;
using AvaloniaApp.Client.Views;
using AvaloniaApp.Client;

namespace AvaloniaApp.Client.Modules
{
    internal class ViewModelsModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainWindowView>()
                .AsSelf()
                .SingleInstance();
            builder
                .RegisterType<MainWindowViewModel>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
