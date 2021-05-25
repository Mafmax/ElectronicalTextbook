using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{

    public class Student : User
    {

   
        [Column("Класс")]
        internal string Class
        {
            get
            {
                return $"{_ClassNumber} {_ClassSymbol}";
            }
            set
            {
                string[] splitted = value.Split(' ');
                _ClassNumber = int.Parse(splitted[0]);
                _ClassSymbol = splitted[1];
            }
        }
        public List<Material> CompletedMaterials { get; set; }
        public List<Material> NotCompletedMaterials { get; set; }
        public List<TestEstimation> TestEstimations { get; set; }
        [NotMapped]
        public int _ClassNumber { get; set; }
        [NotMapped]
        public string _ClassSymbol { get; set; }
        public override string ToString()
        {
            return $"{Name} {Lastname} {Class}";
        }
        public Student()
        {
            CompletedMaterials = new List<Material>();
            NotCompletedMaterials = new List<Material>();
            TestEstimations = new List<TestEstimation>();
        }
    }
}
