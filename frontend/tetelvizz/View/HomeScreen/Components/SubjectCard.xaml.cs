using Microsoft.Maui.Controls;

//namespace tetelvizz.View.HomeScreen.Components;
using tetelvizz.Helpers;
using tetelvizz.Models;

namespace tetelvizz.View.HomeScreen.Components
{
    public partial class SubjectCard : ContentView
    {
        public SubjectCard()
        {
            //InitializeComponent();
            BindingContext = this;
        }

        public static readonly BindableProperty IdProperty =
            BindableProperty.Create(nameof(Id), typeof(int), typeof(SubjectCard), 0);

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly BindableProperty YearProperty =
            BindableProperty.Create(nameof(Year), typeof(int), typeof(SubjectCard), 0);

        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty BaremIdProperty =
            BindableProperty.Create(nameof(BaremId), typeof(int), typeof(SubjectCard), 0);

        public int BaremId
        {
            get => (int)GetValue(BaremIdProperty);
            set => SetValue(BaremIdProperty, value);
        }

        public static readonly BindableProperty SubjectProperty =
            BindableProperty.Create(nameof(Subject), typeof(string), typeof(SubjectCard), "");

        public string Subject
        {
            get => (string)GetValue(SubjectProperty);
            set
            {
                SetValue(SubjectProperty, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        //public string ImagePath =>
            //FileProvider.Instance.ExamType == "BAC" ? "bac_placeholder.png" : "en_placeholder.png";

        public string Title =>
            $"{SubjectNames.GetName(Subject)} - Model {Year}";

        public Command TapCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"pdfviewer?fileId={Id}&baremId={BaremId}");
        });
    }

}
