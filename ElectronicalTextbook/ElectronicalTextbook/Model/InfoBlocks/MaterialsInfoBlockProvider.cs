using ElectronicalTextbook.Model.InfoBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model
{
    class MaterialsInfoBlockProvider : InfoBlockProvider
    {
        protected override IEnumerable<InfoBlock> InitInfoBlocks()
        {
            yield return new TextInfoBlock();
            yield return new PictureInfoBlock();
        }
    }
}
