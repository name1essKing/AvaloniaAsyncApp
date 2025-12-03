using AvaloniaApp.UI;
using AvaloniaApp.Client.Views.AvaloniaApp;
using ReactiveUI;

namespace AvaloniaApp.Client.Views
{
    public sealed partial class MainWindowViewModel : ReactiveViewModelBase
    {
        public AvaloniaAppViewModel AvaloniaAppViewModel { get; }

        public MainWindowViewModel()
        {
            AvaloniaAppViewModel = new AvaloniaAppViewModel();
        }
    }
}
