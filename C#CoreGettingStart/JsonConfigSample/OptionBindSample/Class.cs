using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionBindSample
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Class
    {
        public List<Student> Students { get; set; }
        public string ClassName { get; set; }
    }
}
