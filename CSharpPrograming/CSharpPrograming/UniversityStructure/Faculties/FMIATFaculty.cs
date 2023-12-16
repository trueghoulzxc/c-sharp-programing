namespace CSharpPrograming.UniversityStructure;

internal sealed class FMIATFaculty : Faculty
{
    public FMIATFaculty(University university) : base(university)
    {
    }

    public override string Name { get; init; } = "Факультет математики, информационных и авиационных технологий";
}
