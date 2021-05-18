using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{
    [Table("Специальности")]
    public class Speciality
    {
        [Key]
        [Column("Название специальности")]
        public string Name { get; set; }
    }
}
