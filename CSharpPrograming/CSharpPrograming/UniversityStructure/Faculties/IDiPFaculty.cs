namespace CSharpPrograming.UniversityStructure;

internal sealed class IDiPFaculty : Faculty
{
    public IDiPFaculty(University university) : base(university)
    {
    }

    public override string Name { get; init; } = "Факультет издательского дела и полиграфии";
}
