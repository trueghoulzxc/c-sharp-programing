namespace CSharpPrograming.UniversityStructure;

internal sealed class PMGroup : Group
{
    public PMGroup(Faculty faculty) : base(faculty)
    {
    }

    /// <summary>
    /// Назначить старостой студента с заданным именем
    /// </summary>
    /// <exception cref="ArgumentException">Если передана пустая строка или null</exception>
    /// <exception cref="InvalidOperationException">Если студент не найден в группе</exception>
    public void SetGroupLeader(string studentName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentName);

        Student student = Students.First(s => s.Name.Contains(studentName));
        GroupLeader = student;
    }
}