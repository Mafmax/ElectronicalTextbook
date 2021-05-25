using ElectronicalTextbook.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.Model.Controllers
{
    public class TestViewer : Viewer
    {
        private readonly Test test;
        private readonly TestViewMode mode;

        public TestViewer(Test test, TestViewMode mode)
        {
            this.test = test;
            this.mode = mode;
        }
        public TestViewer(TestEstimation testEstimation, TestViewMode mode)
            : this(testEstimation.Test, mode)
        {
            TestEstimation = testEstimation;
        }

        public TestEstimation TestEstimation { get; }

        protected override void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public override Panel View()
        {
            throw new NotImplementedException();
        }
    }
}
