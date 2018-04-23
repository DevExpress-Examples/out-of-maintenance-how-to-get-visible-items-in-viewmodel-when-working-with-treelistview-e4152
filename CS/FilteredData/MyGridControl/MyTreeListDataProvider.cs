using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System;

namespace FilteredData.MyGridControl {
    class MyTreeListDataProvider : TreeListDataProvider {
        public MyTreeListDataProvider(TreeListView view)
            : base(view) {
        }
        protected override TreeListDataController CreateDataController() {
            return new MyTreeListDataController(this);
        }
        public delegate void RefreshFilteredDataEventHandler(object sender, MyFilterEventArgs e);
        public event RefreshFilteredDataEventHandler FilteredData;
        protected internal virtual void OnRefreshFilteredData(MyFilterEventArgs e) {
            if (FilteredData != null)
                FilteredData(this, e);
        }
    }
    class MyTreeListDataController : TreeListDataController {
        public MyTreeListDataController(MyTreeListDataProvider provider) : base(provider) { }
        public new MyTreeListDataProvider DataProvider { get { return (MyTreeListDataProvider)base.DataProvider; } }
        protected override bool CalcNodeVisibility(DevExpress.Data.TreeList.TreeListNodeBase node, Func<object, bool> customFilterFitPredicate = null) {
            bool visible = base.CalcNodeVisibility(node, customFilterFitPredicate);
            DataProvider.OnRefreshFilteredData(new MyFilterEventArgs((TreeListNode)node, visible));
            return visible;
        }
    }
    public class MyFilterEventArgs : EventArgs {
        public TreeListNode Node {
            get;
            set;
        }
        public bool IsVisible {
            get;
            set;
        }
        public MyFilterEventArgs(TreeListNode node, bool visible) {
            this.Node = node;
            IsVisible = visible;
        }
    }
}
