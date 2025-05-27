using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using tetelvizz.Models;
//using tetelvizz.Services;

namespace tetelvizz.ViewModel;

public class HomeViewModel : BaseViewModel
{
    private bool _isBacSelected = true;
    private string _selectedSubject;
    private ObservableCollection<ExamFile> _allFiles;
    private ObservableCollection<ExamFile> _files;

    public HomeViewModel()
    {
        SelectBacCommand = new Command(() =>
        {
            IsBacSelected = true;
            FilterFiles();
        });

        SelectCapacitateCommand = new Command(() =>
        {
            IsBacSelected = false;
            FilterFiles();
        });

        // Töltsd fel az összes fájlt (demo)
        _allFiles = new ObservableCollection<ExamFile>(GetAllFiles());
        FilterFiles();
    }

    public bool IsBacSelected
    {
        get => _isBacSelected;
        set => SetProperty(ref _isBacSelected, value);
    }

    public string SelectedSubject
    {
        get => _selectedSubject;
        set
        {
            SetProperty(ref _selectedSubject, value);
            FilterFiles();
        }
    }

    public ObservableCollection<ExamFile> Files
    {
        get => _files;
        set => SetProperty(ref _files, value);
    }

    public ICommand SelectBacCommand { get; }
    public ICommand SelectCapacitateCommand { get; }

    private void FilterFiles()
    {
        if (_allFiles == null)
            return;

        var filtered = _allFiles.Where(f =>
            f.Type == (IsBacSelected ? "bac" : "capacitate") &&
            (string.IsNullOrEmpty(SelectedSubject) || f.Subject == SelectedSubject));

        Files = new ObservableCollection<ExamFile>(filtered);
    }

    private IEnumerable<ExamFile> GetAllFiles()
    {
        return new List<ExamFile>
        {
            new ExamFile { Title = "Román nyelv Model 2025", Duration = 180, Type = "bac", Subject = "Román nyelv és irodalom" },
            new ExamFile { Title = "Román nyelv Capacitate", Duration = 120, Type = "capacitate", Subject = "Román nyelv és irodalom" }
        };
    }
}


//public class HomeViewModel : INotifyPropertyChanged
//{
//    private readonly FileService _fileService;
//    private ObservableCollection<SubjectFile> _allFiles;
//    private ObservableCollection<SubjectFile> _filteredFiles;
//    private string _searchQuery = string.Empty;
//    private string _selectedSubject = string.Empty;
//    private bool _isBacSelected = true;

//    public ObservableCollection<SubjectFile> FilteredFiles
//    {
//        get => _filteredFiles;
//        set
//        {
//            _filteredFiles = value;
//            OnPropertyChanged();
//        }
//    }

//    public string SearchQuery
//    {
//        get => _searchQuery;
//        set
//        {
//            if (_searchQuery != value)
//            {
//                _searchQuery = value;
//                OnPropertyChanged();
//                ApplyFilters();
//            }
//        }
//    }

//    public string SelectedSubject
//    {
//        get => _selectedSubject;
//        set
//        {
//            if (_selectedSubject != value)
//            {
//                _selectedSubject = value;
//                OnPropertyChanged();
//                ApplyFilters();
//            }
//        }
//    }

//    public bool IsBacSelected
//    {
//        get => _isBacSelected;
//        set
//        {
//            if (_isBacSelected != value)
//            {
//                _isBacSelected = value;
//                OnPropertyChanged();
//                ApplyFilters();
//            }
//        }
//    }

//    public ICommand RefreshCommand { get; }

//    public event PropertyChangedEventHandler? PropertyChanged;

//    public HomeViewModel(FileService fileService)
//    {
//        _fileService = fileService;
//        _allFiles = _fileService.GetAllFiles();
//        _filteredFiles = new ObservableCollection<SubjectFile>(_allFiles);
//        RefreshCommand = new Command(() =>
//        {
//            _allFiles = _fileService.GetAllFiles();
//            ApplyFilters();
//        });

//        // Kezdőérték lehet pl. az első BAC tantárgy
//        SelectedSubject = "ec_matematica";
//    }

//    private void ApplyFilters()
//    {
//        IEnumerable<SubjectFile> filtered = _allFiles;

//        if (!string.IsNullOrWhiteSpace(SelectedSubject))
//        {
//            filtered = filtered.Where(f => f.Subject == SelectedSubject);
//        }

//        if (!string.IsNullOrWhiteSpace(SearchQuery))
//        {
//            filtered = filtered.Where(f => f.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
//        }

//        FilteredFiles = new ObservableCollection<SubjectFile>(filtered);
//    }

//    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}
