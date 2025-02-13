using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL
{
    public class Student
    {
        public string LName {  get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Age { get; set; }
        public Student(string lname, string name, string  group, string age)
        {
            LName = lname;
            Name = name;
            Group = group;
            Age = age;
        }
    }
}
