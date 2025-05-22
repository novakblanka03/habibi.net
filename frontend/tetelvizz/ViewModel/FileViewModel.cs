using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class FileViewModel : INotifyPropertyChanged
{
    private string _examType = "BAC";
    private string _subject = "ea_limba_si_literatura_romana";

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<ExamFile> Files { get; } = new();
    public ObservableCollection<string> Subjects { get; } = new();

    public string ExamType
    {
        get => _examType;
        set
        {
            if (_examType != value)
            {
                _examType = value;
                OnPropertyChanged(nameof(ExamType));
                UpdateSubjects();
                Subject = Subjects.Count > 0 ? Subjects[0] : string.Empty;
                FetchFilesAsync();
            }
        }
    }

    public string Subject
    {
        get => _subject;
        set
        {
            if (_subject != value)
            {
                _subject = value;
                OnPropertyChanged(nameof(Subject));
                FetchFilesAsync();
            }
        }
    }

    private void UpdateSubjects()
    {
        Subjects.Clear();

        if (_examType == "EN")
        {
            Subjects.Add("limba_si_literatura_romana");
            Subjects.Add("matematica");
            Subjects.Add("limba_si_literatura_materna");
        }
        else if (_examType == "BAC")
        {
            Subjects.Add("ea_limba_si_literatura_romana");
            Subjects.Add("eb_limba_si_literatura_materna");
            Subjects.Add("ec_matematica");
            Subjects.Add("ec_istorie");
            Subjects.Add("ed_anatomie_biologie");
            Subjects.Add("ed_chimie");
            Subjects.Add("ed_fizica");
            Subjects.Add("ed_geografie");
            Subjects.Add("ed_informatica");
            Subjects.Add("ed_socioumane");
        }
    }

    public async Task FetchFilesAsync()
    {
        try
        {
            var url = $"https://31e7-2a09-bac5-5130-261e-00-3cc-4.ngrok-free.app/files/?examType={_examType}&subject={_subject}";
            using HttpClient client = new();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var files = JsonSerializer.Deserialize<List<ExamFile>>(json);
                Files.Clear();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        Files.Add(file);
                    }
                }
            }
            else
            {
                // Hibakezelés
            }
        }
        catch (Exception ex)
        {
            // Hibakezelés
        }
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
