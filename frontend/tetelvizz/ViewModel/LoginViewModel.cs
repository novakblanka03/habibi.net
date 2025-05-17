using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace tetelvizz.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        [ObservableProperty] private string _email;

        [ObservableProperty] private string _password;

        public LoginViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                var result = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(Email, Password);

                if (result.User != null)
                {
                    Console.WriteLine("Login successful");
                    await Shell.Current.GoToAsync("//HomeView");
                }
                else
                {
                    await ShowErrorDialog("Bejelentkezés sikertelen. Próbálja újra.");
                }
            }
            catch (FirebaseAuthException fae)
            {
                await ShowErrorDialog($"Bejelentkezés sikertelen: {fae.Reason}");
            }
            catch (Exception ex)
            {
                await ShowErrorDialog($"Váratlan hiba történt: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync("//RegisterView");
        }

        private async Task ShowErrorDialog(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", message, "OK");
        }
    }
}