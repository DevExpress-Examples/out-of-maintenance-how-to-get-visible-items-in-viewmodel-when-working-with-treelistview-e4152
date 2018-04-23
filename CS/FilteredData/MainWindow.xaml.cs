using System.Windows;

namespace FilteredData {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void treeListView1_CustomNodeFilter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs e) {
            if (checkBox1.IsChecked == false)
                return;
            if (e.Node.RowHandle % 2 != 0)
                e.Visible = false;
            e.Handled = true;
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e) {
            gridControl1.RefreshData();
        }
    }
}
