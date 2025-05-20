using Firebase.Auth;
using tetelvizz.View;
using tetelvizz.ViewModel;

namespace tetelvizz;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var shell = new AppShell();
        var window = new Window(shell);

        shell.Dispatcher.Dispatch(async () => { await Shell.Current.GoToAsync("//HomeView"); });

        return window;
    }
}