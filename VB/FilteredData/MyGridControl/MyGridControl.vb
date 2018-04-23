Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid

Namespace FilteredData.MyGridControl
	Public Class MyGridControl
		Inherits GridControl

		Public Sub New()
		End Sub

		Protected Overrides Function CreateDefaultView() As DataViewBase
			Return New MyTreeListView()
		End Function
	End Class
End Namespace
