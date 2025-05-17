using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;

namespace tetelvizz.ViewModel
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty] private string _username;

        public ProfileViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            if (firebaseAuthClient.User != null)
            {
                Username = firebaseAuthClient.User.Info.DisplayName;
            }
            else
            {
                Username = "Unknown User";
            }
        }
    }
}