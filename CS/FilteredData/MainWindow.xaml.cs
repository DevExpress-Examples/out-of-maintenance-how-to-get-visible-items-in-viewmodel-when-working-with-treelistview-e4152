using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilteredData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void treeListView1_CustomNodeFilter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs e)
        {
            if (checkBox1.IsChecked == false)
                return;
            if (e.Node.RowHandle % 2 != 0)
                e.Visible = false;
            e.Handled = true;
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            gridControl1.RefreshData();
        }
    }
}
