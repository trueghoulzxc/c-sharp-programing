namespace CSharpPrograming.UniversityStructure;

internal class Subgroup : Group
{
    public required int SubgroupNumber { get; set; }

    public int StudentsCount => Students.Count;

    public override string ToString() => $"Группа {GroupNumber}/{SubgroupNumber}";
}