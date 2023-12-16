using CSharpPrograming.UniversityStructure.Interfaces;

namespace CSharpPrograming.UniversityStructure;

internal class Subgroup : ICollectible
{
    public required int SubgroupNumber { get; set; }

    public int StudentsCount => 
        Group.Students
            .Where(x => x.Subgroup.SubgroupNumber == this.SubgroupNumber)
            .Count();

    private Group _group = null!;
    public required Group Group 
    { 
        get => _group; 
        init
        {
            _group = value;
            _group.Subgroups.Add(this);
        }
    }

    public override string ToString() => $"Группа {Group}(подгруппа {SubgroupNumber})";
}