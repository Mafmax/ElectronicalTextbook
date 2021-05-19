using ElectronicalTextbook.Model.DataBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ElectronicalTextbook.Model
{
    [Table("Вопросы")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Column("Содержательная часть вопроса")]
        internal string _QuestionContent { get; set; }
        [Column("Блок ответов")]
        internal string _AnswerBlock { get; set; }
        [Column("Номер теста")]
        public int TestId { get; set; }
        public Test Test { get; set; }
        [NotMapped]

        public AnswerBlock AnswerBlock
        {
            get
            {
                return JsonConvert.DeserializeObject<AnswerBlock>(_AnswerBlock);
            }
            set
            {
                _AnswerBlock = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public List<InfoBlock> QuestionContent
        {
            get
            {
                return JsonConvert.DeserializeObject<List<InfoBlock>>(_QuestionContent);
            }
            set
            {
                _QuestionContent = JsonConvert.SerializeObject(value);
            }
        }
    }
}
