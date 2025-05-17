using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace tetelvizz.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        [ObservableProperty] private string _username;

        [ObservableProperty] private string _email;

        [ObservableProperty] private string _password;

        public RegisterViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        [RelayCommand]
        private async Task Register()
        {
            try
            {
                var result = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);

                if (result.User != null)
                {
                    await Shell.Current.GoToAsync("//HomeView");
                }
                else
                {
                    await ShowErrorDialog("Regisztráció sikertelen. Próbálja újra.");
                }
            }
            catch (FirebaseAuthException fae)
            {
                await ShowErrorDialog($"Regisztráció sikertelen: {fae.Reason}");
            }
            catch (Exception ex)
            {
                await ShowErrorDialog($"Váratlan hiba történt: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync("//LoginView");
        }


        private async Task ShowErrorDialog(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Hiba", message, "OK");
        }
    }
}