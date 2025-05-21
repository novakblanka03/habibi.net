using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using System.Text.Json;
using tetelvizz.Model;

namespace tetelvizz.ViewModel
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty] private string _username;
        [ObservableProperty] private string _eighthGradeCode = "AB1244";
        [ObservableProperty] private string _twelfthGradeCode = "XY9876";
        [ObservableProperty] private string _searchCode;

        [ObservableProperty] private List<StudentData> _allStudents = new();
        [ObservableProperty] private StudentData _foundStudent;

        public ProfileViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            if (firebaseAuthClient.User != null)
                Username = firebaseAuthClient.User.Info.DisplayName;
            else
                Username = "Unknown User";

            _ = LoadAllStudentsAsync();
        }

        private async Task LoadAllStudentsAsync()
        {
            var students = new List<StudentData>();
            var years = Enumerable.Range(2015, 2024 - 2015 + 1);

            foreach (var year in years)
            {
                var filename = $"grades{year}.jsonl";
                try
                {
                    await using var stream = await FileSystem.OpenAppPackageFileAsync(filename);
                    using var reader = new StreamReader(stream);

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var student = JsonSerializer.Deserialize<StudentData>(line);
                            if (student != null)
                                students.Add(student);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Nem sikerült megnyitni: {filename} - {ex.Message}");
                }
            }

            AllStudents = students;
        }

        public void SearchByCode()
        {
            if (string.IsNullOrWhiteSpace(SearchCode))
            {
                FoundStudent = null;
                return;
            }

            FoundStudent = AllStudents.FirstOrDefault(s =>
                s.SchoolCode.Equals(SearchCode.Trim(), StringComparison.OrdinalIgnoreCase)) ?? throw new InvalidOperationException();
        }
    }
}
