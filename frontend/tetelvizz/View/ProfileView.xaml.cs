using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetelvizz.ViewModel;

namespace tetelvizz.View;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileViewModel profileViewModel)
    {
        InitializeComponent();

        BindingContext = profileViewModel;
    }
}