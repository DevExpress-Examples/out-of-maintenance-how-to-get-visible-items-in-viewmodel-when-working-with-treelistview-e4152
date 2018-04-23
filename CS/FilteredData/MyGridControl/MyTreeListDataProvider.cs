using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Grid;

namespace FilteredData.MyGridControl
{
    class MyTreeListDataProvider : TreeListDataProvider
    {
        public MyTreeListDataProvider(TreeListView view)
            : base(view)
        {
        }

        protected override bool CalcNodeVisibility(TreeListNode node)
        {
            bool visible = base.CalcNodeVisibility(node);
            OnRefreshFilteredData(new MyFilterEventArgs(node,visible));
            return visible;
        }

        public delegate void RefreshFilteredDataEventHandler(object sender, MyFilterEventArgs e);
        public event RefreshFilteredDataEventHandler FilteredData;
        protected virtual void OnRefreshFilteredData(MyFilterEventArgs e)
        {
            if (FilteredData != null)
            {
                FilteredData(this, e);
            }
        }
    }

    public class MyFilterEventArgs : EventArgs
    {
        public TreeListNode Node
        {
            get;
            set;
        }
        public bool IsVisible
        {
            get;
            set;
        }
        public MyFilterEventArgs(TreeListNode node, bool visible)
        {
            this.Node = node;
            IsVisible = visible;
        }
    }
}
