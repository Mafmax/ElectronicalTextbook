using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{
    [Table("Тесты")]
    public class Test
    {
        [Key]
        public int Id { get; set; }
        
        [Column("Тема материала")]
        public string MaterialTheme { get; set; }
        [Column("Материал")]
        public Material Material { get; set; }

        public List<TestEstimation> TestEstimations { get; set; }
        public List<Question> Questions { get; set; }
        [Column("Создатель теста")]
        public Teacher Teacher { get; set; }

 
        
        public Test()
        {
            Questions = new List<Question>();
            TestEstimations = new List<TestEstimation>();
        }
        
        
    }
}
