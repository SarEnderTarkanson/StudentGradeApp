using System;
using System.Collections.Generic;

class Program
{

    static Dictionary<int, Student> students = new Dictionary<int, Student>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Student Grade Management System");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. Assign Grades");
            Console.WriteLine("3. Display Records");
            Console.WriteLine("4. Calculate Averages");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AssignGrades();
                    break;
                case "3":
                    DisplayRecords();
                    break;
                case "4":
                    CalculateAverages();
                    break;
                case "5":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Clear();
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (!students.ContainsKey(id))
            {
                students[id] = new Student(name, id);
                Console.WriteLine("Student added successfully!");
            }
            else
            {
                Console.WriteLine("Student ID already exists!");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID. Must be a number.");
        }
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    static void AssignGrades()
    {
        Console.Clear();
        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int id) && students.ContainsKey(id))
        {
            Console.Write("Enter Subject: ");
            string subject = Console.ReadLine();

            Console.Write("Enter Grade: ");
            if (double.TryParse(Console.ReadLine(), out double grade))
            {
                students[id].AddGrade(subject, grade);
                Console.WriteLine("Grade assigned successfully!");
            }
            else
            {
                Console.WriteLine("Invalid grade. Must be a number.");
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    static void DisplayRecords()
    {
        Console.Clear();
        Console.WriteLine("Student Records:");
        foreach (var student in students.Values)
        {
            Console.WriteLine(student);
        }
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    static void CalculateAverages()
    {
        Console.Clear();
        foreach (var student in students.Values)
        {
            student.CalculateAverage();
        }
        Console.WriteLine("Averages calculated. Press Enter to return to the main menu.");
        Console.ReadLine();
    }
}

class Student
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    private Dictionary<string, double> grades = new Dictionary<string, double>();
    private double averageGrade;

    public Student(string name, int id)
    {
        Name = name;
        ID = id;
    }

    public void AddGrade(string subject, double grade)
    {
        grades[subject] = grade;
    }

    public void CalculateAverage()
    {
        if (grades.Count > 0)
        {
            averageGrade = 0;
            foreach (var grade in grades.Values)
            {
                averageGrade += grade;
            }
            averageGrade /= grades.Count;
        }
        else
        {
            averageGrade = 0;
        }
    }

    public override string ToString()
    {
        string gradeList = string.Join(", ", grades.Select(g => $"{g.Key}: {g.Value:F1}"));
        return $"Name: {Name}, ID: {ID}, Grades: [{gradeList}], Average: {averageGrade:F2}";
    }
}
