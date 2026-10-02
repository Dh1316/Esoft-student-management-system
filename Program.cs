using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Course { get; set; } = "";
}

class Program
{
    static List<Student> students = new List<Student>();
    static int nextId = 1;
    static string filepath = "students.txt";

    static void Main()
    {
        Console.WriteLine("=== Esoft Student Management System ===");
        Console.WriteLine("Developed by: Dushan Pasinda HND AI");
        LoadFromFile();

        while (true)
        {
            Console.WriteLine("\n1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Save & Exit");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1") AddStudent();
            else if (choice == "2") ViewStudents();
            else if (choice == "3") SearchStudent();
            else if (choice == "4") DeleteStudent();
            else if (choice == "5") { SaveToFile(); break; }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter Course: ");
        string course = Console.ReadLine() ?? "";
        students.Add(new Student { Id = nextId++, Name = name, Course = course });
        Console.WriteLine("Student Added Successfully!");
    }

    static void ViewStudents()
    {
        Console.WriteLine("\n--- Student List ---");
        if (students.Count == 0) Console.WriteLine("No students found.");
        foreach (var s in students)
        {
            Console.WriteLine($"ID: {s.Id} Name: {s.Name} Course: {s.Course}");
        }
    }

    static void SearchStudent()
    {
        Console.Write("Enter Name to Search: ");
        string search = (Console.ReadLine() ?? "").ToLower();
        var found = students.FindAll(s => s.Name.ToLower().Contains(search));
        if (!found.Any())
        {
            Console.WriteLine("Not Found!");
        }
        else
        {
            foreach (var s in found)
            {
                Console.WriteLine($"Found -> ID: {s.Id} Name:{s.Name} Course: {s.Course}");
            }
        }
    }

    static void DeleteStudent()
    {
        Console.Write("Enter ID to Delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var student = students.Find(s => s.Id == id);
        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("ID not Found!");
        }
    }

    static void SaveToFile()
    {
        List<string> lines = new List<string>();
        foreach (var s in students) lines.Add($"{s.Id},{s.Name},{s.Course}");
        File.WriteAllLines(filepath, lines);
        Console.WriteLine($"Saved {students.Count} students to {filepath}");
    }

    static void LoadFromFile()
    {
        if (!File.Exists(filepath)) return;
        var lines = File.ReadAllLines(filepath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 3)
            {
                students.Add(new Student { Id = int.Parse(parts[0]), Name = parts[1], Course = parts[2] });
                nextId = Math.Max(nextId, int.Parse(parts[0]) + 1);
            }
        }
    }
}