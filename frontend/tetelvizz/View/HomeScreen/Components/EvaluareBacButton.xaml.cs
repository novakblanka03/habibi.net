using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace tetelvizz.View.HomeScreen.Components
{
    public partial class EvaluareBacButton : ContentView
    {
        public static readonly BindableProperty IsBacSelectedProperty =
            BindableProperty.Create(nameof(IsBacSelected), typeof(bool), typeof(EvaluareBacButton), true, propertyChanged: OnSelectionChanged);

        public bool IsBacSelected
        {
            get => (bool)GetValue(IsBacSelectedProperty);
            set => SetValue(IsBacSelectedProperty, value);
        }

        public static readonly BindableProperty SelectionChangedCommandProperty =
            BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(EvaluareBacButton));

        public ICommand? SelectionChangedCommand
        {
            get => (ICommand?)GetValue(SelectionChangedCommandProperty);
            set => SetValue(SelectionChangedCommandProperty, value);
        }

        public Color EvaluareBackgroundColor => IsBacSelected ? Colors.Gray : Color.FromArgb("#1b845e");
        public Color BacBackgroundColor => IsBacSelected ? Color.FromArgb("#1b845e") : Colors.Gray;

        public EvaluareBacButton()
        {
            InitializeComponent();

            this.BindingContext = this;

            var evaluareTapGesture = new TapGestureRecognizer();
            evaluareTapGesture.Tapped += (_, _) =>
            {
                if (IsBacSelected)
                {
                    IsBacSelected = false;
                    SelectionChangedCommand?.Execute(false);
                }
            };

            var bacTapGesture = new TapGestureRecognizer();
            bacTapGesture.Tapped += (_, _) =>
            {
                if (!IsBacSelected)
                {
                    IsBacSelected = true;
                    SelectionChangedCommand?.Execute(true);
                }
            };

            EvaluareButton.GestureRecognizers.Add(evaluareTapGesture);
            BacButton.GestureRecognizers.Add(bacTapGesture);
        }

        private static void OnSelectionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EvaluareBacButton control)
            {
                control.OnPropertyChanged(nameof(EvaluareBackgroundColor));
                control.OnPropertyChanged(nameof(BacBackgroundColor));
            }
        }
    }

}

