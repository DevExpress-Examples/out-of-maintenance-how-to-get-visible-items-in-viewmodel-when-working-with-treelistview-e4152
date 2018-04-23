using DevExpress.Xpf.Grid;

namespace FilteredData.MyGridControl {
    public class MyGridControl : GridControl {

        public MyGridControl() {
        }

        protected override DataViewBase CreateDefaultView() {
            return new MyTreeListView();
        }
    }
}
