using ElectronicalTextbook.Model.DataBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model
{
    [Table("Оценки за тесты")]
    public class TestEstimation
    {
        [Key]
        public int Id { get; set; }
        [Column("Тест")]
        public Test Test { get; set; }
        [Column("Цель тестирования")]
        public Student Student { get; set; }
        [Column("Количество попыток")]
        public int Attempts { get; set; }
        [Column("Оценка")]
        public string _Estimation { get; set; }
        [NotMapped]
        public Estimation Estimation
        {
            get
            {
                return JsonConvert.DeserializeObject<Estimation>(_Estimation);
            }
            set
            {
                _Estimation = JsonConvert.SerializeObject(value);
            }
        }

    }
}
