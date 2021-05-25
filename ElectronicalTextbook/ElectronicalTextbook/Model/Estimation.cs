using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model
{
    public class Estimation
    {
        public bool IsSet { get; set; }
        public double ScoreThreshold { get; private set; }
        public Dictionary<string,double> Grades { get; private set; }

    }
}
