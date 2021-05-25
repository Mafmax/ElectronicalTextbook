using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.Model
{
    public abstract class InfoBlock : ICloneable
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public abstract Panel Draw();
        public abstract void Fill();
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public InfoBlock()
        {
            Id = -1;
        }
    }
}
