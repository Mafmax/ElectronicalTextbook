using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{
    public class Teacher : User
    {
        [Column("Специализация")]
        public string SpecialityName { get; set; }
        [Column("Специализация")]
        public Speciality Speciality { get; set; }
        public List<Material> Materials { get; set; }
        public List<Test> Tests { get; set; }
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
        public Teacher()
        {
            Materials = new List<Material>();
            Tests = new List<Test>();
        }
    }
}
