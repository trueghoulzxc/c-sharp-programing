using CSharpPrograming.UniversityStructure;
using CSharpPrograming.UniversityStructure.Faculties;
using CSharpPrograming.UniversityStructure.Interfaces;

namespace CSharpPrograming;

internal class Program
{
    static void Main()
    {
        List<ICollectible> list = new();
        
        University university = new() 
        { 
            Name = "Ульяновский государственный университет",
            ShortName = "УлГУ",
            Address = "г. Ульяновск, ул. Набережная р. Свияги, д.106"
        };
        list.Add(university);


        FMIATFaculty fmiatFaculty = new(university);
        list.Add(fmiatFaculty);

        IDiPFaculty idipFaculty = new(university);
        list.Add(idipFaculty);

        PMGroup group1 = new(fmiatFaculty)
        {
            GroupNumber = "ПМ-О-17/1",
            CourseNumber = 1
        };
        list.Add(group1);
        AddRandomStudents(group1, 5);
        group1.TrySetGroupLeader();

        PMGroup group2 = new(fmiatFaculty)
        {
            GroupNumber = "ПМ-О-17/2",
            CourseNumber = 1
        };
        list.Add(group2);
        AddRandomStudents(group2, 5);
        group2.TrySetGroupLeader();

        Console.WriteLine("Созданные объекты:");
        foreach (var item in list)
        {
            Console.WriteLine(item + "; ");
        }
        Console.WriteLine();

        Student testStudent = new Student("Петров И.А.", group1);
        group1.TryAcceptStudent(testStudent);

        SetPMGroupLeader(group1, "Петров"); 
        SetPMGroupLeader(group1, "Святослав"); 
        SetPMGroupLeader(group1, "");

        GetStudent(university, "Петров");
        GetStudent(university, "Святослав");
        GetStudent(university, "");
    }

    static void AddRandomStudents(Group group, int number = 1)
    {
        while (number > 0)
        {
            number--;

            Student student = FakeStudentFactory.AddFakeStudent(group);
        }
    }

    static void SetGroupLeader(Group group, Student? student = null)
    {
        if (group.TrySetGroupLeader(student))
            Console.WriteLine($"{group}: назначен староста {group.GroupLeader}");
        else
            Console.WriteLine($"{group}: не удалось назначить старосту");
    }

    static void SetPMGroupLeader(PMGroup group, string studentName)
    {
        Console.WriteLine("Назначение старостой группы ПМ студента " + studentName);

        try
        {
            group.SetGroupLeader(studentName);
            Console.WriteLine($"{group.GroupLeader} успешно назначен старостой");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
    }

    static void GetStudent(University university, string studentName)
    {
        Console.WriteLine("Поиск студента в университете по имени " + studentName);

        if (university.TryGetStudentByName(studentName, out Student? student))
            Console.WriteLine(student + " найден");
        else
            Console.WriteLine("Студент не найден");

        Console.WriteLine();
    }

    static void TestTransferStudent(Faculty faculty, Group group1)
    {
        Console.WriteLine("\n=====Перевод студента=====");

        PMGroup group2 = new(faculty)
        {
            GroupNumber = "ПМ-О-17/2",
            CourseNumber = 1
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
