//using System.Collections.ObjectModel;
//using tetelvizz.Models;
//using MyFileProvider = tetelvizz.Models.FileProvider;

//namespace tetelvizz.Services;

///// <summary>
///// A fájlok betöltéséért felelős szolgáltatás. 
///// Jelenleg statikus adatokat ad vissza a fejlesztési fázishoz.
///// </summary>
//public class FileService
//{
//    private ObservableCollection<SubjectFile> _files;

//    public FileService()
//    {
//        // A jövőben itt történhet majd az API-ból vagy adatbázisból való betöltés.
//        _files = tetelvizz.Models.FileProvider.GetAllFiles();
//    }

//    public ObservableCollection<SubjectFile> GetAllFiles()
//    {
//        return _files;
//    }

//    public ObservableCollection<SubjectFile> GetFilesBySubject(string subject)
//    {
//        return new ObservableCollection<SubjectFile>(
//            _files.Where(f => f.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase))
//        );
//    }

//    public ObservableCollection<SubjectFile> GetFilesByClassLevel(string classLevel)
//    {
//        return new ObservableCollection<SubjectFile>(
//            _files.Where(f => f.ClassLevel == classLevel)
//        );
//    }

//    public ObservableCollection<SubjectFile> SearchFiles(string query)
//    {
//        return new ObservableCollection<SubjectFile>(
//            _files.Where(f => f.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
//        );
//    }
//}
