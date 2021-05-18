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
        public Speciality Speciality { get; set; }
    }
}
