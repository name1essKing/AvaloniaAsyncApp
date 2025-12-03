using Avalonia.Controls;
using AvaloniaApp.Client.Views.AvaloniaApp;

namespace AvaloniaApp.Client.Views;

public partial class AvaloniaAppView : UserControl
{
    public AvaloniaAppView()
    {
        InitializeComponent();

    }

    private void UserControl_ActualThemeVariantChanged(object? sender, System.EventArgs e)
    {
    }
}