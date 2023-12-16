using CSharpPrograming.UniversityStructure.Interfaces;

namespace CSharpPrograming.UniversityStructure;

internal abstract class Faculty : ICollectible
{
    public virtual string Name { get; init; } = null!;
    public University University { get; init; }
    public List<Group> Groups { get; } = new List<Group>();

    public Faculty(University university)
    {
        University = university;
        university.Faculties.Add(this);
    }

    public bool TryAcceptStudent(Student student, Group group) 
    {
        return group.TryAcceptStudent(student);
    }

    public bool TryExpelStudent(Student student, Group group)
    {
        return group.TryExpelStudent(student);
    }

    public bool TryTransferStudent(Student student, Group groupFrom, Group groupTo)
    {
        if (groupTo.CanAcceptStudent(student) && groupFrom.TryExpelStudent(student))
            return groupTo.TryAcceptStudent(student);

        return false;
    }

    public override string ToString() => $"{Name} {University.ShortName}";
}
