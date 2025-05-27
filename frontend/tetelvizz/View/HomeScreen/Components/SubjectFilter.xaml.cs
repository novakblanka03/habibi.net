using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace tetelvizz.View.HomeScreen.Components;

public partial class SubjectFilter : ContentView
{
    public static readonly BindableProperty SelectedSubjectProperty =
        BindableProperty.Create(nameof(SelectedSubject), typeof(string), typeof(SubjectFilter), default(string), BindingMode.TwoWay, propertyChanged: OnSelectedSubjectChanged);

    public static readonly BindableProperty IsBacSelectedProperty =
        BindableProperty.Create(nameof(IsBacSelected), typeof(bool), typeof(SubjectFilter), false, propertyChanged: OnIsBacSelectedChanged);

    private readonly Dictionary<string, string> subjectNames = new()
    {
        { "ea_limba_si_literatura_romana", "Román nyelv és irodalom" },
        { "eb_limba_si_literatura_materna", "Anyanyelv és irodalom" },
        { "ec_matematica", "Matematika" },
        { "ec_istorie", "Történelem" },
        { "ed_anatomie_biologie", "Anatómia és biológia" },
        { "ed_chimie", "Kémia" },
        { "ed_fizica", "Fizika" },
        { "ed_geografie", "Földrajz" },
        { "ed_informatica", "Informatika" },
        { "ed_socioumane", "Szociológia" },
        { "limba_si_literatura_romana", "Román nyelv és irodalom" },
        { "matematica", "Matematika" },
        { "limba_si_literatura_materna", "Anyanyelv és irodalom" }
    };

    private List<string> subjects = new();

    public SubjectFilter()
    {
        InitializeComponent();
        UpdateSubjectList();
    }

    public string SelectedSubject
    {
        get => (string)GetValue(SelectedSubjectProperty);
        set => SetValue(SelectedSubjectProperty, value);
    }

    public bool IsBacSelected
    {
        get => (bool)GetValue(IsBacSelectedProperty);
        set => SetValue(IsBacSelectedProperty, value);
    }

    private static void OnSelectedSubjectChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectFilter)bindable;
        control.SetSelectedPickerItem();
    }

    private static void OnIsBacSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectFilter)bindable;
        control.UpdateSubjectList();
    }

    private void UpdateSubjectList()
    {
        subjects = IsBacSelected
            ? new List<string> {
                "ea_limba_si_literatura_romana",
                "eb_limba_si_literatura_materna",
                "ec_matematica",
                "ec_istorie",
                "ed_anatomie_biologie",
                "ed_chimie",
                "ed_fizica",
                "ed_geografie",
                "ed_informatica",
                "ed_socioumane"
            }
            : new List<string> {
                "limba_si_literatura_romana",
                "matematica",
                "limba_si_literatura_materna"
            };

        SubjectPicker.ItemsSource = subjects.Select(s => subjectNames[s]).ToList();

        SetSelectedPickerItem();
    }

    private void SetSelectedPickerItem()
    {
        if (string.IsNullOrEmpty(SelectedSubject)) return;

        var index = subjects.IndexOf(SelectedSubject);
        if (index >= 0 && index < SubjectPicker.ItemsSource.Count)
        {
            SubjectPicker.SelectedIndex = index;
        }
        else if (subjects.Count > 0)
        {
            SelectedSubject = subjects[0];
            SubjectPicker.SelectedIndex = 0;
        }
    }

    private void OnSubjectChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedIndex >= 0 && picker.SelectedIndex < subjects.Count)
        {
            SelectedSubject = subjects[picker.SelectedIndex];
        }
    }
}
