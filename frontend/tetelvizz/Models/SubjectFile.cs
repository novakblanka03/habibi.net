namespace tetelvizz.Models;

/// <summary>
/// Egy fájlt reprezentál, amely egy tételt vagy vizsgaanyagot tartalmaz.
/// </summary>
public class SubjectFile
{
    public string Title { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string ClassLevel { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
}
