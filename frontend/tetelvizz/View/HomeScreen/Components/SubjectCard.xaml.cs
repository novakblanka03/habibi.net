using Microsoft.Maui.Controls;

namespace tetelvizz.View.HomeScreen.Components;
public partial class SubjectCard : ContentView
{
    public static readonly BindableProperty ExamFileProperty =
        BindableProperty.Create(nameof(ExamFile), typeof(ExamFile), typeof(SubjectCard), propertyChanged: OnExamFileChanged);

    public ExamFile ExamFile
    {
        get => (ExamFile)GetValue(ExamFileProperty);
        set => SetValue(ExamFileProperty, value);
    }

    public SubjectCard()
    {
        InitializeComponent();
    }

    private static void OnExamFileChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectCard)bindable;
        control.UpdateUI();
    }

    private void UpdateUI()
    {
        if (ExamFile == null)
            return;

        string examType = ExamFile.ExamType;
        string imagePath = examType == "BAC" ? "bac_placeholder.png" : "en_placeholder.png";

        // Képet és szöveget beállítani
        SubjectImage.Source = imagePath;

        var subjectNames = new Dictionary<string, string>
        {
            {"ea_limba_si_literatura_romana", "Román nyelv és irodalom"},
            {"eb_limba_si_literatura_materna", "Anyanyelv és irodalom"},
            {"ec_matematica", "Matematika"},
            {"ec_istorie", "Történelem"},
            {"ed_anatomie_biologie", "Anatómia és biológia"},
            {"ed_chimie", "Kémia"},
            {"ed_fizica", "Fizika"},
            {"ed_geografie", "Földrajz"},
            {"ed_informatica", "Informatika"},
            {"ed_socioumane", "Társadalomtudományok"},
            {"limba_si_literatura_romana", "Román nyelv és irodalom"},
            {"matematica", "Matematika"},
            {"limba_si_literatura_materna", "Anyanyelv és irodalom"}
        };

        SubjectLabel.Text = $"{subjectNames.GetValueOrDefault(ExamFile.Subject, "Ismeretlen tantárgy")} - Model {ExamFile.Year}";
        DurationLabel.Text = "180 min"; // Ha dinamikus, ezt módosítsd!
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        // Navigáció PDF megjelenítőhöz
        // Ehhez használd Shell-nél a navigációt vagy a NavigationPage-t
        // Pl.:
        // await Shell.Current.GoToAsync($"pdfviewer?id={ExamFile.Id}&baremId={ExamFile.BaremId}");
    }
}
