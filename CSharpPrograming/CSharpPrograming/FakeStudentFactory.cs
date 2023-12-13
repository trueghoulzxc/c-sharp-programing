using CSharpPrograming.UniversityStructure;
using System.Reflection.Metadata.Ecma335;

namespace CSharpPrograming;

internal static class FakeStudentFactory
{
    private static readonly string[] _names = [
        "Беспалова Анастасия Евгеньевна",
        "Федотова София Игоревна",
        "Руднева Аделина Романовна",
        "Борисова Виктория Дмитриевна",
        "Осипов Степан Маркович",
        "Иванов Сергей Максимович",
        "Александрова Алиса Мироновна",
        "Иванов Сергей Максимович",
        "Романова Варвара Георгиевна",
        "Киселев Даниэль Даниилович",
        "Власов Владислав Савельевич",
        "Захаров Антон Вячеславович",
        "Власова Василиса Ярославовна",
        "Казаков Даниил Игоревич",
        "Никулин Егор Алексеевич",
        "Смирнов Демид Дмитриевич",
        "Савельев Александр Дамирович",
        "Титов Николай Евгеньевич",
        "Ильин Леонид Миронович",
        "Петрова Алиса Егоровна"
    ];

    public static Student AddFakeStudent(Group group)
    {
        int index = new Random().Next(0, _names.Length - 1);

        return new Student(_names[index], group);
    }
}
