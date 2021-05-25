using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model
{
    public abstract class InfoBlockProvider
    {
        private List<InfoBlock> infoBlocks;
        public InfoBlockProvider()
        {
            infoBlocks = new List<InfoBlock>(InitInfoBlocks());
        }
        protected abstract IEnumerable<InfoBlock> InitInfoBlocks();

        public InfoBlock GetInstance<T>() where T : InfoBlock
        {
            return infoBlocks.First(x => x.GetType() == typeof(T)).Clone() as T;
        }
        public InfoBlock GetInstance<T>(T example) where T : InfoBlock
        {
            return example.Clone() as InfoBlock; 
        }
        public IEnumerable<InfoBlock> ShowAvaliable()
        {
            foreach (var item in infoBlocks)
            {
                yield return item;
            }
        }
    }
}
