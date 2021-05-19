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
    [Table("Материалы")]
    public class Material
    {
        [Key]
        [Column("Тема материала")]
        public string Theme { get; set; }
        [Column("Содержимое")]
        internal string _Content { get; set; }
        public List<Speciality> Specialities { get; set; }
        public List<Material> NextMaterials { get; set; }
        public List<Material> PreviewMaterials { get; set; }
        public List<Test> Tests{ get; set; }
        public List<Student> StudentsWhoComplete { get; set; }
        public List<Student> StudentsWhoNotComplete { get; set; }
        [Column("Создатель материала")]
        public Teacher Teacher { get; set; }
        [NotMapped]
        public List<InfoBlock> Content
        {
            get
            {
                return JsonConvert.DeserializeObject<List<InfoBlock>>(_Content);
            }
            set
            {
                _Content = JsonConvert.SerializeObject(value);
            }
        }
        public Material()
        {
            NextMaterials = new List<Material>();
            PreviewMaterials = new List<Material>();
            Content = new List<InfoBlock>();
            Tests = new List<Test>();
            Specialities = new List<Speciality>();
        }
    }
}
