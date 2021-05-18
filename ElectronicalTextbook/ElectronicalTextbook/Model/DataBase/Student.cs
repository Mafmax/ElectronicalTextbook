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
        [NotMapped]
        public int _ClassNumber { get; set; }
        [NotMapped]
        public string _ClassSymbol { get; set; }

    }
}
