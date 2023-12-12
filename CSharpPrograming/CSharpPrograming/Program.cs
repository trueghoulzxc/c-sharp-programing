using CSharpPrograming.UniversityStructure;

namespace CSharpPrograming;

internal class Program
{
    static void Main()
    {
        University university = new() 
        { 
            Name = "Ульяновский государственный университет",
            ShortName = "УлГУ",
            Address = "г. Ульяновск, ул. Набережная р. Свияги, д.106"
        };
        Console.WriteLine(university);

        Faculty faculty = new() 
        { 
            Name = "Факультет математики, информационных и авиационных технологий", 
            University = university 
        };
        Console.WriteLine(faculty);

        Group group = new()
        {
            GroupNumber = "ПМ-О-17",
            CourseNumber = 1,
            Faculty = faculty
        };
        Console.WriteLine(group);

        Subgroup subgroup = new()
        {
            GroupNumber = "ПМ-О-18",
            CourseNumber = 1,
            Faculty = faculty,
            SubgroupNumber = 2
        };
        Console.WriteLine(subgroup);

        AddRandomStudents(group, 5);
        SetGroupLeader(group, group.Students.First());
        group.PrintStudents();
        
        AddRandomStudents(subgroup, 5);
        SetGroupLeader(subgroup);
        subgroup.PrintStudents();

        TestTransferStudent(faculty, group, subgroup);
    }

    static void AddRandomStudents(Group group, int number = 1)
    {
        while (number > 0)
        {
            number--;

            string studentName = FakeStudentFactory.GetFakeStudent();
            _ = group.Faculty.TryAcceptStudent(studentName, group);
        }
    }

    static void SetGroupLeader(Group group, string? groupLeaderName = null)
    {
        if (group.TrySetGroupLeader(groupLeaderName))
            Console.WriteLine($"{group}: назначен староста {group.GroupLeader}");
        else
            Console.WriteLine($"{group}: не удалось назначить старосту");
    }

    static void TestTransferStudent(Faculty faculty, Group group1, Group group2)
    {
        Console.WriteLine("\n=====Перевод студента=====");

        string testStudent = "Иванов А.Д.";
        faculty.TryAcceptStudent(testStudent, group1);
        
        group1.PrintStudents();
        group2.PrintStudents();

        if (faculty.TryTransferStudent(studentName: testStudent, groupFrom: group1, groupTo: group2))
            Console.WriteLine($"Студент {testStudent} успешно переведен");
        else
            Console.WriteLine($"Не удалось перевести студента {testStudent}");

        group1.PrintStudents();
        group2.PrintStudents();
    }
}
