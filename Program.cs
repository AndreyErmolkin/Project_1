using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementApp
{
    public class WrongMarksExeption : ApplicationException
    {
        public WrongMarksExeption(string message) : base(message)
        {
        }
    }
    public class WrongNameExeption : ApplicationException
    {
        public WrongNameExeption(string message) : base(message)
        {
        }
    }
    public class OutputConsole : IOutput
    {
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
    }
    public interface IOutput
    {
        void WriteLine(string str);
    }
    public class Information
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public Group Groups { get; set; }
        public string NameGroup { get; set; }
    }
    public class DataStorage
    {
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }

        private static DataStorage dataStorage;
        public static DataStorage Instance
        {
            get
            {
                if (dataStorage == null)
                    dataStorage = new DataStorage();
                return dataStorage;
            }
        }
        private DataStorage()
        {
            Students = new List<Student>();
            Groups = new List<Group>();
        }
    }
    public class Group : Information
    {
    }
    public class Student : Information
    {
        public List<int> Marks { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IOutput output = new OutputConsole();

            Group group1 = new Group { NameGroup = "101" };
            Group group2 = new Group { NameGroup = "202" };
            Group group3 = new Group { NameGroup = "303" };

            DataStorage.Instance.Groups.Add(group1);
            DataStorage.Instance.Groups.Add(group2);
            DataStorage.Instance.Groups.Add(group3);

            Student student1 = new Student
            {
                ID = "001",
                Name = "Alexey",
                SurName = "Tipov",
                Marks = new List<int> { 4, 5, 6, 2, 4 },
                Groups = group1
            };
            Student student2 = new Student
            {
                ID = "002",
                Name = "Alesia",
                SurName = "Ribkina",
                Marks = new List<int> { 3, 8, 5, 6, 9 },
                Groups = group2
            };
            Student student3 = new Student
            {
                ID = "003",
                Name = "Ivan Repin",
                SurName = "Repin",
                Marks = new List<int> { 9, 7, 8, 10, 8 },
                Groups = group1
            };
            Student student4 = new Student
            {
                ID = "004",
                Name = "Nikolay",
                SurName = "Zorov",
                Marks = new List<int> { 3, 2, 8, 5, 5 },
                Groups = group3
            };
            Student student5 = new Student
            {
                ID = "005",
                Name = "Olia",
                SurName = "Kitova",
                Marks = new List<int> { 7, 9, 6, 6, 10 },
                Groups = group3
            };
            Student student6 = new Student
            {
                ID = "006",
                Name = "Katia",
                SurName = "Banina",
                Marks = new List<int> { 8, 5, 6, 8, 8 },
                Groups = group2
            };

            DataStorage.Instance.Students.Add(student1);
            DataStorage.Instance.Students.Add(student2);
            DataStorage.Instance.Students.Add(student3);
            DataStorage.Instance.Students.Add(student4);
            DataStorage.Instance.Students.Add(student5);
            DataStorage.Instance.Students.Add(student6);

            while (true)
            {
                output.WriteLine("Выберите операцию: \n" +
                  "1. Вывести информацию о студентах. \n" +
                  "2. Вывести оценки студентов. \n" +
                  "3. Добавить студента. \n" +
                  "4. Редактировать студента. \n" +
                  "5. Удалить студента. \n" +
                  "6. Вывод наименований групп. \n" +
                  "7. Добавление группы. \n" +
                  "8. Выход из программы.");

                var select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        {
                            PrintStudents(DataStorage.Instance.Students, output);
                            break;
                        }
                    case "2":
                        {
                            PrintMarksStudents(DataStorage.Instance.Students, output);
                            break;
                        }
                    case "3":
                        var newStudent = NewStudent(output);
                        DataStorage.Instance.Students.Add(newStudent);
                        try
                        {
                            throw new WrongNameExeption($"Error_2");
                        }
                        catch (WrongNameExeption exc)
                        {
                            Console.WriteLine($"Длина имени не должна быть в гарницах от 3 до 15 символов ({exc.Message})");
                        }
                        try
                        {
                            throw new WrongNameExeption($"Error_2");
                        }
                        catch (WrongNameExeption exc)
                        {
                            Console.WriteLine($"Длина имени не должна быть в гарницах от 3 до 12 символов ({exc.Message})");
                        }
                        try
                        {
                            throw new WrongMarksExeption($"Error_1");
                        }
                        catch (FormatException exc)
                        {
                            Console.WriteLine($"Неккоректно поставлена оценка ({exc.Message})");
                        }
                        break;
                    case "4":
                        {
                            RedactStudent(output);
                            break;
                        }
                    case "5":
                        {
                            DeleteStudent(output);
                            break;
                        }
                    case "6":
                        {
                            PrintGroups(DataStorage.Instance.Groups, output);
                            break;
                        }
                    case "7":
                        {
                            var newGroup = InputNewGroup(output);
                            DataStorage.Instance.Groups.Add(newGroup);
                            break;
                        }
                    case "8":
                        {
                            return;
                        }
                }
            }
        }

        public static void LoadStudents()
        {

        }
        private static void PrintStudents(List<Student> students, IOutput output)
        {
            output.WriteLine("Список студентов:");
            foreach (var student in students)
            {
                output.WriteLine($"ID:{student.ID} \n " +
                   $"Имя:{student.Name}\n" +
                   $"Фамилия:{student.SurName} \n" +
                   $"Группа:{student.Groups?.NameGroup} \n");
            }
        }
        public static void PrintMarksStudents(List<Student> students, IOutput output)
        {
            foreach (var student in students)
            {
                var printMarks = string.Join(", ", student.Marks);
                output.WriteLine($"Имя:{student.Name} {student.SurName} \n" +
                    $"Оценки: {printMarks} \n" +
                    $"Средний балл: {student.Marks.Average()}");
            }
        }
        private static Student NewStudent(IOutput output)
        {
            output.WriteLine("Введите ID:");
            var id = Console.ReadLine();

            output.WriteLine("Введите имя:");
            var firstName = Console.ReadLine();
            try
            {
                if (firstName.Length < 3 || firstName.Length > 10)
                {
                    throw new WrongNameExeption($"Имя ({firstName}) выходит за гарницы допустимых значений");
                }
            }
            catch (WrongNameExeption exc)
            {
                Console.WriteLine($"Длина имени не должна быть в гарницах от 3 до 15 символов ({exc.Message})");
            }

            output.WriteLine("Введите фамилию:");
            var lastName = Console.ReadLine();
            try
            {
                if (lastName.Length < 3 || lastName.Length > 12)
                {
                    throw new WrongNameExeption($"Имя ({lastName}) выходит за гарницы допустимых значений");
                }
            }
            catch (WrongNameExeption exc)
            {
                Console.WriteLine($"Длина имени не должна быть в гарницах от 3 до 12 символов ({exc.Message})");
            }

            output.WriteLine("Введите номер группы:");
            var number = Console.ReadLine();
            var group = new Group { NameGroup = number };

            output.WriteLine("Введите оценки (через запятую):");

            List<string> newMark = Console.ReadLine().Split(",").ToList();
            foreach (var mark in newMark)
            {
                try
                {
                    List<int> list = newMark.Select(n => int.Parse(n)).ToList();
                }
                catch (FormatException exc)
                {
                    throw new WrongMarksExeption($"Неккоректно поставлена оценка -({exc.Message})");
                    Console.WriteLine($"ДНеккоректно поставлена оценка ({exc.Message})");
                }
            }
            List<int> newMarkList = newMark.Select(n => int.Parse(n.Trim())).ToList();
            foreach (var mark in newMarkList)
            {
                if (mark < 0 || mark > 10)
                {
                    throw new WrongMarksExeption($"Оценка ({mark}) выходит за гарницы допустимых значений");
                }
            }
            Student student = new Student
            {
                ID = id,
                Name = firstName,
                SurName = lastName,
                Marks = newMarkList,
                Groups = group
            };
            return student;
        }
        private static void RedactStudent(IOutput output)
        {
            PrintStudents(DataStorage.Instance.Students, output);

            output.WriteLine("Введите ID:");

            var id = Console.ReadLine();

            var studentToChange = DataStorage.Instance.Students.FirstOrDefault(s => s.ID == id);

            if (studentToChange == null)
            {
                output.WriteLine("Нет совпадений");
                return;
            }

            output.WriteLine("Введите имя: ");
            var name = Console.ReadLine();

            output.WriteLine("Введите фамилию: ");
            var surName = Console.ReadLine();

            output.WriteLine("Введите новые оценки(через запятую): ");
            List<string> newMark = Console.ReadLine().Split(",").ToList();
            List<int> newMarkList = newMark.Select(n => int.Parse(n.Trim())).ToList();

            PrintGroups(DataStorage.Instance.Groups, output);

            output.WriteLine("Введите номер группы: ");
            var groupName = Console.ReadLine();

            if (name != "")
                studentToChange.Name = name;

            if (surName != "")
                studentToChange.SurName = surName;

            if (newMark != null)
                studentToChange.Marks = newMarkList;

            if (groupName != "")
            {
                var newGroup = DataStorage.Instance.Groups.FirstOrDefault(g => g.NameGroup == groupName);

                if (newGroup == null)
                {
                    output.WriteLine("Нет совпадений!");
                    return;
                }
                studentToChange.Groups = newGroup;
            }
            PrintStudents(DataStorage.Instance.Students, output);
        }
        private static void DeleteStudent(IOutput output)
        {
            PrintStudents(DataStorage.Instance.Students, output);
            output.WriteLine("Введите ID:");

            var id = Console.ReadLine();

            var studentToChange = DataStorage.Instance.Students.FirstOrDefault(s => s.ID == id);
            if (studentToChange == null)
            {
                output.WriteLine("Нет совпадений");
                return;
            }
            else
            {
                DataStorage.Instance.Students.Remove(studentToChange);
            }
            PrintStudents(DataStorage.Instance.Students, output);
        }
        private static Group InputNewGroup(IOutput output)
        {
            output.WriteLine("Введите номер группы:");
            var number = Console.ReadLine();
            var group = new Group { NameGroup = number };
            return group;
        }
        private static void PrintGroups(List<Group> groups, IOutput output)
        {
            output.WriteLine("Наименование групп:");
            foreach (var group in groups)
            {
                output.WriteLine($"{group.NameGroup}");
            }
        }
    }
}