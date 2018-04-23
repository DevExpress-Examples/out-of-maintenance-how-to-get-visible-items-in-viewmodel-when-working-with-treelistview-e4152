using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace FilteredData.MyGridControl {
    class MyTreeListView : TreeListView {
        bool ChangedFromViewModel;
        public ObservableCollection<object> ForceUnfiltered;
        public object NodeToDelete;

        public static readonly DependencyProperty VisibleDataProperty = DependencyProperty.Register("VisibleData", typeof(ObservableCollection<object>), typeof(MyTreeListView), new UIPropertyMetadata(FilteredDataPropertyChanged));
        public ObservableCollection<object> VisibleData {
            get { return (ObservableCollection<object>)GetValue(VisibleDataProperty); }
            set { SetValue(VisibleDataProperty, value); }
        }
        private static void FilteredDataPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            MyTreeListView view = source as MyTreeListView;
            view.ChangedFromViewModel = true;
            view.ForceUnfiltered = new ObservableCollection<object>();
            ObservableCollection<object> list = e.NewValue as ObservableCollection<object>;
            view.DataControl.RefreshData();
            list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(view.list_CollectionChanged);
            view.CustomNodeFilter += new DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventHandler(view.view_CustomNodeFilter);
        }

        public void view_CustomNodeFilter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs e) {
            if (ForceUnfiltered.Contains(e.Node.Content))
                e.Visible = true;
            e.Handled = true;
        }

        public void list_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (!ChangedFromViewModel)
                return;
            if (e.Action == NotifyCollectionChangedAction.Add && !ForceUnfiltered.Contains(e.NewItems[0])) {
                ForceUnfiltered.Add(e.NewItems[0]);
                this.DataControl.RefreshData();
            }
            if (e.Action == NotifyCollectionChangedAction.Remove && ForceUnfiltered.Contains(e.OldItems[0])) {
                ForceUnfiltered.Remove(e.OldItems[0]);
                NodeToDelete = e.OldItems[0];
                this.DataControl.RefreshData();
            }
        }

        void dataProvider_RefreshFilteredData(object sender, MyFilterEventArgs e) {
            ObservableCollection<object> col = VisibleData;
            if (col == null) {
                return;
            }
            bool f = false;
            if (f)
                col.Clear();
            ChangedFromViewModel = false;
            if (e.IsVisible)
                if (col.Contains(e.Node.Content)) {
                    ChangedFromViewModel = true;
                    return;
                }
                else
                    col.Add(e.Node.Content);
            else {
                if (col.Contains(e.Node.Content) || NodeToDelete == e.Node.Content) {
                    List<object> objectsToRemove = new List<object>();
                    objectsToRemove.AddRange(new TreeListNodeIterator(e.Node.Nodes).Select<TreeListNode, object>(n => n.Content));
                    objectsToRemove.Add(e.Node.Content);
                    foreach (object o in objectsToRemove)
                        col.Remove(o);
                    NodeToDelete = null;
                }
            }
            ChangedFromViewModel = true;
        }

        protected override DevExpress.Xpf.Grid.TreeList.TreeListDataProvider CreateDataProvider() {
            MyTreeListDataProvider dataProvider = new MyTreeListDataProvider(this);
            dataProvider.FilteredData += new MyTreeListDataProvider.RefreshFilteredDataEventHandler(dataProvider_RefreshFilteredData);
            return dataProvider;
        }

    }
}
