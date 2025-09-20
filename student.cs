using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTrinhNangCao1
{

    enum CourseName { Java = 1, DotNet = 2, CCPP = 3}

    class Student
    {
        public string Id { get; set; } = " ";
        public string Name { get; set; } = " ";
        public int Semester {  get; set; }
        public CourseName Course {  get; set; }

        public override string ToString()
            => $"{Id,-10} | {Name,-20} | Sem {Semester,-2} | {CourseToText(Course)}";

        public static string CourseToText(CourseName c)
            => c switch { CourseName.Java => "Java", CourseName.DotNet => ".Net", _ => "C/C++" };
    }

    
}
