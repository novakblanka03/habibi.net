using System.Collections.ObjectModel;

namespace tetelvizz.Models;

/// <summary>
/// Példányosítható szolgáltatás, amely egyelőre statikus adatokat ad vissza,
/// de később API vagy lokális adatbázis is csatlakoztatható hozzá.
/// </summary>
public static class FileProvider
{
    public static ObservableCollection<SubjectFile> GetAllFiles()
    {
        return new ObservableCollection<SubjectFile>
        {
            new SubjectFile
            {
                Title = "Matematika érettségi 2023",
                FilePath = "files/matek_2023.pdf",
                Subject = "Matematika",
                ClassLevel = "12",
                Year = "2023"
            },
            new SubjectFile
            {
                Title = "Történelem tételsor 2022",
                FilePath = "files/tori_2022.pdf",
                Subject = "Történelem",
                ClassLevel = "12",
                Year = "2022"
            },
            new SubjectFile
            {
                Title = "Fizika dolgozat 2021",
                FilePath = "files/fizika_2021.pdf",
                Subject = "Fizika",
                ClassLevel = "8",
                Year = "2021"
            },
        };
    }
}
