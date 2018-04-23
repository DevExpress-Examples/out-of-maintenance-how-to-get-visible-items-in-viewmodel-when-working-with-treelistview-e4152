using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;

namespace FilteredData.MyGridControl
{
    public class MyGridControl : GridControl
    {

        public MyGridControl()
        {
        }

        protected override DataViewBase CreateDefaultView()
        {
            return new MyTreeListView();
        }
    }
}
