using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{
    [Table("Пользователи")]
    public abstract class User
    {
        [Key]
        public int Id { get; set; }
        [Column("Имя пользователя")]
        public string Username { get; set; }

        [Column("Имя")]
        public string Name { get; set; }

        [Column("Фамилия")]
        public string Lastname { get; set; }

        [Column("Отчество")]
        public string Surname { get; set; }

        [Column("Hash пароля")]
        public string PasswordHash { get; set; }
    }
}
