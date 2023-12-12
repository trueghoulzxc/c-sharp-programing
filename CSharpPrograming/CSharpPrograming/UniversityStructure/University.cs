namespace CSharpPrograming.UniversityStructure;

internal class University
{
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Address { get; set; }

    public override string ToString() => "Университет: " + Name;
}
