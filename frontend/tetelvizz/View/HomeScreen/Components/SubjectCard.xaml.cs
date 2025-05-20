using Microsoft.Maui.Controls;

namespace tetelvizz.View.HomeScreen.Components;
public partial class SubjectCard : ContentView
{
    public SubjectCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SubjectCard), string.Empty, propertyChanged: OnTitleChanged);

    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(SubjectCard), string.Empty, propertyChanged: OnSubtitleChanged);

    public static readonly BindableProperty ExamTypeProperty =
        BindableProperty.Create(nameof(ExamType), typeof(string), typeof(SubjectCard), string.Empty, propertyChanged: OnExamTypeChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public string ExamType
    {
        get => (string)GetValue(ExamTypeProperty);
        set => SetValue(ExamTypeProperty, value);
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectCard)bindable;
        control.TitleLabel.Text = (string)newValue;
    }

    private static void OnSubtitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectCard)bindable;
        control.SubtitleLabel.Text = (string)newValue;
    }

    private static void OnExamTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectCard)bindable;
        control.ExamTypeLabel.Text = (string)newValue;
    }
}
