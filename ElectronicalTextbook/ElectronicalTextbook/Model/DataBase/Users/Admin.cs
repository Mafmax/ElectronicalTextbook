using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.DataBase
{
    public class Admin : User
    {
        public Admin()
        {
            Username = "admin";
            Lastname = "Петров";
            Name = "Иван";
            Surname = "Олегович";

        }
    }
}
