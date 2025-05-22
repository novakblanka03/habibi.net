using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using tetelvizz.Models;
using tetelvizz.Services;

namespace tetelvizz.ViewModel;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly FileService _fileService;

    private ObservableCollection<SubjectFile> _allFiles;
    private ObservableCollection<SubjectFile> _filteredFiles;
    private string _searchQuery = string.Empty;
    private string _selectedSubject = "Összes";

    public ObservableCollection<SubjectFile> FilteredFiles
    {
        get => _filteredFiles;
        set
        {
            _filteredFiles = value;
            OnPropertyChanged();
        }
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (_searchQuery != value)
            {
                _searchQuery = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }
    }

    public string SelectedSubject
    {
        get => _selectedSubject;
        set
        {
            if (_selectedSubject != value)
            {
                _selectedSubject = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }
    }

    public ObservableCollection<string> Subjects { get; } = new ObservableCollection<string>
    {
        "Összes",
        "Matematika",
        "Magyar",
        "Történelem",
        "Fizika",
        "Kémia",
        "Biológia",
        "Informatika"
    };

    public ICommand RefreshCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public HomeViewModel(FileService fileService)
    {
        _fileService = new FileService();
        _allFiles = _fileService.GetAllFiles();
        FilteredFiles = new ObservableCollection<SubjectFile>(_allFiles);
        RefreshCommand = new Command(() =>
        {
            _allFiles = _fileService.GetAllFiles();
            ApplyFilters();
        });
        _fileService = fileService;
    }


    private void ApplyFilters()
    {
        IEnumerable<SubjectFile> filtered = _allFiles;

        if (SelectedSubject != "Összes")
        {
            filtered = filtered.Where(f => f.Subject == SelectedSubject);
        }

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            filtered = filtered.Where(f => f.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        FilteredFiles = new ObservableCollection<SubjectFile>(filtered);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
