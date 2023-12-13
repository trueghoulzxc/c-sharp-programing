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

        Faculty faculty = new() 
        { 
            Name = "Факультет математики, информационных и авиационных технологий",
            University = university 
        };

        PMGroup group1 = new()
        {
            GroupNumber = "ПМ-О-17/1",
            CourseNumber = 1,
            Faculty = faculty
        };

        PMGroup group2 = new()
        {
            GroupNumber = "ПМ-О-17/2",
            CourseNumber = 1,
            Faculty = faculty
        };

        PMGroup group3 = new()
        {
            GroupNumber = "ПМ-О-16/1",
            CourseNumber = 2,
            Faculty = faculty
        };

        Group.PrintAllObjects();
    }

    static void AddRandomStudents(Group group, int number = 1)
    {
        while (number > 0)
        {
            number--;

            Student student = FakeStudentFactory.AddFakeStudent(group);
            _ = group.TryAcceptStudent(student);
        }
    }

    static void SetGroupLeader(Group group, Student? student = null)
    {
        if (group.TrySetGroupLeader(student))
            Console.WriteLine($"{group}: назначен староста {group.GroupLeader}");
        else
            Console.WriteLine($"{group}: не удалось назначить старосту");
    }

    static void TestTransferStudent(Faculty faculty, Group group1)
    {
        Console.WriteLine("\n=====Перевод студента=====");

        PMGroup group2 = new()
        {
            GroupNumber = "ПМ-О-17/2",
            CourseNumber = 1,
            Faculty = faculty
        };

        AddRandomStudents(group2, 5);
        group2.TrySetGroupLeader();

        Student testStudent = new("Иванов А.Д.", group1);
        faculty.TryAcceptStudent(testStudent, group1);
        
        group1.PrintStudents();
        group2.PrintStudents();

        if (faculty.TryTransferStudent(student: testStudent, groupFrom: group1, groupTo: group2))
            Console.WriteLine($"{testStudent} успешно переведен");
        else
            Console.WriteLine($"Не удалось перевести студента {testStudent}");

        group1.PrintStudents();
        group2.PrintStudents();
    }
}
