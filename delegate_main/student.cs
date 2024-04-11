using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace delegate_main
{
    public class student
    {
        public int MSSV { get; set; }
        public string NameSV { get; set; }
        public float DiemTB { get; set; }
        public Boolean Gender { get; set; }
        public DateTime NS { get; set; }
        public double DTB { get; set; }
        public int ID_Lop { get; set; }
        public void show()
        {
            Console.WriteLine("MSSV =" + MSSV + ", Name =" + NameSV + ",DTB = " + DTB);
        }
    }
}