<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModel="clr-namespace:tetelvizz.ViewModel"
             x:DataType="viewModel:RegisterViewModel"
             x:Class="tetelvizz.View.RegisterView"
             Title="Register"
             BackgroundColor="{StaticResource BACKGROUND_COLOR}"
             Shell.NavBarIsVisible="False">

    <VerticalStackLayout Padding="25" Spacing="20">
        <Label Text="Regisztáció"
               FontSize="40"
               FontAttributes="Bold"
               TextColor="{StaticResource DARK_GREEN_LETTER_COLOR}"
               HorizontalOptions="Center" />

        <Entry Placeholder="Írd be a neved"
               Text="{Binding Username}"
               TextColor="White"
               PlaceholderColor="Gray"
               BackgroundColor="Transparent" />

        <Entry Placeholder="Írd be az emailed"
               Keyboard="Email"
               Text="{Binding Email}"
               TextColor="White"
               PlaceholderColor="Gray"
               BackgroundColor="Transparent" />

        <Entry Placeholder="Írd be a jelszavad"
               IsPassword="True"
               Text="{Binding Password}"
               TextColor="White"
               PlaceholderColor="Gray"
               BackgroundColor="Transparent" />

        <Button Text="Regisztráció"
                Command="{Binding RegisterCommand}"
                BackgroundColor="{StaticResource DARK_GREEN_LETTER_COLOR}"
                TextColor="Black"
                CornerRadius="5" />

        <HorizontalStackLayout HorizontalOptions="Center">
            <Label Text="Már van fiókod?" TextColor="White" />
            <Label Text=" Jelentkezz be"
                   TextColor="{StaticResource DARK_GREEN_LETTER_COLOR}">
                <Label.GestureRecognizers>
                    <TapGestureRecognizer Command="{Binding NavigateToLoginCommand}" />
                </Label.GestureRecognizers>
            </Label>
        </HorizontalStackLayout>
    </VerticalStackLayout>
</ContentPage>