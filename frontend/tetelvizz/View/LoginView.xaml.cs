using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetelvizz.ViewModel;

namespace tetelvizz.View;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();

        BindingContext = loginViewModel;
    }
}