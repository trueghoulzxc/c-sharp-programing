using CSharpPrograming.UniversityStructure.Interfaces;

namespace CSharpPrograming.UniversityStructure;

internal class University : ICollectible
{
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Address { get; set; }
    public List<Faculty> Faculties { get; } = new List<Faculty>();

    /// <summary>
    /// Поиск студента по имени
    /// </summary>
    /// <exception cref="ArgumentException">Если передана пустая строка или null</exception>
    /// <exception cref="InvalidOperationException">Если студент не найден</exception>
    public Student GetStudentByName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return Faculties
            .SelectMany(x => x.Groups)
            .SelectMany(x => x.Students)
            .First(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Поиск студента по имени
    /// </summary>
    /// <returns>true если студент найден, иначе false</returns>
    public bool TryGetStudentByName(string name, out Student? student)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            student = null; 
            return false;
        }

        student = Faculties
            .SelectMany(x => x.Groups)
            .SelectMany(x => x.Students)
            .Where(x => x.Name.Contains(name))
            .FirstOrDefault(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return student != null;
    }

    public override string ToString() => Name;
}
