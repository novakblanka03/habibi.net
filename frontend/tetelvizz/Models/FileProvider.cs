using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

namespace tetelvizz.Models;

public class FileProvider
{
    private static FileProvider? _instance;
    public static FileProvider Instance => _instance ??= new FileProvider();

    private FileProvider()
    {
        // privát konstruktor
        UpdateSubjects();
    }

    private string _examType = "BAC";
    private string _subject = "ea_limba_si_literatura_romana";
    private List<ExamFile> _files = new();
    private List<string> _subjects = new();

    public string ExamType
    {
        get => _examType;
        set
        {
            if (_examType != value)
            {
                _examType = value;
                UpdateSubjects();
                _subject = _subjects.FirstOrDefault() ?? "";
                FetchFilesAsync(); // opcionálisan await
            }
        }
    }

    public string Subject
    {
        get => _subject;
        set
        {
            _subject = value;
            FetchFilesAsync();
        }
    }

    public List<ExamFile> Files => _files;
    public List<string> Subjects => _subjects;

    private void UpdateSubjects()
    {
        if (_examType == "EN")
        {
            _subjects = new List<string>
            {
                "limba_si_literatura_romana",
                "matematica",
                "limba_si_literatura_materna"
            };
            _subject = _subjects[0];
        }
        else if (_examType == "BAC")
        {
            _subjects = new List<string>
            {
                "ea_limba_si_literatura_romana",
                "eb_limba_si_literatura_materna",
                "ec_matematica",
                "ec_istorie",
                "ed_anatomie_biologie",
                "ed_chimie",
                "ed_fizika",
                "ed_geografie",
                "ed_informatica",
                "ed_socioumane"
            };
            _subject = _subjects[0];
        }
        else
        {
            _subjects = new();
        }
    }

    public async Task FetchFilesAsync()
    {
        try
        {
            var client = new HttpClient();
            var url = $"https://localhost:3000/files/?examType={_examType}&subject={_subject}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<List<ExamFile>>(content);
                _files = json ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
