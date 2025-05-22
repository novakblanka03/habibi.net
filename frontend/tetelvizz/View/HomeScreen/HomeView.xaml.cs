using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetelvizz.Services;
using tetelvizz.ViewModel;

namespace tetelvizz.View;

public partial class HomeView : ContentPage
{
    public HomeView()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel(new FileService());
    }
}