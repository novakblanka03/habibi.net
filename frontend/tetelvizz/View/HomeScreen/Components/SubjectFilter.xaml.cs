using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace tetelvizz.View.HomeScreen.Components;

public partial class SubjectFilter : ContentView
{
    public SubjectFilter()
    {
        InitializeComponent();
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, e) => IsSelected = !IsSelected;
        GestureRecognizers.Add(tapGesture);
        UpdateVisualState();
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SubjectFilter), string.Empty, propertyChanged: OnTextChanged);

    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SubjectFilter), false, propertyChanged: OnIsSelectedChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectFilter)bindable;
        control.TextLabel.Text = (string)newValue;
    }

    private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SubjectFilter)bindable;
        control.UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        FilterFrame.BackgroundColor = IsSelected ? Color.FromArgb("#3F3F46") : Color.FromArgb("#27272A");
        TextLabel.TextColor = IsSelected ? Colors.White : Color.FromArgb("#A1A1AA");
    }
}
