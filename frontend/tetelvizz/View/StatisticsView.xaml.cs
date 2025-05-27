using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetelvizz.ViewModel;

namespace tetelvizz.View;

public partial class RegisterView : ContentPage
{
    public RegisterView( RegisterViewModel registerViewModel)
    {
        InitializeComponent();

        BindingContext = registerViewModel;
    }
}