using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTrinhNangCao1
{
    class StudentService
    {
        private readonly List<Student> _students = new();

        // 1) Input list
        public void InputMany()
        {
            Console.WriteLine("Enter students (leave Id empty to stop):");
            while (true)
            {
                Console.Write("Id: ");
                string id = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(id)) break;

                Console.Write("Name: ");
                string name = (Console.ReadLine() ?? "").Trim();

                int sem = ReadInt("Semester (integer >= 1): ", min: 1);

                CourseName course = ReadCourse();

                _students.Add(new Student { Id = id, Name = name, Semester = sem, Course = course });
                Console.WriteLine("Added.\n");
            }
        }

        // 2) Search by id or name (substring, case-insensitive)
        public void Search()
        {
            Console.Write("Search by id or name: ");
            string q = (Console.ReadLine() ?? "").Trim().ToLower();

            var rs = _students.Where(s =>
                s.Id.ToLower().Contains(q) || s.Name.ToLower().Contains(q)).ToList();

            PrintList(rs);
        }

        // 3) Edit or Delete by Id
        public void EditOrDelete()
        {
            Console.Write("Enter Id to edit/delete: ");
            string id = (Console.ReadLine() ?? "").Trim();

            var idx = _students.FindIndex(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (idx < 0) { Console.WriteLine("Not found.\n"); return; }

            Console.Write("Choose (U)pdate or (D)elete: ");
            string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (choice == "D")
            {
                _students.RemoveAt(idx);
                Console.WriteLine("Deleted.\n");
            }
            else if (choice == "U")
            {
                var s = _students[idx];
                Console.Write($"Name ({s.Name}): ");
                string name = Console.ReadLine()!;
                if (!string.IsNullOrWhiteSpace(name)) s.Name = name.Trim();

                string semStr;
                Console.Write($"Semester ({s.Semester}): ");
                semStr = Console.ReadLine()!; 
                if (int.TryParse(semStr, out int sem) && sem >= 1) s.Semester = sem;

                Console.WriteLine($"Course (current: {Student.CourseToText(s.Course)}):");
                s.Course = ReadCourse(allowSkip: true, current: s.Course);

                Console.WriteLine("Updated.\n");
            }
            else Console.WriteLine("Cancelled.\n");
        }

        // 4) Statistics: Name | Course | Count
        public void Report()
        {
            Console.WriteLine("Course   | Total Students");
            Console.WriteLine("---------+---------------");

            var stat = _students
                .GroupBy(s => s.Course)  // 1️⃣ Group by course only
                .Select(g => new { Course = Student.CourseToText(g.Key), Count = g.Select(s => s.Id).Distinct().Count() }) // 2️⃣ count distinct students
                .OrderBy(x => x.Course);  // 3️⃣ sort by course name

            foreach (var x in stat)
                Console.WriteLine($"{x.Course,-8}| {x.Count,14}");

            Console.WriteLine();
        }

        // Helper: show all
        public void ShowAll() => PrintList(_students);

        // --- small helpers ---
        private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int v) && v >= min && v <= max) return v;
                Console.WriteLine("Invalid number. Try again.");
            }
        }

        private static CourseName ReadCourse(bool allowSkip = false, CourseName current = CourseName.Java)
        {
            while (true)
            {
                Console.WriteLine("1. Java  2. .Net  3. C/C++" + (allowSkip ? "   (Enter to keep current)" : ""));
                Console.Write("Choose 1-3: ");
                string s = Console.ReadLine() ?? "";
                if (allowSkip && string.IsNullOrWhiteSpace(s)) return current;
                if (int.TryParse(s, out int n) && n >= 1 && n <= 3) return (CourseName)n;
                Console.WriteLine("Invalid choice.");
            }
        }

        private static void PrintList(List<Student> list)
        {
            if (list.Count == 0) { Console.WriteLine("No data.\n"); return; }
            Console.WriteLine("Id        | Name                 | Semester | Course");
            Console.WriteLine("----------+----------------------+----------+--------");
            foreach (var s in list)
                Console.WriteLine($"{s.Id,-10} | {s.Name,-20} | {s.Semester,8} | {Student.CourseToText(s.Course)}");
            Console.WriteLine();
        }
    }
}
