Imports System.Windows

Namespace FilteredData
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub treeListView1_CustomNodeFilter(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs)
			If checkBox1.IsChecked = False Then
				Return
			End If
			If e.Node.RowHandle Mod 2 <> 0 Then
				e.Visible = False
			End If
			e.Handled = True
		End Sub

		Private Sub checkBox1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			gridControl1.RefreshData()
		End Sub
	End Class
End Namespace
